using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Sphere_Gobbler {
    public partial class MainForm : Form {

        Player player;
        EnemyController enemyController;

        public MainForm() {
            InitializeComponent();
            this.player = new Player(this.panel2, 230, 460, 7, 5);
            this.enemyController = new EnemyController(this.panel2, 3, 15, 20);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                    player.MoveLeft = false;
                    break;
                case Keys.Right:
                    player.MoveRight = false;
                    break;
                case Keys.Up:
                    player.MoveUp = false;
                    break;
                case Keys.Down:
                    player.MoveDown = false;
                    break;
                case Keys.Space:
                    player.Boost = false;
                    break;
            }   
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                    player.MoveLeft = true;
                    break;
                case Keys.Right:
                    player.MoveRight = true;
                    break;
                case Keys.Up:
                    player.MoveUp = true;
                    break;
                case Keys.Down:
                    player.MoveDown = true;
                    break;
                case Keys.Space:
                    player.Boost = true;
                    break;
            }
        }

        private void tmrPlayerMove_Tick(object sender, EventArgs e) {
            if (player.MoveLeft)
                player.MovePlayer(Movement.Left);
            if (player.MoveRight)
                player.MovePlayer(Movement.Right);
            if (player.MoveUp)
                player.MovePlayer(Movement.Up);
            if (player.MoveDown)
                player.MovePlayer(Movement.Down);
        }

        private void tmrEnemyMovement_Tick(object sender, EventArgs e) {
            this.enemyController.UpdateAllEnemies();
        }

        private void tmrCollision_Tick(object sender, EventArgs e) {
            this.enemyController.CheckCollisionsWithBorder();
            this.enemyController.CheckCollisionsWithPlayer(this.player);
            if (!this.player.Alive) {
                
                this.tmrCollision.Stop();
                this.tmrEnemyMovement.Stop();
                this.tmrPlayerMovement.Stop();

                GameOverScreen gameOver = new GameOverScreen(this.panel2, this.player);
            }
        }

        
    }

    public enum Movement {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public class GameOverScreen : PictureBox {
        
        public GameOverScreen(Panel drawPanel, Player player) {

            // main blood panel

            this.Size = new Size(drawPanel.Width, drawPanel.Height);
            this.Left = 0;
            this.Top = -this.Height + 10;
            this.BackColor = Color.Crimson;
            drawPanel.Controls.Add(this);
            this.BringToFront();

            Animate(drawPanel);

            // game over text

            Label gameOverTxt = new Label();

            gameOverTxt.AutoSize = true;
            gameOverTxt.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gameOverTxt.ForeColor = System.Drawing.Color.White;
            gameOverTxt.BackColor = System.Drawing.Color.Crimson;
            gameOverTxt.Size = new System.Drawing.Size(308, 128);
            gameOverTxt.Location = new System.Drawing.Point(drawPanel.Width / 2 - gameOverTxt.Width, drawPanel.Height / 2 - gameOverTxt.Height);
            gameOverTxt.Text = "GAME OVER";
            drawPanel.Controls.Add(gameOverTxt);
            gameOverTxt.BringToFront();
            drawPanel.Update();

            // score
            Label scoreText = new Label();

            scoreText.AutoSize = true;
            scoreText.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            scoreText.ForeColor = System.Drawing.Color.White;
            scoreText.BackColor = System.Drawing.Color.Crimson;
            scoreText.Size = new System.Drawing.Size(308, 128);
            scoreText.Location = new System.Drawing.Point(drawPanel.Width / 2 - scoreText.Width, drawPanel.Height / 2 - scoreText.Height + gameOverTxt.Height + 50);
            scoreText.Text = "SCORE: " + player.Score;
            drawPanel.Controls.Add(scoreText);
            scoreText.BringToFront();
            drawPanel.Update();

        }

        public void Animate(Panel drawPanel) {
            bool complete = false;
            while (!complete) {
                Thread.Sleep(5);
                this.Top += 7;
                drawPanel.Update();
                if (this.Top + this.Height > drawPanel.Height) {
                    this.Top = drawPanel.Height - this.Height;
                    complete = true;
                }
            }
        }


    }

    public class Player : PictureBox{

        public int Speed;
        public bool MoveLeft;
        public bool MoveRight;
        public bool MoveUp;
        public bool MoveDown;
        public int currentSize;
        public bool Alive;
        public int Score;

        public bool Boost;

        public Player(Panel panel, int spawnX, int spawnY, int spawnSize, int speed) {
            this.Left = spawnX;
            this.Top = spawnY;
            this.currentSize = spawnSize;
            this.Size = new Size(this.currentSize, this.currentSize);
            this.Speed = speed;
            this.Image = global::Sphere_Gobbler.Properties.Resources.Player;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            panel.Controls.Add(this);
            Alive = true;
        }

        public void MovePlayer(Movement movement) {

            int moveSpeed = Boost ? this.Speed * 2 : this.Speed;
            
            switch (movement) {
                case Movement.Left:
                    this.Left -= moveSpeed;
                    break;
                case Movement.Right:
                    this.Left += moveSpeed;
                    break;
                case Movement.Up:
                    this.Top -= moveSpeed;
                    break;
                case Movement.Down:
                    this.Top += moveSpeed;
                    break;
            }
        }
    }

    public class Enemy : PictureBox{

        public int currentSize;
        public int Speed;
        public int currentYMovement;
        public int currentXMovement;
        private Random randomNum;

        public Enemy(int spawnX, int spawnY, int spawnSize, int speed) {
            this.Left = spawnX;
            this.Top = spawnY;
            this.currentSize = spawnSize;
            this.Size = new Size(this.currentSize, this.currentSize);
            this.Speed = speed;
            this.Image = global::Sphere_Gobbler.Properties.Resources.Enemy;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
 
            SetInitialMovement();

        }

        private void SetInitialMovement() {
            // set a random initial movement direction
            // movement can be a combination of:
            // up right, up left, down right, down left or standard 4 directions
            // at varying ints up to the speed value

            this.randomNum = new Random(Environment.TickCount);

            bool valid = false;
            while (!valid) {
                this.currentXMovement = this.randomNum.Next(-this.Speed, this.Speed);
                if (this.currentXMovement != 0) valid = true;
            }
            valid = false;
            while (!valid) {
                this.currentYMovement = this.randomNum.Next(-this.Speed, this.Speed);
                if (this.currentYMovement != 0) valid = true;
            }

        }

    }

    public class EnemyController {

        List<Enemy> allEnemies;
        Panel mainPanel;
        Random rndNumGen;
        int minEnemySize;
        public int maxEnemySize;
        int borderPadding = 10;
        int maxSpeed;
        int maxEnemies = 10;

        public EnemyController(Panel mainPanel, int minEnemySize, int maxEnemySize, int maxSpeed){
            this.allEnemies = new List<Enemy>();
            this.mainPanel = mainPanel;
            rndNumGen = new Random(Environment.TickCount);
            this.minEnemySize = minEnemySize;
            this.maxEnemySize = maxEnemySize;
            this.maxSpeed = maxSpeed;
            StartingCreationOfEnemies();
        }

        public void StartingCreationOfEnemies() {
            for(int i = 0; i < maxEnemies; i++) {
                this.CreateNewEnemy();
            }
        }

        public void CreateNewEnemy() {

            if (allEnemies.Count < maxEnemies) {
                int startingSize = rndNumGen.Next(minEnemySize, maxEnemySize);
                int newX = rndNumGen.Next(0, this.mainPanel.Width - startingSize - borderPadding);
                int newY = rndNumGen.Next(0, this.mainPanel.Height - startingSize - borderPadding);
                int enemySpeed = rndNumGen.Next(1, maxSpeed);

                Enemy tempEnemy = new Enemy(newX, newY, startingSize, enemySpeed);

                this.mainPanel.Controls.Add(tempEnemy);

                this.allEnemies.Add(tempEnemy);
            }
        }

        public void UpdateAllEnemies() {
            foreach(var enemy in this.allEnemies) {
                enemy.Left += enemy.currentXMovement;
                enemy.Top += enemy.currentYMovement;
            }
        }

        public void CheckCollisionsWithBorder() {
            foreach(var enemy in this.allEnemies) {

                if (enemy.Left < 0) {
                    enemy.Left = 0;
                    enemy.currentXMovement = -enemy.currentXMovement;
                } 

                if(enemy.Left > this.mainPanel.Width - enemy.Width) {
                    enemy.Left = this.mainPanel.Width - enemy.Width;
                    enemy.currentXMovement = -enemy.currentXMovement;
                } 
                    
                if (enemy.Top < 0) {
                    enemy.Top = 0;
                    enemy.currentYMovement = -enemy.currentYMovement;
                }

                if (enemy.Top > this.mainPanel.Height - enemy.Height) {
                    enemy.Top = this.mainPanel.Height - enemy.Height;
                    enemy.currentYMovement = -enemy.currentYMovement;
                }

            }
        }

        public void CheckCollisionsWithPlayer(Player player) {
            foreach(var enemy in this.allEnemies.ToList()) {
                if (enemy.Bounds.IntersectsWith(player.Bounds)) {
                    // collision occurred
                    this.CalculateSizeSap(enemy, player);
                }
            }
        }

        private void CalculateSizeSap(Enemy enemy, Player player) {
            if(enemy.Width < player.Width) {
                // player eats enemy

                player.Width += Math.Max((int)(enemy.Width * 0.01),2);
                player.Height += Math.Max((int)(enemy.Height * 0.01),2);
                player.Score += enemy.Width / 2 * enemy.Speed;

                //eatenSound.Play();

                this.mainPanel.Controls.Remove(enemy);
                this.allEnemies.Remove(enemy);
                this.CreateNewEnemy();
                this.maxEnemySize += 2;
                this.minEnemySize += 2;
            } else {
                player.Alive = false;
            }
                        
        }

    }
}

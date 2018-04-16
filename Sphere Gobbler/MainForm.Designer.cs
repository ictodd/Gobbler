namespace Sphere_Gobbler {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tmrPlayerMovement = new System.Windows.Forms.Timer(this.components);
            this.tmrEnemyMovement = new System.Windows.Forms.Timer(this.components);
            this.tmrCollision = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(13, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(859, 837);
            this.panel2.TabIndex = 3;
            // 
            // tmrPlayerMovement
            // 
            this.tmrPlayerMovement.Enabled = true;
            this.tmrPlayerMovement.Interval = 20;
            this.tmrPlayerMovement.Tick += new System.EventHandler(this.tmrPlayerMove_Tick);
            // 
            // tmrEnemyMovement
            // 
            this.tmrEnemyMovement.Enabled = true;
            this.tmrEnemyMovement.Interval = 20;
            this.tmrEnemyMovement.Tick += new System.EventHandler(this.tmrEnemyMovement_Tick);
            // 
            // tmrCollision
            // 
            this.tmrCollision.Enabled = true;
            this.tmrCollision.Interval = 20;
            this.tmrCollision.Tick += new System.EventHandler(this.tmrCollision_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 861);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Sphere Gobbler";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer tmrPlayerMovement;
        private System.Windows.Forms.Timer tmrEnemyMovement;
        private System.Windows.Forms.Timer tmrCollision;
    }
}


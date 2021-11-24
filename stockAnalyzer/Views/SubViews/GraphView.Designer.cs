namespace stockAnalyzer
{
    partial class GraphView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
         this.components = new System.ComponentModel.Container();
         this.timer60Hz = new System.Windows.Forms.Timer(this.components);
         this.lblDataInfo = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // timer60Hz
         // 
         this.timer60Hz.Enabled = true;
         this.timer60Hz.Interval = 16;
         this.timer60Hz.Tick += new System.EventHandler(this.timer60Hz_Tick);
         // 
         // lblDataInfo
         // 
         this.lblDataInfo.AutoSize = true;
         this.lblDataInfo.BackColor = System.Drawing.Color.Transparent;
         this.lblDataInfo.Location = new System.Drawing.Point(3, 0);
         this.lblDataInfo.Name = "lblDataInfo";
         this.lblDataInfo.Size = new System.Drawing.Size(0, 15);
         this.lblDataInfo.TabIndex = 0;
         // 
         // GraphView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.Black;
         this.Controls.Add(this.lblDataInfo);
         this.DoubleBuffered = true;
         this.ForeColor = System.Drawing.Color.White;
         this.Name = "GraphView";
         this.Size = new System.Drawing.Size(628, 588);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.Render);
         this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphView_MouseDoubleClick);
         this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphView_MouseMove);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer60Hz;
      private Label lblDataInfo;
   }
}

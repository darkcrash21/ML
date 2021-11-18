namespace stockAnalyzer
{
    partial class BaseSubView
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
            this.tcInvestment = new System.Windows.Forms.TabControl();
            this.tpDetailView = new System.Windows.Forms.TabPage();
            this.lvDetails = new System.Windows.Forms.ListView();
            this.tpGraph = new System.Windows.Forms.TabPage();
            this.tcInvestment.SuspendLayout();
            this.tpDetailView.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcInvestment
            // 
            this.tcInvestment.Controls.Add(this.tpDetailView);
            this.tcInvestment.Controls.Add(this.tpGraph);
            this.tcInvestment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcInvestment.Location = new System.Drawing.Point(0, 0);
            this.tcInvestment.Name = "tcInvestment";
            this.tcInvestment.SelectedIndex = 0;
            this.tcInvestment.Size = new System.Drawing.Size(562, 491);
            this.tcInvestment.TabIndex = 0;
            // 
            // tpDetailView
            // 
            this.tpDetailView.Controls.Add(this.lvDetails);
            this.tpDetailView.Location = new System.Drawing.Point(4, 24);
            this.tpDetailView.Name = "tpDetailView";
            this.tpDetailView.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetailView.Size = new System.Drawing.Size(554, 463);
            this.tpDetailView.TabIndex = 0;
            this.tpDetailView.Text = "Details";
            this.tpDetailView.UseVisualStyleBackColor = true;
            // 
            // lvDetails
            // 
            this.lvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDetails.Location = new System.Drawing.Point(3, 3);
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.Size = new System.Drawing.Size(548, 457);
            this.lvDetails.TabIndex = 0;
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            // 
            // tpGraph
            // 
            this.tpGraph.Location = new System.Drawing.Point(4, 24);
            this.tpGraph.Name = "tpGraph";
            this.tpGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tpGraph.Size = new System.Drawing.Size(554, 463);
            this.tpGraph.TabIndex = 1;
            this.tpGraph.Text = "Graph";
            this.tpGraph.UseVisualStyleBackColor = true;
            // 
            // BaseSubView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcInvestment);
            this.Name = "BaseSubView";
            this.Size = new System.Drawing.Size(562, 491);
            this.Load += new System.EventHandler(this.BaseSubView_Load);
            this.tcInvestment.ResumeLayout(false);
            this.tpDetailView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tcInvestment;
        private TabPage tpDetailView;
        private TabPage tpGraph;
        private ListView lvDetails;
    }
}

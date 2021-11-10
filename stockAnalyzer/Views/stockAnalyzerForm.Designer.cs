namespace stockAnalyzer
{
    partial class stockAnalyzerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxPath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tcStocksOrCoins = new System.Windows.Forms.TabControl();
            this.tpStocks = new System.Windows.Forms.TabPage();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.tpCoins = new System.Windows.Forms.TabPage();
            this.tcStocks = new System.Windows.Forms.TabControl();
            this.tcCoins = new System.Windows.Forms.TabControl();
            this.tcStocksOrCoins.SuspendLayout();
            this.tpStocks.SuspendLayout();
            this.tpCoins.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxPath
            // 
            this.tbxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxPath.Location = new System.Drawing.Point(3, 3);
            this.tbxPath.Name = "tbxPath";
            this.tbxPath.Size = new System.Drawing.Size(463, 23);
            this.tbxPath.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(472, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tcStocksOrCoins
            // 
            this.tcStocksOrCoins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcStocksOrCoins.Controls.Add(this.tpStocks);
            this.tcStocksOrCoins.Controls.Add(this.tpCoins);
            this.tcStocksOrCoins.Location = new System.Drawing.Point(3, 32);
            this.tcStocksOrCoins.Name = "tcStocksOrCoins";
            this.tcStocksOrCoins.SelectedIndex = 0;
            this.tcStocksOrCoins.Size = new System.Drawing.Size(555, 371);
            this.tcStocksOrCoins.TabIndex = 2;
            // 
            // tpStocks
            // 
            this.tpStocks.Controls.Add(this.tcStocks);
            this.tpStocks.Location = new System.Drawing.Point(4, 24);
            this.tpStocks.Name = "tpStocks";
            this.tpStocks.Padding = new System.Windows.Forms.Padding(3);
            this.tpStocks.Size = new System.Drawing.Size(547, 343);
            this.tpStocks.TabIndex = 0;
            this.tpStocks.Text = "Stocks";
            this.tpStocks.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(186, 409);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(372, 23);
            this.progressBar.TabIndex = 3;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.Location = new System.Drawing.Point(3, 409);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(177, 23);
            this.lblProgress.TabIndex = 4;
            // 
            // tpCoins
            // 
            this.tpCoins.Controls.Add(this.tcCoins);
            this.tpCoins.Location = new System.Drawing.Point(4, 24);
            this.tpCoins.Name = "tpCoins";
            this.tpCoins.Padding = new System.Windows.Forms.Padding(3);
            this.tpCoins.Size = new System.Drawing.Size(547, 343);
            this.tpCoins.TabIndex = 1;
            this.tpCoins.Text = "Coins";
            this.tpCoins.UseVisualStyleBackColor = true;
            // 
            // tcStocks
            // 
            this.tcStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStocks.Location = new System.Drawing.Point(3, 3);
            this.tcStocks.Name = "tcStocks";
            this.tcStocks.SelectedIndex = 0;
            this.tcStocks.Size = new System.Drawing.Size(541, 337);
            this.tcStocks.TabIndex = 0;
            // 
            // tcCoins
            // 
            this.tcCoins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCoins.Location = new System.Drawing.Point(3, 3);
            this.tcCoins.Name = "tcCoins";
            this.tcCoins.SelectedIndex = 0;
            this.tcCoins.Size = new System.Drawing.Size(541, 337);
            this.tcCoins.TabIndex = 0;
            // 
            // stockAnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 435);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tcStocksOrCoins);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbxPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "stockAnalyzerForm";
            this.Text = "stockAnalyzer";
            this.tcStocksOrCoins.ResumeLayout(false);
            this.tpStocks.ResumeLayout(false);
            this.tpCoins.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbxPath;
        private Button btnOpen;
        private TabControl tcStocksOrCoins;
        private TabPage tpStocks;
        private ProgressBar progressBar;
        private Label lblProgress;
        private TabPage tpCoins;
        private TabControl tcStocks;
        private TabControl tcCoins;
    }
}
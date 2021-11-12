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
            this.tcStocksOrCoins = new System.Windows.Forms.TabControl();
            this.tpStocks = new System.Windows.Forms.TabPage();
            this.tcStocks = new System.Windows.Forms.TabControl();
            this.tpCoins = new System.Windows.Forms.TabPage();
            this.tcCoins = new System.Windows.Forms.TabControl();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenDataDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDates = new System.Windows.Forms.TableLayoutPanel();
            this.btnProcessDates = new System.Windows.Forms.Button();
            this.lvDates = new System.Windows.Forms.ListView();
            this.tcStocksOrCoins.SuspendLayout();
            this.tpStocks.SuspendLayout();
            this.tpCoins.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpDates.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcStocksOrCoins
            // 
            this.tcStocksOrCoins.Controls.Add(this.tpStocks);
            this.tcStocksOrCoins.Controls.Add(this.tpCoins);
            this.tcStocksOrCoins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStocksOrCoins.Location = new System.Drawing.Point(123, 3);
            this.tcStocksOrCoins.Name = "tcStocksOrCoins";
            this.tcStocksOrCoins.SelectedIndex = 0;
            this.tcStocksOrCoins.Size = new System.Drawing.Size(429, 370);
            this.tcStocksOrCoins.TabIndex = 2;
            // 
            // tpStocks
            // 
            this.tpStocks.Controls.Add(this.tcStocks);
            this.tpStocks.Location = new System.Drawing.Point(4, 24);
            this.tpStocks.Name = "tpStocks";
            this.tpStocks.Padding = new System.Windows.Forms.Padding(3);
            this.tpStocks.Size = new System.Drawing.Size(421, 342);
            this.tpStocks.TabIndex = 0;
            this.tpStocks.Text = "Stocks";
            this.tpStocks.UseVisualStyleBackColor = true;
            // 
            // tcStocks
            // 
            this.tcStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStocks.Location = new System.Drawing.Point(3, 3);
            this.tcStocks.Name = "tcStocks";
            this.tcStocks.SelectedIndex = 0;
            this.tcStocks.Size = new System.Drawing.Size(415, 336);
            this.tcStocks.TabIndex = 0;
            // 
            // tpCoins
            // 
            this.tpCoins.Controls.Add(this.tcCoins);
            this.tpCoins.Location = new System.Drawing.Point(4, 24);
            this.tpCoins.Name = "tpCoins";
            this.tpCoins.Padding = new System.Windows.Forms.Padding(3);
            this.tpCoins.Size = new System.Drawing.Size(421, 342);
            this.tpCoins.TabIndex = 1;
            this.tpCoins.Text = "Coins";
            this.tpCoins.UseVisualStyleBackColor = true;
            // 
            // tcCoins
            // 
            this.tcCoins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCoins.Location = new System.Drawing.Point(3, 3);
            this.tcCoins.Name = "tcCoins";
            this.tcCoins.SelectedIndex = 0;
            this.tcCoins.Size = new System.Drawing.Size(415, 336);
            this.tcCoins.TabIndex = 0;
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(561, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenDataDirectory,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tsmiOpenDataDirectory
            // 
            this.tsmiOpenDataDirectory.Name = "tsmiOpenDataDirectory";
            this.tsmiOpenDataDirectory.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpenDataDirectory.Size = new System.Drawing.Size(224, 22);
            this.tsmiOpenDataDirectory.Text = "Open Data Directory";
            this.tsmiOpenDataDirectory.Click += new System.EventHandler(this.tsmiOpenDataDirectory_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tcStocksOrCoins, 1, 0);
            this.tlpMain.Controls.Add(this.tlpDates, 0, 0);
            this.tlpMain.Location = new System.Drawing.Point(3, 27);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(555, 376);
            this.tlpMain.TabIndex = 6;
            // 
            // tlpDates
            // 
            this.tlpDates.ColumnCount = 1;
            this.tlpDates.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDates.Controls.Add(this.btnProcessDates, 0, 1);
            this.tlpDates.Controls.Add(this.lvDates, 0, 0);
            this.tlpDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDates.Location = new System.Drawing.Point(3, 3);
            this.tlpDates.Name = "tlpDates";
            this.tlpDates.RowCount = 2;
            this.tlpDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDates.Size = new System.Drawing.Size(114, 370);
            this.tlpDates.TabIndex = 3;
            // 
            // btnProcessDates
            // 
            this.btnProcessDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProcessDates.Location = new System.Drawing.Point(3, 333);
            this.btnProcessDates.Name = "btnProcessDates";
            this.btnProcessDates.Size = new System.Drawing.Size(108, 34);
            this.btnProcessDates.TabIndex = 4;
            this.btnProcessDates.Text = "Process";
            this.btnProcessDates.UseVisualStyleBackColor = true;
            this.btnProcessDates.Click += new System.EventHandler(this.btnProcessDates_Click);
            // 
            // lvDates
            // 
            this.lvDates.CheckBoxes = true;
            this.lvDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDates.Location = new System.Drawing.Point(3, 3);
            this.lvDates.Name = "lvDates";
            this.lvDates.Size = new System.Drawing.Size(108, 324);
            this.lvDates.TabIndex = 5;
            this.lvDates.UseCompatibleStateImageBehavior = false;
            this.lvDates.View = System.Windows.Forms.View.SmallIcon;
            // 
            // stockAnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 435);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "stockAnalyzerForm";
            this.Text = "stockAnalyzer";
            this.tcStocksOrCoins.ResumeLayout(false);
            this.tpStocks.ResumeLayout(false);
            this.tpCoins.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpDates.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TabControl tcStocksOrCoins;
        private TabPage tpStocks;
        private ProgressBar progressBar;
        private Label lblProgress;
        private TabPage tpCoins;
        private TabControl tcStocks;
        private TabControl tcCoins;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem tsmiOpenDataDirectory;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpDates;
        private Button btnProcessDates;
        private ListView lvDates;
    }
}
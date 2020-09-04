namespace FacCigo
{
    partial class MainForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuClients = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExamens = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewExam = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListExams = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInvoices = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCurrencies = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExchangeRate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectCurrency = new System.Windows.Forms.ToolStripComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblExchange = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 484);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1109, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // menuStrip
            // 
            this.menuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClients,
            this.mnuExamens,
            this.mnuInvoices,
            this.mnuCurrencies,
            this.mnuSelectCurrency});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1109, 27);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClickedAsync);
            // 
            // mnuClients
            // 
            this.mnuClients.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewClient,
            this.mnuListClient});
            this.mnuClients.Name = "mnuClients";
            this.mnuClients.Size = new System.Drawing.Size(55, 23);
            this.mnuClients.Text = "&Clients";
            // 
            // mnuNewClient
            // 
            this.mnuNewClient.Name = "mnuNewClient";
            this.mnuNewClient.Size = new System.Drawing.Size(156, 22);
            this.mnuNewClient.Text = "Nouveau C&lient";
            // 
            // mnuListClient
            // 
            this.mnuListClient.Name = "mnuListClient";
            this.mnuListClient.Size = new System.Drawing.Size(156, 22);
            this.mnuListClient.Text = "Liste Client";
            // 
            // mnuExamens
            // 
            this.mnuExamens.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewExam,
            this.mnuListExams});
            this.mnuExamens.Name = "mnuExamens";
            this.mnuExamens.Size = new System.Drawing.Size(66, 23);
            this.mnuExamens.Text = "&Examens";
            // 
            // mnuNewExam
            // 
            this.mnuNewExam.Name = "mnuNewExam";
            this.mnuNewExam.Size = new System.Drawing.Size(167, 22);
            this.mnuNewExam.Text = "Nouveau Examen";
            // 
            // mnuListExams
            // 
            this.mnuListExams.Name = "mnuListExams";
            this.mnuListExams.Size = new System.Drawing.Size(167, 22);
            this.mnuListExams.Text = "List Examens";
            // 
            // mnuInvoices
            // 
            this.mnuInvoices.Name = "mnuInvoices";
            this.mnuInvoices.Size = new System.Drawing.Size(63, 23);
            this.mnuInvoices.Text = "&Factures";
            // 
            // mnuCurrencies
            // 
            this.mnuCurrencies.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewCurrency,
            this.mnuListCurrency,
            this.mnuExchangeRate});
            this.mnuCurrencies.Name = "mnuCurrencies";
            this.mnuCurrencies.Size = new System.Drawing.Size(71, 23);
            this.mnuCurrencies.Text = "&Monnaies";
            // 
            // mnuNewCurrency
            // 
            this.mnuNewCurrency.Name = "mnuNewCurrency";
            this.mnuNewCurrency.Size = new System.Drawing.Size(121, 22);
            this.mnuNewCurrency.Text = "Nouvelle";
            // 
            // mnuListCurrency
            // 
            this.mnuListCurrency.Name = "mnuListCurrency";
            this.mnuListCurrency.Size = new System.Drawing.Size(121, 22);
            this.mnuListCurrency.Text = "Liste";
            // 
            // mnuExchangeRate
            // 
            this.mnuExchangeRate.Name = "mnuExchangeRate";
            this.mnuExchangeRate.Size = new System.Drawing.Size(121, 22);
            this.mnuExchangeRate.Text = "&Taux ";
            // 
            // mnuSelectCurrency
            // 
            this.mnuSelectCurrency.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuSelectCurrency.Name = "mnuSelectCurrency";
            this.mnuSelectCurrency.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.mnuSelectCurrency.Size = new System.Drawing.Size(75, 23);
            this.mnuSelectCurrency.Text = "Selection Monnaie";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblExchange);
            this.panel1.Location = new System.Drawing.Point(784, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 34);
            this.panel1.TabIndex = 2;
            // 
            // lblExchange
            // 
            this.lblExchange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExchange.AutoSize = true;
            this.lblExchange.BackColor = System.Drawing.Color.Transparent;
            this.lblExchange.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblExchange.ForeColor = System.Drawing.Color.Red;
            this.lblExchange.Location = new System.Drawing.Point(25, 6);
            this.lblExchange.Name = "lblExchange";
            this.lblExchange.Size = new System.Drawing.Size(142, 25);
            this.lblExchange.TabIndex = 0;
            this.lblExchange.Text = " 1 USD ={0} FC";
            this.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 130);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1109, 354);
            this.panel2.TabIndex = 3;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1109, 506);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuClients;
        private System.Windows.Forms.ToolStripMenuItem mnuNewClient;
        private System.Windows.Forms.ToolStripMenuItem mnuListClient;
        private System.Windows.Forms.ToolStripMenuItem mnuExamens;
        private System.Windows.Forms.ToolStripMenuItem mnuNewExam;
        private System.Windows.Forms.ToolStripMenuItem mnuListExams;
        private System.Windows.Forms.ToolStripMenuItem mnuInvoices;
        private System.Windows.Forms.ToolStripMenuItem mnuCurrencies;
        private System.Windows.Forms.ToolStripMenuItem mnuNewCurrency;
        private System.Windows.Forms.ToolStripMenuItem mnuListCurrency;
        private System.Windows.Forms.ToolStripMenuItem mnuExchangeRate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripComboBox mnuSelectCurrency;
   
    }
}


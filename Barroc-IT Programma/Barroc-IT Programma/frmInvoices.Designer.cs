namespace Barroc_IT_Programma
{
    partial class frmInvoices
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnShowInvoices = new System.Windows.Forms.Button();
            this.btnAddInvoices = new System.Windows.Forms.Button();
            this.btnEditInvoices = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowInvoices
            // 
            this.btnShowInvoices.Location = new System.Drawing.Point(69, 76);
            this.btnShowInvoices.Name = "btnShowInvoices";
            this.btnShowInvoices.Size = new System.Drawing.Size(106, 58);
            this.btnShowInvoices.TabIndex = 0;
            this.btnShowInvoices.Text = "Show invoices";
            this.btnShowInvoices.UseVisualStyleBackColor = true;
            this.btnShowInvoices.Click += new System.EventHandler(this.btnShowInvoices_Click);
            // 
            // btnAddInvoices
            // 
            this.btnAddInvoices.Location = new System.Drawing.Point(12, 12);
            this.btnAddInvoices.Name = "btnAddInvoices";
            this.btnAddInvoices.Size = new System.Drawing.Size(106, 58);
            this.btnAddInvoices.TabIndex = 1;
            this.btnAddInvoices.Text = "Add invoices";
            this.btnAddInvoices.UseVisualStyleBackColor = true;
            // 
            // btnEditInvoices
            // 
            this.btnEditInvoices.Location = new System.Drawing.Point(124, 12);
            this.btnEditInvoices.Name = "btnEditInvoices";
            this.btnEditInvoices.Size = new System.Drawing.Size(106, 58);
            this.btnEditInvoices.TabIndex = 2;
            this.btnEditInvoices.Text = "Edit invoices";
            this.btnEditInvoices.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(154, 140);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 140);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 170);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnEditInvoices);
            this.Controls.Add(this.btnAddInvoices);
            this.Controls.Add(this.btnShowInvoices);
            this.Name = "frmInvoices";
            this.Text = "Invoices";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInvoices_FormClosing_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowInvoices;
        private System.Windows.Forms.Button btnAddInvoices;
        private System.Windows.Forms.Button btnEditInvoices;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnBack;
    }
}
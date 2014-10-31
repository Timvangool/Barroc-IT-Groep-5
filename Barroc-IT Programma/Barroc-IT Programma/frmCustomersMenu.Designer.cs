namespace Barroc_IT_Programma
{
    partial class frmCustomersMenu
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
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnEditCustomer = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblCustomerVisible = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Location = new System.Drawing.Point(12, 12);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(230, 62);
            this.btnAddCustomer.TabIndex = 1;
            this.btnAddCustomer.Text = "Add Customer";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Location = new System.Drawing.Point(11, 96);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(231, 62);
            this.btnEditCustomer.TabIndex = 2;
            this.btnEditCustomer.Text = "Edit Customer";
            this.btnEditCustomer.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(157, 182);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(85, 37);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Log-out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 182);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(85, 37);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblCustomerVisible
            // 
            this.lblCustomerVisible.AutoSize = true;
            this.lblCustomerVisible.Location = new System.Drawing.Point(220, -4);
            this.lblCustomerVisible.Name = "lblCustomerVisible";
            this.lblCustomerVisible.Size = new System.Drawing.Size(35, 13);
            this.lblCustomerVisible.TabIndex = 7;
            this.lblCustomerVisible.Text = "label1";
            this.lblCustomerVisible.Visible = false;
            this.lblCustomerVisible.TextChanged += new System.EventHandler(this.lblCustomerVisible_TextChanged);
            // 
            // frmCustomersMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 231);
            this.Controls.Add(this.lblCustomerVisible);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnEditCustomer);
            this.Controls.Add(this.btnAddCustomer);
            this.Name = "frmCustomersMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCustomersMenu_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnEditCustomer;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnBack;
        public System.Windows.Forms.Label lblCustomerVisible;

    }
}
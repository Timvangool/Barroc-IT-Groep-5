namespace Barroc_IT_Programma
{
    partial class frmAddInvoice
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
            this.components = new System.ComponentModel.Container();
            this.cmbLbl = new System.Windows.Forms.Label();
            this.cmbBox = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tblInvoicesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._Barroc_ITDataSet = new Barroc_IT_Programma._Barroc_ITDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.tblInvoicesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._Barroc_ITDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbLbl
            // 
            this.cmbLbl.AutoSize = true;
            this.cmbLbl.Location = new System.Drawing.Point(13, 13);
            this.cmbLbl.Name = "cmbLbl";
            this.cmbLbl.Size = new System.Drawing.Size(230, 13);
            this.cmbLbl.TabIndex = 0;
            this.cmbLbl.Text = "Please select a customer to view invoices from:";
            // 
            // cmbBox
            // 
            this.cmbBox.FormattingEnabled = true;
            this.cmbBox.Location = new System.Drawing.Point(250, 13);
            this.cmbBox.Name = "cmbBox";
            this.cmbBox.Size = new System.Drawing.Size(242, 21);
            this.cmbBox.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(395, 41);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select Customer";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tblInvoicesBindingSource
            // 
            this.tblInvoicesBindingSource.DataMember = "Tbl_Invoices";
            this.tblInvoicesBindingSource.DataSource = this._Barroc_ITDataSet;
            // 
            // _Barroc_ITDataSet
            // 
            this._Barroc_ITDataSet.DataSetName = "_Barroc_ITDataSet";
            this._Barroc_ITDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmAddInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 221);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.cmbBox);
            this.Controls.Add(this.cmbLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddInvoice";
            this.Text = "Add Invoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddInvoice_FormClosing);
            //((System.ComponentModel.ISupportInitialize)(this.tblInvoicesBindingSource)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this._Barroc_ITDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cmbLbl;
        private System.Windows.Forms.ComboBox cmbBox;
        private System.Windows.Forms.Button btnSelect;
        private _Barroc_ITDataSet _Barroc_ITDataSet;
        private System.Windows.Forms.BindingSource tblInvoicesBindingSource;
       // private _Barroc_ITDataSetTableAdapters.Tbl_InvoicesTableAdapter tbl_InvoicesTableAdapter;
    }
}
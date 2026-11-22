namespace StockAnalysisWinForms
{
    partial class Form_Home
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
            this.button_loadData = new System.Windows.Forms.Button();
            this.label_start = new System.Windows.Forms.TextBox();
            this.label_end = new System.Windows.Forms.TextBox();
            this.dateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
            this.openFileDialog_LoadTickers = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // button_loadData
            // 
            this.button_loadData.Location = new System.Drawing.Point(346, 13);
            this.button_loadData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_loadData.Name = "button_loadData";
            this.button_loadData.Size = new System.Drawing.Size(132, 27);
            this.button_loadData.TabIndex = 0;
            this.button_loadData.Text = "Load Tickers";
            this.button_loadData.UseVisualStyleBackColor = true;
            // 
            // label_start
            // 
            this.label_start.Location = new System.Drawing.Point(13, 15);
            this.label_start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label_start.Name = "label_start";
            this.label_start.Size = new System.Drawing.Size(73, 22);
            this.label_start.TabIndex = 1;
            this.label_start.Text = "Start Date";
            // 
            // label_end
            // 
            this.label_end.Location = new System.Drawing.Point(498, 15);
            this.label_end.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label_end.Name = "label_end";
            this.label_end.Size = new System.Drawing.Size(67, 22);
            this.label_end.TabIndex = 2;
            this.label_end.Text = "End Date";
            // 
            // dateTimePicker_StartDate
            // 
            this.dateTimePicker_StartDate.Location = new System.Drawing.Point(94, 15);
            this.dateTimePicker_StartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker_StartDate.Name = "dateTimePicker_StartDate";
            this.dateTimePicker_StartDate.Size = new System.Drawing.Size(244, 22);
            this.dateTimePicker_StartDate.TabIndex = 3;
            // 
            // dateTimePicker_EndDate
            // 
            this.dateTimePicker_EndDate.Location = new System.Drawing.Point(573, 15);
            this.dateTimePicker_EndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker_EndDate.Name = "dateTimePicker_EndDate";
            this.dateTimePicker_EndDate.Size = new System.Drawing.Size(237, 22);
            this.dateTimePicker_EndDate.TabIndex = 4;
            // 
            // openFileDialog_LoadTickers
            // 
            this.openFileDialog_LoadTickers.FileName = "openFileDialog_LoadTickers";
            this.openFileDialog_LoadTickers.Multiselect = true;
            // 
            // Form_Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 50);
            this.Controls.Add(this.dateTimePicker_EndDate);
            this.Controls.Add(this.dateTimePicker_StartDate);
            this.Controls.Add(this.label_end);
            this.Controls.Add(this.label_start);
            this.Controls.Add(this.button_loadData);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form_Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Home";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_loadData;
        private System.Windows.Forms.TextBox label_start;
        private System.Windows.Forms.TextBox label_end;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndDate;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadTickers;
    }
}
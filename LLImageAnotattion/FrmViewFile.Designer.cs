namespace LLImageAnotattion
{
    partial class FrmViewFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmViewFile));
            this.richTextBoxFile = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxFile
            // 
            this.richTextBoxFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxFile.Location = new System.Drawing.Point(8, 23);
            this.richTextBoxFile.Name = "richTextBoxFile";
            this.richTextBoxFile.Size = new System.Drawing.Size(357, 422);
            this.richTextBoxFile.TabIndex = 0;
            this.richTextBoxFile.Text = "";
            // 
            // FrmViewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 473);
            this.Controls.Add(this.richTextBoxFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmViewFile";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxFile;
    }
}
namespace LLImageAnotattion
{
    partial class FrmCreateProyect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateProyect));
            this.sfComboBoxSize = new Syncfusion.WinForms.ListView.SfComboBox();
            this.autoLabelSize = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.sfComboBoxType = new Syncfusion.WinForms.ListView.SfComboBox();
            this.autoLabelType = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.colorPickerUIAdvColor = new Syncfusion.Windows.Forms.Tools.ColorPickerUIAdv();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.autoLabelName = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.textBoxClass = new System.Windows.Forms.TextBox();
            this.autoLabelClass = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.sfButtonAddClass = new Syncfusion.WinForms.Controls.SfButton();
            this.textBoxClassProyect = new System.Windows.Forms.TextBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.autoLabelPath = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.sfButtonPath = new Syncfusion.WinForms.Controls.SfButton();
            this.sfButtonOK = new Syncfusion.WinForms.Controls.SfButton();
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBoxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBoxType)).BeginInit();
            this.SuspendLayout();
            // 
            // sfComboBoxSize
            // 
            this.sfComboBoxSize.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.sfComboBoxSize.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxSize.Location = new System.Drawing.Point(80, 113);
            this.sfComboBoxSize.Name = "sfComboBoxSize";
            this.sfComboBoxSize.Size = new System.Drawing.Size(298, 26);
            this.sfComboBoxSize.Style.EditorStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxSize.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxSize.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sfComboBoxSize.Style.TokenStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxSize.TabIndex = 0;
            this.sfComboBoxSize.TabStop = false;
            // 
            // autoLabelSize
            // 
            this.autoLabelSize.DX = -68;
            this.autoLabelSize.DY = 2;
            this.autoLabelSize.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.autoLabelSize.LabeledControl = this.sfComboBoxSize;
            this.autoLabelSize.Location = new System.Drawing.Point(12, 115);
            this.autoLabelSize.Name = "autoLabelSize";
            this.autoLabelSize.Size = new System.Drawing.Size(64, 21);
            this.autoLabelSize.TabIndex = 1;
            this.autoLabelSize.Text = "Tamaño";
            // 
            // sfComboBoxType
            // 
            this.sfComboBoxType.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.sfComboBoxType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxType.Location = new System.Drawing.Point(80, 157);
            this.sfComboBoxType.Name = "sfComboBoxType";
            this.sfComboBoxType.Size = new System.Drawing.Size(298, 26);
            this.sfComboBoxType.Style.EditorStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxType.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxType.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sfComboBoxType.Style.TokenStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sfComboBoxType.TabIndex = 2;
            this.sfComboBoxType.TabStop = false;
            // 
            // autoLabelType
            // 
            this.autoLabelType.DX = -44;
            this.autoLabelType.DY = 2;
            this.autoLabelType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.autoLabelType.LabeledControl = this.sfComboBoxType;
            this.autoLabelType.Location = new System.Drawing.Point(36, 159);
            this.autoLabelType.Name = "autoLabelType";
            this.autoLabelType.Size = new System.Drawing.Size(40, 21);
            this.autoLabelType.TabIndex = 3;
            this.autoLabelType.Text = "Tipo";
            // 
            // colorPickerUIAdvColor.RecentGroup
            // 
            this.colorPickerUIAdvColor.RecentGroup.Name = "Recent Colors";
            this.colorPickerUIAdvColor.RecentGroup.Visible = false;
            // 
            // colorPickerUIAdvColor.StandardGroup
            // 
            this.colorPickerUIAdvColor.StandardGroup.Name = "Standard Colors";
            // 
            // colorPickerUIAdvColor.ThemeGroup
            // 
            this.colorPickerUIAdvColor.ThemeGroup.IsSubItemsVisible = true;
            this.colorPickerUIAdvColor.ThemeGroup.Name = "Theme Colors";
            // 
            // colorPickerUIAdvColor
            // 
            this.colorPickerUIAdvColor.Location = new System.Drawing.Point(230, 215);
            this.colorPickerUIAdvColor.Name = "colorPickerUIAdvColor";
            this.colorPickerUIAdvColor.Size = new System.Drawing.Size(172, 211);
            this.colorPickerUIAdvColor.TabIndex = 5;
            this.colorPickerUIAdvColor.Text = "colorPickerUIAdv1";
            this.colorPickerUIAdvColor.Picked += new Syncfusion.Windows.Forms.Tools.ColorPickerUIAdv.ColorPickedEventHandler(this.colorPickerUIAdvColor_Picked);
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxName.Location = new System.Drawing.Point(80, 65);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(298, 29);
            this.textBoxName.TabIndex = 6;
            // 
            // autoLabelName
            // 
            this.autoLabelName.DX = -72;
            this.autoLabelName.DY = 4;
            this.autoLabelName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.autoLabelName.LabeledControl = this.textBoxName;
            this.autoLabelName.Location = new System.Drawing.Point(8, 69);
            this.autoLabelName.Name = "autoLabelName";
            this.autoLabelName.Size = new System.Drawing.Size(68, 21);
            this.autoLabelName.TabIndex = 7;
            this.autoLabelName.Text = "Nombre";
            // 
            // textBoxClass
            // 
            this.textBoxClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.textBoxClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxClass.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxClass.Location = new System.Drawing.Point(80, 212);
            this.textBoxClass.Name = "textBoxClass";
            this.textBoxClass.Size = new System.Drawing.Size(111, 29);
            this.textBoxClass.TabIndex = 9;
            // 
            // autoLabelClass
            // 
            this.autoLabelClass.DX = -51;
            this.autoLabelClass.DY = 4;
            this.autoLabelClass.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.autoLabelClass.LabeledControl = this.textBoxClass;
            this.autoLabelClass.Location = new System.Drawing.Point(29, 216);
            this.autoLabelClass.Name = "autoLabelClass";
            this.autoLabelClass.Size = new System.Drawing.Size(47, 21);
            this.autoLabelClass.TabIndex = 10;
            this.autoLabelClass.Text = "Clase";
            // 
            // sfButtonAddClass
            // 
            this.sfButtonAddClass.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.sfButtonAddClass.Location = new System.Drawing.Point(193, 214);
            this.sfButtonAddClass.Name = "sfButtonAddClass";
            this.sfButtonAddClass.Size = new System.Drawing.Size(32, 24);
            this.sfButtonAddClass.TabIndex = 11;
            this.sfButtonAddClass.Text = "+";
            this.sfButtonAddClass.Click += new System.EventHandler(this.sfButtonAddClass_Click);
            // 
            // textBoxClassProyect
            // 
            this.textBoxClassProyect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxClassProyect.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxClassProyect.Location = new System.Drawing.Point(20, 246);
            this.textBoxClassProyect.Multiline = true;
            this.textBoxClassProyect.Name = "textBoxClassProyect";
            this.textBoxClassProyect.Size = new System.Drawing.Size(204, 180);
            this.textBoxClassProyect.TabIndex = 12;
            // 
            // textBoxPath
            // 
            this.textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPath.Enabled = false;
            this.textBoxPath.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxPath.Location = new System.Drawing.Point(80, 19);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(261, 29);
            this.textBoxPath.TabIndex = 13;
            // 
            // autoLabelPath
            // 
            this.autoLabelPath.DX = -68;
            this.autoLabelPath.DY = 4;
            this.autoLabelPath.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.autoLabelPath.LabeledControl = this.textBoxPath;
            this.autoLabelPath.Location = new System.Drawing.Point(12, 23);
            this.autoLabelPath.Name = "autoLabelPath";
            this.autoLabelPath.Size = new System.Drawing.Size(64, 21);
            this.autoLabelPath.TabIndex = 14;
            this.autoLabelPath.Text = "Carpeta";
            // 
            // sfButtonPath
            // 
            this.sfButtonPath.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.sfButtonPath.Location = new System.Drawing.Point(347, 19);
            this.sfButtonPath.Name = "sfButtonPath";
            this.sfButtonPath.Size = new System.Drawing.Size(31, 29);
            this.sfButtonPath.TabIndex = 15;
            this.sfButtonPath.Text = "+";
            this.sfButtonPath.Click += new System.EventHandler(this.sfButtonPath_Click);
            // 
            // sfButtonOK
            // 
            this.sfButtonOK.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.sfButtonOK.Location = new System.Drawing.Point(230, 486);
            this.sfButtonOK.Name = "sfButtonOK";
            this.sfButtonOK.Size = new System.Drawing.Size(172, 54);
            this.sfButtonOK.TabIndex = 16;
            this.sfButtonOK.Text = "ACEPTAR";
            this.sfButtonOK.Click += new System.EventHandler(this.sfButtonOK_Click);
            // 
            // FrmCreateProyect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 564);
            this.Controls.Add(this.sfButtonOK);
            this.Controls.Add(this.sfButtonPath);
            this.Controls.Add(this.autoLabelPath);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.textBoxClassProyect);
            this.Controls.Add(this.sfButtonAddClass);
            this.Controls.Add(this.autoLabelClass);
            this.Controls.Add(this.textBoxClass);
            this.Controls.Add(this.autoLabelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.colorPickerUIAdvColor);
            this.Controls.Add(this.autoLabelType);
            this.Controls.Add(this.sfComboBoxType);
            this.Controls.Add(this.autoLabelSize);
            this.Controls.Add(this.sfComboBoxSize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCreateProyect";
            this.ShowMaximizeBox = false;
            this.ShowMinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crear proyecto";
            this.Load += new System.EventHandler(this.FrmCreateProyect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBoxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBoxType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.WinForms.ListView.SfComboBox sfComboBoxSize;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelSize;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBoxType;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelType;
        private Syncfusion.Windows.Forms.Tools.ColorPickerUIAdv colorPickerUIAdvColor;
        private System.Windows.Forms.TextBox textBoxName;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelName;
        private System.Windows.Forms.TextBox textBoxClass;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelClass;
        private Syncfusion.WinForms.Controls.SfButton sfButtonAddClass;
        private System.Windows.Forms.TextBox textBoxClassProyect;
        private System.Windows.Forms.TextBox textBoxPath;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelPath;
        private Syncfusion.WinForms.Controls.SfButton sfButtonPath;
        private Syncfusion.WinForms.Controls.SfButton sfButtonOK;
    }
}
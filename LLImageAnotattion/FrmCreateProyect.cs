using LLImageAnotattion.Helpers;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LLImageAnotattion
{
    public partial class FrmCreateProyect : MetroForm
    {
        public classProyect classProyect { get; set; } // Make Reserva a property
        
       

        private int _numLineClass = 0;

        public FrmCreateProyect()
        {
            if (classProyect == null)            
                classProyect = new classProyect(); // Replace ClassProyect with the actual type of classProyect
                
            InitializeComponent();
            sfComboBoxSize.DataSource = classProyect.ListSizesImages();
            sfComboBoxSize.DisplayMember = "Value";
            sfComboBoxSize.ValueMember = "Key";

            sfComboBoxType.DataSource = classProyect.ListTypeProyects();
            sfComboBoxType.DisplayMember = "Value";
            sfComboBoxType.ValueMember = "Key";

        }

        private void colorPickerUIAdvColor_Picked(object sender, Syncfusion.Windows.Forms.Tools.ColorPickerUIAdv.ColorPickedEventArgs args)
        {
            textBoxClass.BackColor = this.colorPickerUIAdvColor.SelectedColor;
        }
        /// <summary>
        /// Agrega una clase al proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sfButtonAddClass_Click(object sender, EventArgs e)
        {
            if (textBoxClassProyect.Text.Trim() == "")
            {
                _numLineClass = 0;
            }
            if (textBoxClass.Text == "")
            {
                MessageBox.Show("No se ha ingresado el nombre de la clase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtiene el color de fondo del TextBox
            Color backColorClass = textBoxClass.BackColor;

            // Convierte el color a hexadecimal
            string hexColorClass = ColorTranslator.ToHtml(backColorClass);

            textBoxClassProyect.AppendText($"{_numLineClass} : {textBoxClass.Text} : {hexColorClass}{Environment.NewLine}");
            textBoxClass.Text = "";
            _numLineClass++;

        }

        private void sfButtonPath_Click(object sender, EventArgs e)
        {

            // Si el valor es nulo o vacío, abre un diálogo para seleccionar una carpeta
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Selecciona la carpeta del proyecto";
                
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {                    
                    textBoxPath.Text = folderBrowserDialog.SelectedPath;
                }

            }
        }

        private void FrmCreateProyect_Load(object sender, EventArgs e)
        {

        }

        private void sfButtonOK_Click(object sender, EventArgs e)
        {
            string sizeImage = sfComboBoxSize.SelectedValue is null ? "" : sfComboBoxSize.SelectedValue.ToString();
            string typeProyect = sfComboBoxType.SelectedValue is null ? "" : sfComboBoxType.SelectedValue.ToString();

             if (sizeImage == "")
             {
                 MessageBox.Show("No se ha seleccionado el tamaño de las imágenes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             if (typeProyect == "")
             {
                 MessageBox.Show("No se ha seleccionado el tipo de proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             if (textBoxClassProyect.Text.Trim() == "")
             {
                 MessageBox.Show("No se han ingresado las clases", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             if (textBoxPath.Text.Trim() == "")
             {
                 MessageBox.Show("No se ha seleccionado la ruta del proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             if (textBoxName.Text.Trim() == "")
             {
                 MessageBox.Show("No se ha ingresado el nombre del proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
                         
            classProyect.Path = textBoxPath.Text;
            classProyect.NameProyect = textBoxName.Text;
            classProyect.SizeImages = sizeImage;
            classProyect.TypeProyect = typeProyect;
            classProyect.ClassProyect = textBoxClassProyect.Text;           
            DialogResult = DialogResult.OK;
        }
    }
}

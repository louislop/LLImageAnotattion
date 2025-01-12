using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace LLImageAnotattion
{
    public partial class FrmViewFile :  MetroForm
    {
        private string _fileLabel = null;
        public FrmViewFile(string FileLabel)
        {
            InitializeComponent();

            this._fileLabel = FileLabel;
            this.Text = "Visualizador de Archivos - " + this._fileLabel;

            try
            {
                string contenido = File.ReadAllText(this._fileLabel);
                richTextBoxFile.Text = contenido;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message);
            }

        }
    }
}

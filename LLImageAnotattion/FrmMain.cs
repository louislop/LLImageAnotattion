using LLImageAnotattion.Helpers;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LLImageAnotattion.Helpers.ImageManager;
using System.IO;

namespace LLImageAnotattion
{
    public partial class FrmMain : RibbonForm
    {

        /// <summary>
        /// 
        /// </summary>
        private APPHandler APPHandler = new APPHandler();

        /// <summary>
        /// Panel de barra lateral
        /// </summary>
        private Panel _panelSidebar;

        /// <summary>
        /// Panel de contenido
        /// </summary>
        private Panel _panelContent;

        /// <summary>
        /// Ruta del proyecto
        /// </summary>
        private string _workspace = null;
      

        public FrmMain()
        {
           

            InitializeComponent();
            APPHandler.CreateWorkspace(this);


        }

        private void toolStripButtonProyect_Click(object sender, EventArgs e)
        {
            FrmCreateProyect frmCreateProyect = new FrmCreateProyect();
            if ( frmCreateProyect.ShowDialog() == DialogResult.OK)
            {
                if (frmCreateProyect.classProyect != null)
                {
                    APPHandler.CreateProyect(frmCreateProyect.classProyect.Path, frmCreateProyect.classProyect.NameProyect, frmCreateProyect.classProyect.SizeImages, frmCreateProyect.classProyect.TypeProyect, frmCreateProyect.classProyect.ClassProyect);

                    this._workspace = APPHandler.GetWorkspace();
                    this.toolStripComboBoxClass.Items.Clear();
                    List<object> listClass = APPHandler.OpenProyect(this._workspace);
                    this.toolStripComboBoxClass.Items.AddRange(listClass.ToArray());

                    MessageBox.Show($"Proyecto {frmCreateProyect.classProyect.NameProyect} creado con exito \n {this._workspace}");
                    this.Text = $"LLImageAnotattion - {this._workspace}";

                }
            }


        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            // Si el valor es nulo o vacío, abre un diálogo para seleccionar una carpeta
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Selecciona la carpeta del proyecto";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    APPHandler.OpenProyect(folderBrowserDialog.SelectedPath);
                    // Comprobar si APPHandler.OpenProyect devuelve un valor no nulo
                    List<object> listClass = APPHandler.OpenProyect(folderBrowserDialog.SelectedPath);
                    if (listClass != null)
                    {
                        this._workspace = folderBrowserDialog.SelectedPath;
                        this.toolStripComboBoxClass.Items.Clear();
                        this.toolStripComboBoxClass.Items.AddRange(listClass.ToArray());
                        MessageBox.Show($"Proyecto abierto con exito \n {this._workspace}");
                        this.Text = $"LLImageAnotattion - {this._workspace}";
                      
                    }
                    else
                    {
                        // Manejar el caso en que APPHandler.OpenProyect devuelve null
                        MessageBox.Show("Error al abrir el proyecto. Verifique la ruta o el archivo 'data.yaml'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }
        }

        /// <summary>
        /// Seleccionar clase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// Obtener el valor seleccionado
            string valorSeleccionado = toolStripComboBoxClass.SelectedItem.ToString();
            int index = toolStripComboBoxClass.SelectedIndex;
            APPHandler.SetCurrentCategory(index);
        }

        /// <summary>
        /// Importar imágenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonImage_Click(object sender, EventArgs e)
        {
            if(this._workspace == null)
            {
                MessageBox.Show("No se ha seleccionado un proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si el valor es nulo o vacío, abre un diálogo para seleccionar una carpeta
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Selecciona la carpeta para importar JPG";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    APPHandler.ImportImages(folderBrowserDialog.SelectedPath, ImageOperation.ResizeAndCrop);
                    MessageBox.Show("Imágenes importadas correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void toolStripButtonBoxSave_Click(object sender, EventArgs e)
        {
            var ImageSelect = APPHandler.GetImageSelect();
            if (ImageSelect != null && ImageSelect.Image != null)
            {
                if (APPHandler.CountBoundingBoxes() == 0)
                {
                    MessageBox.Show("No hay BoundingBoxes a guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Guardando " + APPHandler.CountBoundingBoxes().ToString() + " BoundingBoxes" , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                APPHandler.SaveBoundingBoxes(ImageSelect.FileLabel);
                APPHandler.RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado una imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }      
         }

        private void toolStripButtonBoxDel_Click(object sender, EventArgs e)
        {
            var ImageSelect = APPHandler.GetImageSelect();
            if (ImageSelect != null && ImageSelect.Image != null)
            {
                if (File.Exists(ImageSelect.FileLabel))
                    File.Delete(ImageSelect.FileLabel);
                APPHandler.ClearBoundingBoxes();
                APPHandler.RefreshDataGrid();
            }
               

           
        }

        private void toolStripButtonIMGDelete_Click(object sender, EventArgs e)
        {
            var ImageSelect = APPHandler.GetImageSelect();
            if (ImageSelect != null && ImageSelect.Image != null)
            {
                
                    if (MessageBox.Show("¿Estás seguro de que deseas eliminar la imagen? \n" + ImageSelect.FileImage, "Confirmar eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        if (File.Exists(ImageSelect.FileLabel))
                            File.Delete(ImageSelect.FileLabel);
                        if (File.Exists(ImageSelect.FileImage))
                            File.Delete(ImageSelect.FileImage);
                        APPHandler.ClearBoundingBoxes();
                        APPHandler.RefreshDataGrid();
                    }
               
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonBoxView_Click(object sender, EventArgs e)
        {
            var ImageSelect = APPHandler.GetImageSelect();

            if (ImageSelect != null && ImageSelect.FileLabel != null)
            {
                if (File.Exists(ImageSelect.FileLabel))
                {
                    FrmViewFile frmViewFile = new FrmViewFile(ImageSelect.FileLabel);
                    frmViewFile.ShowDialog();
                }
                    
                
            }
            
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Documents;

using static LLImageAnotattion.Helpers.ImageManager;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;




namespace LLImageAnotattion.Helpers
{
    internal class APPHandler
    {
        public ImageInfo ImageSelect;

        /// <summary>
        /// Tamaño de las imágenes
        /// </summary>
        private string _SizeImage = null;

        /// <summary>
        /// Ruta del archivo YAML
        /// </summary>
        private string _FileYAML = null;

        /// <summary>
        /// Ruta del proyecto
        /// </summary>
        private string _Workspace = null;
        
        /// <summary>
        /// Ruta de las imágenes
        /// </summary>
        private string _PathImages = null;
        
        /// <summary>
        /// Ruta de las etiquetas
        /// </summary>
        private string _PathLabels = null;

        /// <summary>
        /// Panel de barra lateral
        /// </summary>
        private Panel _PanelSidebar;

        /// <summary>
        /// Panel de contenido
        /// </summary>
        private Panel _PanelContent;

        /// <summary>
        /// PictureBox principal
        /// </summary>
        private PictureBox _PictureBoxMain;

        /// <summary>
        /// SfDataGrid para mostrar las imágenes
        /// </summary>
        private SfDataGrid _SfDataGrid;

        /// <summary>
        /// Clase de ayuda para el SfDataGrid
        /// </summary>
        private sfDataGridImages dataGridHelper;

       
        /// <summary>
        /// Clase para trabajar con los archivos INI de configuracion
        /// </summary>
        private IniFileHandler IniSettings = new IniFileHandler("settings.ini");

        /// <summary>
        /// Establecer la categoría actual en el fichero de configuración
        /// </summary>
        /// <param name="Category"></param>
        public void SetCurrentCategory(int Category) 
        {  
            dataGridHelper.SetCurrentCategory(Category);
        }

        /// <summary>
        /// Limpiar las anotaciones
        /// </summary>
        public void ClearBoundingBoxes()
        {
            dataGridHelper.ClearBoundingBoxes();
        }

        /// <summary>
        /// Guarda las anotaciones
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveBoundingBoxes(string filePath)
        {
            dataGridHelper.SaveBoundingBoxes(filePath);
        }

        /// <summary>
        /// Obtener la imagen seleccionada
        /// </summary>
        /// <returns></returns>
        public ImageInfo GetImageSelect()
        {
         
            ImageSelect = dataGridHelper.ImageSelect;
            return ImageSelect;

        }
        /// <summary>
        /// Contar las bounding boxes
        /// </summary>
        /// <returns></returns>
        public int CountBoundingBoxes()
        {
            return dataGridHelper.CountBoundingBoxes();
        }

        /// <summary>
        /// Refrescar el contenido del SfDataGrid
        /// </summary>
        public void RefreshDataGrid()
        {
            if(this._PathImages != null && this._PathLabels != null)
            {
                dataGridHelper.Refresh(this._PathImages, this._PathLabels);
            }
           
        }

        /// <summary>
        /// Obtener el tamaño de las imágenes
        /// </summary>
        /// <returns></returns>
        public string GetSizeImage()
        {
            return this._SizeImage;
        }

        /// <summary>
        /// Obtener la ruta del archivo YAML
        /// </summary>
        /// <returns></returns>
        public string GetFileYAML()
        {
            return this._FileYAML;
        }

        /// <summary>
        /// Obtener la ruta de las etiquetas
        /// </summary>
        /// <returns></returns>
        public string GetPathLabels()
        {
            return this._PathLabels;
        }

        /// <summary>
        /// Obtener la ruta de las imágenes
        /// </summary>
        /// <returns></returns>
        public string GetPathImages()
        {
            return this._PathImages;
        }

        /// <summary>
        /// Obtener la ruta del proyecto
        /// </summary>
        /// <returns></returns>
        public string GetWorkspace()
        {
            return this._Workspace;
        }


        /// <summary>
        /// Crear el espacio de trabajo
        /// </summary>
        /// <param name="FrmMain"></param>
        public void CreateWorkspace(RibbonForm FrmMain)
        {
            // Configuración del PictureBox
            this._PictureBoxMain = new PictureBox
            {
                Size = new Size(640, 640),
                BackColor = System.Drawing.SystemColors.ButtonFace,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.None // Importante: quitamos cualquier anclaje
            };

            // Configuración del SfDataGrid
            dataGridHelper = new sfDataGridImages(this._PictureBoxMain);

       
            // Configuración del panel sidebar
            this._PanelSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 205,
                BackColor = System.Drawing.SystemColors.ButtonFace
            };

            // Crear y configurar el SfDataGrid
             this._SfDataGrid = dataGridHelper.GetSfDataGrid();
             this._PanelSidebar.Controls.Add(this._SfDataGrid);

            // Configuración del panel de contenido
            this._PanelContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0) // Aseguramos que no haya padding
            };

            
            // Primero agregamos los paneles al formulario
            FrmMain.Controls.Add(this._PanelContent);
            FrmMain.Controls.Add(this._PanelSidebar);

            // Luego agregamos el PictureBox al panel de contenido
            this._PanelContent.Controls.Add(this._PictureBoxMain);

            // Suscribimos a los eventos de redimensionamiento
            this._PanelContent.Resize += (s, e) => this._CenterPictureBox();
            this._PictureBoxMain.Resize += (s, e) => this._CenterPictureBox();

            // Realizamos el centrado inicial
            this._CenterPictureBox();
        }

        /// <summary>
        /// Crear un proyecto
        /// </summary>
        /// <param name="pathProyect"></param>
        /// <param name="nameProyect"></param>
        /// <param name="sizeImage"></param>
        /// <param name="typeProyect"></param>
        /// <param name="classes"></param>
        public void CreateProyect(string pathProyect, string nameProyect, string sizeImage, string typeProyect, string classes)
        {
            CreateProyect createProyect = new CreateProyect(pathProyect, nameProyect, sizeImage, typeProyect, classes);
           
            //Obtener los datos del proyecto
            this._Workspace = createProyect.PathProyect;
            this._PathImages = createProyect.PathImagesTrain;
            this._PathLabels = createProyect.PathLabelsTrain;
            this._FileYAML = createProyect.FileYAML;
            this._SizeImage = createProyect.SizeImage;
           
            //Cambiar tamaño de la imagen
            this._PictureBoxMain.Size = _StringToSize<System.Drawing.Size>(this._SizeImage);
            this._PictureBoxMain.Image = null;

        }
        /// <summary>
        /// Abrir un proyecto
        /// </summary>
        /// <param name="pathProyect"></param>
        public List<object> OpenProyect(string pathProyect)
        {
            string filePath = pathProyect + "\\data.yaml";
            //si el archivo no existe, retorna nulo
            if (!File.Exists(filePath))                   
                return null;

            //Leer el archivo YAML
            YamlConfigReader config = new YamlConfigReader(filePath);
            //Obtener los datos del proyecto
            this._Workspace = pathProyect;
            this._FileYAML = filePath;
            this._PathImages = pathProyect + "\\train\\images\\";
            this._PathLabels = pathProyect + "\\train\\labels\\";
            this._SizeImage = config.Size;
            //Cambiar tamaño de la imagen
            this._PictureBoxMain.Size = _StringToSize<System.Drawing.Size>(this._SizeImage);
            this._PictureBoxMain.Image = null;
            dataGridHelper.SetColors(config.Colors);


           

            //Actualizar el contenido del SfDataGrid
            dataGridHelper.Refresh(this._PathImages, this._PathLabels);
           
         
            //Retornar las clases del proyecto
            return config.GetObjectsFromList(config.Names);

        }
        /// <summary>
        /// Redimensionar imágenes
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="operation"></param>
        public async void ImportImages(string sourcePath, ImageOperation operation)
        {
            var resizer = new ImageManager(sourcePath);
            string destinationPath = this._PathImages;
            OpenCvSharp.Size targetSize = _StringToSize<OpenCvSharp.Size>(this._SizeImage);
            

            try
            {
                await resizer.ResizeImagesAsync(destinationPath, targetSize, operation);

                // Redimensionar la imagen
                //await processor.ProcessImageAsync(filePath, new Size(640, 480), destinationPath, ImageOperation.Resize);

                // Recortar la imagen
                //await processor.ProcessImageAsync(filePath, new Size(640, 480), destinationPath, ImageOperation.Crop);
                Console.WriteLine("¡Imágenes redimensionadas correctamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
          
            //// Redimensionar la imagen
            dataGridHelper.Refresh(this._PathImages, this._PathLabels);
        }

        #region Private Methods

        /// <summary>
        /// Convertir una cadena de tamaño a un objeto OpenCvSharp.Size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sizeString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// // Obtener un OpenCvSharp.Size
        //OpenCvSharp.Size sizeOpenCv = _StringToSize<OpenCvSharp.Size>("640x480");
        //
        // Obtener un System.Drawing.Size
        //System.Drawing.Size sizeDrawing = _StringToSize<System.Drawing.Size>("640x480");
        private T _StringToSize<T>(string sizeString) where T : struct
        {
            string[] dimensions = sizeString.Split('x');

            if (dimensions.Length != 2)
            {
                throw new ArgumentException("La cadena de tamaño debe tener el formato 'anchoxalto'");
            }

            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);

            // Conversión explícita al tipo T
            if (typeof(T) == typeof(OpenCvSharp.Size))
            {
                return (T)(object)new OpenCvSharp.Size(width, height);
            }
            else if (typeof(T) == typeof(System.Drawing.Size))
            {
                return (T)(object)new System.Drawing.Size(width, height);
            }
            else
            {
                throw new ArgumentException("Tipo T no soportado");
            }
        }

        /// <summary>
        /// Centrar el PictureBox en el panel de contenido
        /// </summary>
        private void _CenterPictureBox()
        {
            if (this._PanelContent.ClientRectangle.Width > 0 && this._PanelContent.ClientRectangle.Height > 0)
            {
                int x = (this._PanelContent.ClientRectangle.Width - this._PictureBoxMain.Width) / 2;
                int y = (this._PanelContent.ClientRectangle.Height - this._PictureBoxMain.Height) / 2;

                this._PictureBoxMain.Location = new Point(x, y);
            }
        }
        #endregion



    }
}

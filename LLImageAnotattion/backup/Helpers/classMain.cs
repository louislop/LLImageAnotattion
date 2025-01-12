using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace LLImageAnotattion.Helpers
{
    internal class ClassMain
    {
        private string _strPathProject; // Ruta del proyecto
        private string _strPathImage; // Ruta de la imagen
        private PictureBox _pictureBox;
        private Label _labelDimensions; // Etiqueta para mostrar dimensiones
        private FlowLayoutPanel _contentPanel; // Contenedor

        // _labelDimensions.Text = $"{selectedImage.Width} x {selectedImage.Height}";
        public ClassMain(FlowLayoutPanel content)
        {

            _strPathProject = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _contentPanel = content;

            // Inicializar y configurar el PictureBox
            _pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom
            };
            _pictureBox.BorderStyle = BorderStyle.FixedSingle;
            // Inicializar y configurar el Label
            _labelDimensions = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular)
              
            };

            // Agregar controles al FlowLayoutPanel
            _contentPanel.Controls.Add(_pictureBox);
            _contentPanel.Controls.Add(_labelDimensions);

            // Configurar alineación del FlowLayoutPanel
            _contentPanel.FlowDirection = FlowDirection.TopDown;
            _contentPanel.WrapContents = false;
            _contentPanel.AutoScroll = true;

            // Manejar el evento Resize del contenedor o formulario
            _contentPanel.Resize += (sender, e) => CenterControls();
        }
        /// <summary>
        /// Obtiene o establece la ruta del proyecto.
        /// </summary>
        public string PathProject
        {
            get => _strPathProject;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    // Si el valor es nulo o vacío, abre un diálogo para seleccionar una carpeta
                    using (var folderBrowserDialog = new FolderBrowserDialog())
                    {
                        folderBrowserDialog.Description = "Selecciona la carpeta del proyecto";
                        folderBrowserDialog.SelectedPath = _strPathProject; // Establece la ruta inicial

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            value = folderBrowserDialog.SelectedPath;
                        }
                        else
                        {
                            // El usuario canceló la selección
                            return;
                        }
                    }
                }

                // Valida la ruta del proyecto seleccionada
                if (!IsValidProjectPath(value))
                {
                    throw new ArgumentException("Ruta de proyecto inválida. Por favor, selecciona una carpeta válida.");
                }

                _strPathProject = value;
            }
        }
        /// <summary>
        /// Obtiene o establece la ruta de la imagen.
        /// </summary>
        public string PathImage
        {
            get => _strPathImage;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    // Crear un diálogo para abrir un archivo
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                        openFileDialog.Title = "Selecciona una imagen";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            value = openFileDialog.FileName;
                        }
                        else
                        {
                            return; // El usuario canceló la selección
                        }
                    }
                }

                // Validar la ruta de la imagen
                if (File.Exists(value))
                {
                    _strPathImage = value;
                    var selectedImage = Image.FromFile(_strPathImage);

                    // Cargar la imagen en el PictureBox
                    _pictureBox.Image = selectedImage;
                    _pictureBox.Width = selectedImage.Width;
                    _pictureBox.Height = selectedImage.Height;
                    //_pictureBox.Width = Math.Min(_contentPanel.Width, selectedImage.Width);
                    //_pictureBox.Height = Math.Min(_contentPanel.Height, selectedImage.Height);

                    // Actualizar el texto de la etiqueta
                    _labelDimensions.Text = $"{selectedImage.Width} x {selectedImage.Height}";
                }
                else
                {
                    throw new FileNotFoundException("El archivo de imagen especificado no existe.");
                }

                // Centrar los controles en el FlowLayoutPanel
                CenterControls();
            }
        }

        /// <summary>
        /// Valida si la ruta del proyecto es válida.
        /// Puedes personalizar esta lógica según tus necesidades.
        /// </summary>
        /// <param name="path">La ruta a validar.</param>
        /// <returns>True si la ruta es válida, False en caso contrario.</returns>
        private bool IsValidProjectPath(string path)
        {
            // Verifica si el directorio existe (puedes agregar más validaciones)
            return Directory.Exists(path);
        }
        /// <summary>
        /// Centra los controles dentro del FlowLayoutPanel.
        /// </summary>
        private void CenterControls()
        {
            // Obtener el tamaño del FlowLayoutPanel
            int panelWidth = _contentPanel.ClientSize.Width;
            int panelHeight = _contentPanel.ClientSize.Height;

            // Calcular el margen izquierdo y superior para centrar el PictureBox
            int marginLeft = Math.Max((panelWidth - _pictureBox.Width) / 2, 0);
            int marginTop = Math.Max((panelHeight - (_pictureBox.Height + _labelDimensions.Height)) / 2, 0);

            // Ajustar los márgenes del PictureBox
            _pictureBox.Margin = new Padding(marginLeft, marginTop, 0, 0);

            // Ajustar los márgenes del Label
            _labelDimensions.Margin = new Padding(marginLeft, 10, 0, 0);
        }
    }


}
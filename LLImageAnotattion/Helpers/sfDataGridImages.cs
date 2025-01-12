using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LLImageAnotattion.Helpers
{
    internal class sfDataGridImages
    {
        private int _CurrentCategory;
        private List<string> _Colors;
        private SfDataGrid _SfDataGrid;
        private PictureBox _PictureBox;
        private BoundingBoxManager _boundingBoxes;

        public ImageInfo ImageSelect;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pictureBox"></param>
        public sfDataGridImages(PictureBox pictureBox)
        {
            _PictureBox = pictureBox;
            _boundingBoxes = new BoundingBoxManager(_PictureBox, new List<string>());
        }
        /// <summary>
        /// Establece los colores de las categorías
        /// </summary>
        /// <param name="colors"></param>
        public void SetColors(List<string> colors)
        {
            _Colors = colors;
            _boundingBoxes.InitializeCategories(colors);
        }

        /// <summary>
        /// Establece la categoría actual
        /// </summary>
        /// <param name="category"></param>
        public void SetCurrentCategory(int category)
        {
            _CurrentCategory = category;
            _boundingBoxes.SetCurrentCategory(category);
        }

        /// <summary>
        /// Guarda las anotaciones
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveBoundingBoxes(string filePath)
        {
            _boundingBoxes.SaveBoundingBoxes(filePath);
        }

        /// <summary>
        /// Limpiar las anotaciones
        /// </summary>
        public void ClearBoundingBoxes()
        {
            _boundingBoxes.ClearBoundingBoxes();
        }

        /// <summary>
        /// Contar las bounding boxes
        /// </summary>
        /// <returns></returns>
        public int CountBoundingBoxes()
        {
            return _boundingBoxes.CountBoundingBoxes();
        }

        /// <summary>
        /// Obtiene el SfDataGrid
        /// </summary>
        /// <returns></returns>
        public SfDataGrid GetSfDataGrid()
        {
            

            _SfDataGrid = new SfDataGrid
            {
                AllowEditing = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                RowHeight = 150,
                AutoGenerateColumns = false,
                SelectionMode = GridSelectionMode.None,
                Width = 200
            };

            _SfDataGrid.Columns.Add(new GridTextColumn
            {
                MappingName = "Status",
                HeaderText = "",
                Width = 30,
                AllowSorting = false,
                AllowFiltering = true
            });

            _SfDataGrid.Columns.Add(new GridImageColumn
            {
                MappingName = "Image",
                HeaderText = "Imagen",
                Width = 150,
                ImageLayout = ImageLayout.Zoom
            });

            _SfDataGrid.CellClick += SfDataGrid_CellClick;
            _SfDataGrid.QueryCellStyle += SfDataGrid_QueryCellStyle;

            return _SfDataGrid;
        }

        /// <summary>
        /// Cambiar el color de la celda según el estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SfDataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.Column.MappingName == "Status")
            {
                e.Style.BackColor = e.DisplayText == "Si" ? ColorTranslator.FromHtml("#cdeb8d") : ColorTranslator.FromHtml("#f5b7b1");
                e.DisplayText = "";
            }
        }

        /// <summary>
        /// Cargar imagen al hacer clic en la celda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SfDataGrid_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.DataRow.RowType == RowType.DefaultRow && e.DataColumn.GridColumn.MappingName == "Image")
            {
                ImageSelect = e.DataRow.RowData as ImageInfo;
              
                if (ImageSelect != null && ImageSelect.Image != null)
                {
                   /* if (_boundingBoxes.HasBoundingBoxes())
                    {
                        var result = MessageBox.Show("¿Deseas guardar las anotaciones antes de cargar una nueva imagen?", "Guardar Anotaciones", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            _boundingBoxes.SaveBoundingBoxes(ImageSelect.FileLabel);
                            _boundingBoxes.ClearBoundingBoxes();
                        }
                        else if (result == DialogResult.No)
                        {
                            _boundingBoxes.ClearBoundingBoxes();
                        }
                        else
                        {
                            return; // Cancelar la carga de la nueva imagen
                        }
                    }*/

                    _boundingBoxes.ClearBoundingBoxes();
                    _boundingBoxes.LoadImage(ImageSelect.FileImage);
                    _boundingBoxes.LoadBoundingBoxes(ImageSelect.FileLabel); // Cargar bounding boxes si existen
                }
            }
        }

        public void Refresh(string pathImages, string pathLabels)
        {
            BindingList<ImageInfo> imageList = new BindingList<ImageInfo>();

            var jpgFiles = Directory.GetFiles(pathImages, "*.jpg");
            foreach (var file in jpgFiles)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                string txtFilePath = Path.Combine(pathLabels, fileNameWithoutExtension + ".txt");
                bool fileExists = File.Exists(txtFilePath);

                byte[] imageData = File.ReadAllBytes(file);

                imageList.Add(new ImageInfo
                {
                    Status = fileExists ? "Si" : "No",
                    Image = imageData,
                    FileLabel = txtFilePath,
                    FileImage = file
                });
            }

            _SfDataGrid.DataSource = null;
            _SfDataGrid.DataSource = imageList;
        }

        public ImageInfo GetSelectedImage()
        {
            return _SfDataGrid.SelectedItem as ImageInfo;
        }
    }

    public class ImageInfo
    {
        public string Status { get; set; }
        public byte[] Image { get; set; }
        public string FileLabel { get; set; }
        public string FileImage { get; set; }
    }
}
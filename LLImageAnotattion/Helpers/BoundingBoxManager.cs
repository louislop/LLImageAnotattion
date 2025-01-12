
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace LLImageAnotattion.Helpers
{
    public class BoundingBox
    {
        public Rect Box { get; set; }
        public int Category { get; set; }
        public Color Color { get; set; }
    }

    public class BoundingBoxManager
    {
        private List<BoundingBox> _boundingBoxes = new List<BoundingBox>();
        private Rect _currentBox;
        private bool _drawing = false;
        private Mat _image;
        private PictureBox _pictureBox;
        private Dictionary<int, Color> _categoryColors;
        private int _currentCategory = 0;

        /// <summary>
        /// Contar las bounding boxes
        /// </summary>
        /// <returns></returns>
        public int CountBoundingBoxes()
        {
            return _boundingBoxes.Count;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="colors"></param>
        public BoundingBoxManager(PictureBox pictureBox, List<string> colors)
        {
            _pictureBox = pictureBox;
            InitializeEvents();
            InitializeCategories(colors);
        }

        /// <summary>
        /// Inicializar eventos del PictureBox
        /// </summary>
        private void InitializeEvents()
        {
            _pictureBox.MouseDown += OnMouseDown;
            _pictureBox.MouseMove += OnMouseMove;
            _pictureBox.MouseUp += OnMouseUp;
            _pictureBox.MouseDoubleClick += OnMouseDoubleClick;
            _pictureBox.Paint += OnPaint;
        }

        /// <summary>
        /// Inicializar las categorías con los colores
        /// </summary>
        /// <param name="colors"></param>
        public void InitializeCategories(List<string> colors)
        {
            _categoryColors = new Dictionary<int, Color>();
            for (int i = 0; i < colors.Count; i++)
            {
                string colorHex = colors[i].Trim('"');
                Color color = ColorTranslator.FromHtml(colorHex);
                _categoryColors.Add(i, color);
            }
        }

        /// <summary>
        /// Establecer la categoría actual
        /// </summary>
        /// <param name="category"></param>
        public void SetCurrentCategory(int category)
        {
            if (_categoryColors.ContainsKey(category))
            {
                _currentCategory = category;
            }
        }

        /// <summary>
        /// Cargar imagen en el PictureBox
        /// </summary>
        /// <param name="imagePath"></param>
        public void LoadImage(string imagePath)
        {
            _image = Cv2.ImRead(imagePath);
            _pictureBox.Image = BitmapConverter.ToBitmap(_image);
            _pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            _pictureBox.Invalidate();
        }

        /// <summary>
        /// Verificar si hay bounding boxes
        /// </summary>
        /// <returns></returns>
        public bool HasBoundingBoxes()
        {
            return _boundingBoxes.Count > 0;
        }

        /// <summary>
        /// Guardar bounding boxes en un archivo
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveBoundingBoxes(string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (var bbox in _boundingBoxes)
                {
                    double centerX = (bbox.Box.X + bbox.Box.Width / 2.0) / _image.Width;
                    double centerY = (bbox.Box.Y + bbox.Box.Height / 2.0) / _image.Height;
                    double width = (double)bbox.Box.Width / _image.Width;
                    double height = (double)bbox.Box.Height / _image.Height;

                    file.WriteLine($"{bbox.Category} {centerX:F6} {centerY:F6} {width:F6} {height:F6}");
                }
            }
        }

        /// <summary>
        ///  Método para cargar las bounding boxes desde un archivo si existe
        /// Cargar bounding boxes desde un archivo
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadBoundingBoxes(string filePath)
        {
            if (!File.Exists(filePath)) return;

            _boundingBoxes.Clear();
            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var parts = line.Split(' ');
                    if (parts.Length != 5) continue;

                    int category = int.Parse(parts[0]);
                    double centerX = double.Parse(parts[1]);
                    double centerY = double.Parse(parts[2]);
                    double width = double.Parse(parts[3]);
                    double height = double.Parse(parts[4]);

                    int boxWidth = (int)(width * _image.Width);
                    int boxHeight = (int)(height * _image.Height);
                    int boxX = (int)(centerX * _image.Width - boxWidth / 2.0);
                    int boxY = (int)(centerY * _image.Height - boxHeight / 2.0);

                    _boundingBoxes.Add(new BoundingBox
                    {
                        Box = new Rect(boxX, boxY, boxWidth, boxHeight),
                        Category = category,
                        Color = _categoryColors[category]
                    });
                }
            }
            _pictureBox.Invalidate();
        }

        /// <summary>
        /// Limpiar todas las bounding boxes
        /// </summary>
        public void ClearBoundingBoxes()
        {
            _boundingBoxes.Clear();
            _pictureBox.Invalidate();
        }


        /// <summary>
        /// Dibujar bounding boxes en el PictureBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (_pictureBox.Image == null) return;

            using (var pen = new Pen(Color.Red, 2))
            {
                foreach (var boundingBox in _boundingBoxes)
                {
                    using (Pen boxPen = new Pen(boundingBox.Color, 2))
                    {
                        var scaledBox = ScaleBox(boundingBox.Box);
                        e.Graphics.DrawRectangle(boxPen, scaledBox.X, scaledBox.Y, scaledBox.Width, scaledBox.Height);
                    }
                }

                if (_drawing)
                {
                    var scaledBox = ScaleBox(_currentBox);
                    e.Graphics.DrawRectangle(pen, scaledBox.X, scaledBox.Y, scaledBox.Width, scaledBox.Height);
                }
            }
        }

        /// <summary>
        /// Escalar una bounding box a las dimensiones del PictureBox
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private Rectangle ScaleBox(Rect box)
        {
            float scaleX = (float)_pictureBox.Width / _image.Width;
            float scaleY = (float)_pictureBox.Height / _image.Height;
            float scale = Math.Min(scaleX, scaleY);

            int offsetX = (_pictureBox.Width - (int)(_image.Width * scale)) / 2;
            int offsetY = (_pictureBox.Height - (int)(_image.Height * scale)) / 2;

            return new Rectangle(
                (int)(box.X * scale) + offsetX,
                (int)(box.Y * scale) + offsetY,
                (int)(box.Width * scale),
                (int)(box.Height * scale)
            );
        }

        /// <summary>
        /// Escalar un punto de la imagen a las dimensiones del PictureBox
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private OpenCvSharp.Point UnscalePoint(System.Drawing.Point point)
        {
            if (_image == null )
                return new OpenCvSharp.Point(0, 0);

            float scaleX = (float)_pictureBox.Width / _image.Width;
            float scaleY = (float)_pictureBox.Height / _image.Height;
            float scale = Math.Min(scaleX, scaleY);

            int offsetX = (_pictureBox.Width - (int)(_image.Width * scale)) / 2;
            int offsetY = (_pictureBox.Height - (int)(_image.Height * scale)) / 2;

            return new OpenCvSharp.Point(
                (int)((point.X - offsetX) / scale),
                (int)((point.Y - offsetY) / scale)
            );
        }

        /// <summary>
        /// Al presionar el botón del mouse se inicia el dibujo de la bounding box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _drawing = true;
                var unscaledPoint = UnscalePoint(new System.Drawing.Point(e.X, e.Y));
                _currentBox = new Rect(unscaledPoint.X, unscaledPoint.Y, 0, 0);
            }
        }

        /// <summary>
        /// Mientras se arrastra el mouse se actualiza el tamaño de la bounding box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_drawing)
            {
                var unscaledPoint = UnscalePoint(new System.Drawing.Point(e.X, e.Y));
                _currentBox.Width = unscaledPoint.X - _currentBox.X;
                _currentBox.Height = unscaledPoint.Y - _currentBox.Y;
                _pictureBox.Invalidate();
            }
        }

        /// <summary>
        /// Al soltar el botón del mouse se agrega la bounding box a la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (_drawing)
            {
                _drawing = false;
                if (_currentBox.Width != 0 && _currentBox.Height != 0)
                {
                    if (_currentBox.Width < 0)
                    {
                        _currentBox.X += _currentBox.Width;
                        _currentBox.Width = Math.Abs(_currentBox.Width);
                    }
                    if (_currentBox.Height < 0)
                    {
                        _currentBox.Y += _currentBox.Height;
                        _currentBox.Height = Math.Abs(_currentBox.Height);
                    }

                    _boundingBoxes.Add(new BoundingBox
                    {
                        Box = _currentBox,
                        Category = _currentCategory,
                        Color = _categoryColors[_currentCategory]
                    });
                }
                _pictureBox.Invalidate();
            }

        }

        /// <summary>
        /// Eliminar bounding box al hacer doble clic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            var clickedPoint = UnscalePoint(new System.Drawing.Point(e.X, e.Y));

            // Buscar el bounding box que contiene el punto clicado
            BoundingBox boxToRemove = _boundingBoxes.FirstOrDefault(bbox =>
                clickedPoint.X >= bbox.Box.X &&
                clickedPoint.X <= bbox.Box.X + bbox.Box.Width &&
                clickedPoint.Y >= bbox.Box.Y &&
                clickedPoint.Y <= bbox.Box.Y + bbox.Box.Height
            );

            if (boxToRemove != null)
            {
                _boundingBoxes.Remove(boxToRemove);
                _pictureBox.Invalidate();
            }
        }
    }
}

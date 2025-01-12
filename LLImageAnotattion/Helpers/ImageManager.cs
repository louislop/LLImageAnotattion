using OpenCvSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic; // Required for Task.WaitAll

namespace LLImageAnotattion.Helpers
{
    internal class ImageManager
    {
        /// <summary>
        /// Ruta de origen
        /// </summary>
        private string _sourcePath=null;

        /// <summary>
        ///Contador de imágenes
        /// </summary>
        public int CountImages = 0;

        /// <summary>
        /// Contador de imágenes seleccionadas
        /// </summary>
        public int CountImagesSelectd = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourcePath"></param>
        public ImageManager(string sourcePath)
        {
            this._sourcePath = sourcePath;
        }

        /// <summary>
        /// Operaciones de imagen
        /// </summary>
        public enum ImageOperation
        {
            Resize,
            Crop,
            ResizeAndCrop
        }

        /// <summary>
        /// Redimensiona las imágenes en el directorio de origen y las guarda en el directorio de destino
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <param name="targetSize"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task ResizeImagesAsync(string destinationPath, Size targetSize, ImageOperation operation)
        {
            
            // Obtiene todos los archivos JPG en el directorio de origen
            var jpgFiles = Directory.GetFiles(this._sourcePath, "*.jpg");

            // Crea una lista de tareas para el procesamiento paralelo
            var tasks = new List<Task>();
            foreach (var file in jpgFiles)
            {
                this.CountImages++;
                tasks.Add(ProcessImageAsync(file, targetSize, destinationPath, operation));
                
            }

            // Espera a que todas las tareas terminen
            await Task.WhenAll(tasks.ToArray());
        }

        /// <summary>
        /// Procesa una imagen
        /// </summary>
        /// <param name="file"></param>
        /// <param name="targetSize"></param>
        /// <param name="destinationPath"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task ProcessImageAsync(string file, OpenCvSharp.Size targetSize, string destinationPath, ImageOperation operation)
        {
            using (var image = Cv2.ImRead(file))
            {
                if (image.Width >= targetSize.Width && image.Height >= targetSize.Height)
                {
                    Mat processedImage;

                    switch (operation)
                    {
                        case ImageOperation.Resize:
                            // Redimensionar la imagen al tamaño objetivo
                            processedImage = new Mat();
                            Cv2.Resize(image, processedImage, targetSize);
                            break;

                        case ImageOperation.Crop:
                            // Calcular las coordenadas del ROI (Región de Interés)
                            int x = (image.Width - targetSize.Width) / 2;
                            int y = (image.Height - targetSize.Height) / 2;
                            Rect roi = new Rect(x, y, targetSize.Width, targetSize.Height);

                            // Recortar la imagen directamente utilizando el ROI
                            processedImage = new Mat(image, roi);
                            break;

                        case ImageOperation.ResizeAndCrop:
                        
                            double aspectRatioOriginal = (double)image.Width / image.Height;
                            double aspectRatioTarget = (double)targetSize.Width / targetSize.Height;
                            OpenCvSharp.Size resizeSize;

                            if (aspectRatioOriginal > aspectRatioTarget)
                            {
                                resizeSize = new OpenCvSharp.Size(
                                    (int)(targetSize.Height * aspectRatioOriginal),
                                    targetSize.Height
                                );
                            }
                            else
                            {
                                resizeSize = new OpenCvSharp.Size(
                                    targetSize.Width,
                                    (int)(targetSize.Width / aspectRatioOriginal)
                                );
                            }

                            Mat resizedImage = new Mat();
                            Cv2.Resize(image, resizedImage, resizeSize);

                            int cropX = (resizeSize.Width - targetSize.Width) / 2;
                            int cropY = (resizeSize.Height - targetSize.Height) / 2;
                            Rect cropRoi = new Rect(cropX, cropY, targetSize.Width, targetSize.Height);
                            processedImage = new Mat(resizedImage, cropRoi);
                            break;

                        default:
                            throw new ArgumentException("Operación de imagen no válida.");
                    }

                    // Generar un nombre único para el archivo de destino
                    var uniqueFileName = Path.GetRandomFileName() + ".jpg";
                    var destinationFilePath = Path.Combine(destinationPath, uniqueFileName);

                    // Guardar la imagen procesada en el archivo de destino
                    Cv2.ImWrite(destinationFilePath, processedImage);
                }
                else
                {
                    throw new ArgumentException("El tamaño de la imagen original es menor que el tamaño objetivo.");
                }
            }
        }


    }
}

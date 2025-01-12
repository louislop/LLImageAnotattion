using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LLImageAnotattion.Helpers
{
    internal class CreateProyect
    {
        // <summary>
        /// ruta del archivo YAML
        /// </summary>
        public string FileYAML;
        /// <summary>
        /// Ruta del proyecto
        /// </summary>
        public string PathProyect;
        /// <summary>
        /// Ruta de las imágenes de entrenamiento y validación
        /// </summary>
        public string PathImagesTrain;
        /// <summary>
        /// Ruta de las imágenes de validación
        /// </summary>
        public string PathImagesValid;
        /// <summary>
        /// Ruta de las etiquetas de entrenamiento y validación
        /// </summary>
        public string PathLabelsTrain;
        /// <summary>
        /// Ruta de las etiquetas de validación
        /// </summary>
        public string PathLabelsValid;
        /// <summary>
        /// Nombre del proyecto
        /// </summary>
        private string NameProyect;
        /// <summary>
        /// Tamaño de las imágenes
        /// </summary>
        public string SizeImage;
        /// <summary>
        /// Tipo de proyecto
        /// </summary>
        private string TypeProyect;
        public CreateProyect(string pathProyect, string nameProyect, string sizeImage, string typeProyect, string classes)
        {
            this.SizeImage = sizeImage;
            this.NameProyect = nameProyect;
            this.TypeProyect = typeProyect;
            this.PathProyect = $"{pathProyect}\\{nameProyect}";
            this.FileYAML = this.PathProyect + "\\data.yaml";
            this._CreateDirStructure(this.PathProyect);
            this._CreateYAML(classes);
        }

        /// <summary>
        /// Crea el archivo YAML con la configuración del proyecto
        /// </summary>
        /// <param name="classes"></param>
        private void _CreateYAML(string classes)
        {
            //string configFilePath = this.PathProyect + "\\data.yaml";
            int lineCount = classes.Split('\n').Length - 1;

            // Updated regex to match the exact input format
            var nameColorRegex = new Regex(@"(\d+)\s*:\s*([^:]+):\s*(#[A-Fa-f0-9]{6})", RegexOptions.Multiline);

            var names = new List<string>();
            var colors = new List<string>();
            var vcolors = "";

            foreach (Match match in nameColorRegex.Matches(classes))
            {
                string name = match.Groups[2].Value.Trim();
                string color = match.Groups[3].Value;

                names.Add($"\'{name}\'");
                colors.Add($"\'{color}\'");
                vcolors += $"    {name}: {color}\n";
            }

            string configContent = $@"
train: ../train/images
val: ../valid/images
nc: {lineCount}
names: [{string.Join(", ", names)}]

LLImagenAnotattion:
    date: {DateTime.Now}
    project: {this.NameProyect}
    type: {this.TypeProyect}
    size: {this.SizeImage}
    colors: [{string.Join(", ", colors)}]
    path: {this.PathProyect}
    imagest: {this.PathImagesTrain}
    imagesv: {this.PathImagesValid}
    labelst: {this.PathLabelsTrain}
    labelsv: {this.PathLabelsValid}
{vcolors}";

            File.WriteAllText(this.FileYAML, configContent);
        }


        /// <summary>
        /// Crea la estructura de carpetas para el proyecto de anotación de imágenes
        /// </summary>
        /// <param name="pathProyect"></param>
        /// <param name="nameProyect"></param>
        private void _CreateDirStructure(string pathProyect)
        {
            string basePath = pathProyect;
            Directory.CreateDirectory(basePath);

            string trainPath = Path.Combine(basePath, "train");
            Directory.CreateDirectory(trainPath);

            string validPath = Path.Combine(basePath, "valid");
            Directory.CreateDirectory(validPath);

            string trainPathImages = Path.Combine(trainPath, "images");
            this.PathImagesTrain = trainPathImages;
            Directory.CreateDirectory(trainPathImages);

            string trainPathLabel = Path.Combine(trainPath, "labels");
            this.PathLabelsTrain = trainPathLabel;
            Directory.CreateDirectory(trainPathLabel);

            string validPathImages = Path.Combine(validPath, "images");
            this.PathImagesValid = validPathImages;
            Directory.CreateDirectory(validPathImages);

            string validPathLabel = Path.Combine(validPath, "labels");
            this.PathLabelsValid = validPathLabel;
            Directory.CreateDirectory(validPathLabel);
        }
    }
}


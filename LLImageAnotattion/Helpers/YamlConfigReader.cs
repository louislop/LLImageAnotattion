using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LLImageAnotattion.Helpers
{
    internal class YamlConfigReader
    {
        /// <summary>
        /// Ruta de entrenamiento
        /// </summary>
        public string TrainPath { get; set; }

        /// <summary>
        /// Ruta de entrenamiento
        /// </summary>
        public string ValPath { get; set; }

        /// <summary>         
        /// Número de clases
        /// </summary>
        public int Nc { get; set; }
        
        /// <summary>
        /// Nombres
        /// </summary>
        public List<string> Names { get; set; }
        
        /// <summary>
        /// Fecha
        /// </summary>
        public string Date { get; set; }
        
        /// <summary>
        /// Proyecto
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Tipo
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Tamaño
        /// </summary>
        public string Size { get; set; }
        
        /// <summary>
        /// Colores
        /// </summary>
        public List<string> Colors { get; set; }
        
        /// <summary>
        /// Ruta
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Imágenes de entrenamiento
        /// </summary>
        public string Imagest { get; set; }

        /// <summary>
        /// Imágenes de validación
        /// </summary>
        public string Imagesv { get; set; }

        /// <summary>
        /// Etiquetas de entrenamiento
        /// </summary>
        public string Labelst { get; set; }
        
        /// <summary>
        /// Etiquetas de validación
        /// </summary>
        public string Labelsv { get; set; }
        
        /// <summary>
        /// Clases y colores
        /// </summary>
        public Dictionary<string, string> ClassColors { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public YamlConfigReader(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Archivo de configuración no encontrado: {filePath}");
            }

            string content = File.ReadAllText(filePath);

            // Extraer valores clave-valor
            var keyValuePairs = content.Split(new[] { Environment.NewLine, "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(line => line.Split(':'))
                                        .Where(parts => parts.Length == 2)
                                        .Select(parts => new { Key = parts[0].Trim(), Value = parts[1].Trim() })
                                        .ToList();

            // Asignar valores a las propiedades
            foreach (var kvp in keyValuePairs)
            {
                switch (kvp.Key)
                {
                    case "train":
                        TrainPath = kvp.Value;
                        break;
                    case "val":
                        ValPath = kvp.Value;
                        break;
                    case "nc":
                        Nc = int.Parse(kvp.Value);
                        break;
                    case "names":
                        Names = ParseStringList(kvp.Value);
                        break;
                    case "date":
                        Date = kvp.Value;
                        break;
                    case "project":
                        Project = kvp.Value;
                        break;
                    case "type":
                        Type = kvp.Value;
                        break;
                    case "size":
                        Size = kvp.Value;
                        break;
                    case "colors":
                        Colors = ParseStringList(kvp.Value);
                        break;
                    case "path":
                        Path = kvp.Value;
                        break;
                    case "imagest":
                        Imagest = kvp.Value;
                        break;
                    case "imagesv":
                        Imagesv = kvp.Value;
                        break;
                    case "labelst":
                        Labelst = kvp.Value;
                        break;
                    case "labelsv":
                        Labelsv = kvp.Value;
                        break;
                    default:
                        // Asumir que son definiciones de clases y colores
                        if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                        {
                            if (ClassColors == null)
                            {
                                ClassColors = new Dictionary<string, string>();
                            }
                            ClassColors.Add(kvp.Key, kvp.Value);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Convierte una lista de cadenas en una lista de objetos
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<object> GetObjectsFromList(List<string> list)
        {
            return list.Cast<object>().ToList();
        }

        /// <summary>
        /// Convierte una cadena en una lista de cadenas
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private List<string> ParseStringList(string value)
        {
            if (value.StartsWith("[") && value.EndsWith("]"))
            {
                return value.Trim('[', ']')
                            .Split(',')
                            .Select(s => s.Trim().Trim('\'')) // Quitar comillas simples
                            .ToList();
            }
            return new List<string>();
        }
    }

}

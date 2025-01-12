using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLImageAnotattion.Helpers
{
    internal class classProyect
    {
        public static List<KeyValuePair<string, string>> ListSizesImages()
        {
            var returnData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("416x416", "416 x 416"), 
                new KeyValuePair<string, string>("420x420", "420 x 420"), 
                new KeyValuePair<string, string>("608x608", "608 x 608"),
                new KeyValuePair<string, string>("640x640", "640 x 640"),
                new KeyValuePair<string, string>("1280x1280", "1280 x 1280"),
            };         
            return returnData;
        }
        public static List<KeyValuePair<string, string>> ListTypeProyects()
        {
            var returnData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("yolo11", "YOLO 11"),         
            };
            return returnData;
        }
    }
}

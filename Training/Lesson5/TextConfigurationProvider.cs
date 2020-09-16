using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Training
{
    public class TextConfigurationProvider : ConfigurationProvider
    {
        public string FilePath { get; set; }

        public TextConfigurationProvider(string path)
        {
            FilePath = path;
        }

        public override void Load()
        {
            // Словарь в котором будут храниться считываемые данные
            var data = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
            // Считываем файл построчно до пустой строки
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
            {
                using (StreamReader textReader = new StreamReader(fileStream))
                {
                    string line;
                    while( (line = textReader.ReadLine()) != null)
                    {
                        string key = line.Trim();
                        string value = textReader.ReadLine();
                        data.Add(key, value);
                    }
                }
            }
            // Унаследованное свойство от ConfigurationProvider
            Data = data;
        }
    }
}

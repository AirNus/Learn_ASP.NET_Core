using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training
{
    public class TextConfigurationSource : IConfigurationSource
    {
        public string FilePath { get; private set; }

        public TextConfigurationSource(string fileName)
        {
            FilePath = fileName;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            // Получем полный путь до файла 
            string filePath = builder.GetFileProvider().GetFileInfo(FilePath).PhysicalPath;
            return new TextConfigurationProvider(filePath);
        }
    }
}

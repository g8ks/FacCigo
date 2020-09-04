using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FacCigo.Models
{
    public class ETLOption
    {
        public ETLOption(string filePath, char delimiter, bool hasHeader)
        {
            FilePath = filePath;
            Delimiter = delimiter;
            HasHeader = hasHeader;
        }

        public string FilePath { get; set; }
        public bool HasHeader { get; set; }
        public char Delimiter { get; set; }
       
    }
}

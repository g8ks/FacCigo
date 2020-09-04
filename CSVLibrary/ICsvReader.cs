using System;
using System.Collections.Generic;
using System.Text;

namespace CSVLibrary
{
    public interface ICsvReader<T>
    {
        IEnumerable<T> Read(string filePath, bool hasHeaders, char delimiter = ',');
    }
}

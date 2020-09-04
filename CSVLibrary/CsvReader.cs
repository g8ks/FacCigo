using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSVLibrary
{
    public class CsvReader<T> where T : CsvableBase, new()
    {
        public IEnumerable<T> Read(string filePath, bool hasHeaders,char delimiter=',')
        {
            var objects = new List<T>();
            using (var sr = new StreamReader(filePath))
            {
                bool headersRead = false;
                string line;
                do
                {
                    line = sr.ReadLine();

                    if (line != null && headersRead)
                    {
                        var obj = new T();
                        var propertyValues = line.Split(delimiter);
                        obj.AssignValuesFromCsv(propertyValues);
                        objects.Add(obj);
                    }
                    if (!headersRead)
                    {
                        headersRead = true;
                    }
                } while (line != null);
            }

            return objects;
        }
    }
}

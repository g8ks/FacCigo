﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CSVLibrary
{
    public abstract class CsvableBase
    {
  
        public virtual string ToCsv()
        {
            string output = "";

            var properties = GetType().GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType.IsSubclassOf(typeof(CsvableBase)))
                {
                    var m = properties[i].PropertyType
                            .GetMethod("ToCsv", new Type[0]);
                    output += m.Invoke(properties[i].GetValue(this),
                                        new object[0]);
                }
                else
                {
                    output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                }
                if (i != properties.Length - 1)
                {
                    output += ",";
                }
            }

            return output;
        }

        public virtual string ToCsv(string[] propertyNames, bool isIgnore)
        {
            string output = "";
            bool isFirstPropertyWritten = false;


            var properties = GetType().GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                if (isIgnore)
                {
                    if (!propertyNames.Contains(properties[i].Name))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }

                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(CsvableBase)))
                        {
                            var m = properties[i].PropertyType
                            .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }
                else
                {
                    if (propertyNames.Contains(properties[i].Name))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }

                        if (properties[i].PropertyType
                        .IsSubclassOf(typeof(CsvableBase)))
                        {
                            var m = properties[i].PropertyType
                                    .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }
            }

            return output;
        }

        public virtual string ToCsv(int[] propertyIndexes, bool isIgnore)
        {
            string output = "";

            bool isFirstPropertyWritten = false;

            var properties = GetType().GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                if (isIgnore)
                {
                    if (!propertyIndexes.Contains(i))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }

                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(CsvableBase)))
                        {
                            var m = properties[i].PropertyType
                                    .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }
                else
                {
                    if (propertyIndexes.Contains(i))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }

                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(CsvableBase)))
                        {
                            var m = properties[i].PropertyType
                                    .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }

            }

            return output;
        }

        public virtual void AssignValuesFromCsv(string[] propertyValues)
        {
            var properties = GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType
                    .IsSubclassOf(typeof(CsvableBase)))
                {
                    var instance = Activator.CreateInstance(properties[i].PropertyType);
                    var instanceProperties = instance.GetType().GetProperties();
                    var propertyList = new List<string>();

                    for (var j = 0; j < instanceProperties.Length; j++)
                    {
                        propertyList.Add(propertyValues[i + j]);
                    }
                    var m = instance.GetType().GetMethod("AssignValuesFromCsv", new Type[] { typeof(string[]) });
                    m.Invoke(instance, new object[] { propertyList.ToArray() });
                    properties[i].SetValue(this, instance);

                    i += instanceProperties.Length;
                }
                else
                {
                    var type = properties[i].PropertyType.Name;
                    switch (type)
                    {
                        case "Int32":
                            properties[i].SetValue(this,
                                            int.Parse(propertyValues[i]));
                            break;
                        case "Guid":
                            properties[i].SetValue(this,
                                            Guid.Parse(propertyValues[i]));
                            break;
                        case "double":
                            properties[i].SetValue(this,
                                            double.Parse(propertyValues[i]));
                            break;
                        case "decimal":
                            properties[i].SetValue(this,
                                            decimal.Parse(propertyValues[i]));
                            break;
                        case "float":
                            properties[i].SetValue(this,
                                            float.Parse(propertyValues[i]));
                            break;
                        default:
                            properties[i].SetValue(this, propertyValues[i]);
                            break;
                    }
                }
            }
        }

        private string PreProcess(string input)
    {
        input = input.Replace('ı', 'i')
            .Replace('ç', 'c')
            .Replace('ö', 'o')
            .Replace('ş', 's')
            .Replace('ü', 'u')
            .Replace('ğ', 'g')
            .Replace('İ', 'I')
            .Replace('Ç', 'C')
            .Replace('Ö', 'O')
            .Replace('Ş', 'S')
            .Replace('Ü', 'U')
            .Replace('Ğ', 'G')
            .Replace("\"", "\"\"")
            .Trim();
        if (input.Contains(","))
        {
            input = "\"" + input + "\"";
        }
        return input;
    }
    }
}

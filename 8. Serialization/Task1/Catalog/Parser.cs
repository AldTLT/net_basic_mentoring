using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CatalogLib
{
    public static class Parser
    {
        public static bool Serialize(Catalog catalog, string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Catalog));
                using var fs = new FileStream(path, FileMode.Create);
                serializer.Serialize(fs, catalog);

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static Catalog Deserialize(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Catalog));
                using var fs = new FileStream(path, FileMode.Open);

                return serializer.Deserialize(fs) as Catalog;
            }
            catch ( Exception)
            {
                return null;
            }
        }
    }
}

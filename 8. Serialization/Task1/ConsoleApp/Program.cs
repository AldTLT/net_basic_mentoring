using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CatalogLib;
using CatalogLib.Enums;
using CatalogLib.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var pathToXml = Directory.GetCurrentDirectory();
            var catalog = GetCatalog();

            // Serialize Catalog class to xml file.
            Parser.Serialize(catalog, Path.Combine(pathToXml, "Data\\serialized_books.xml"));

            // Deserialize book.xml to Catalog class.
            var result = Parser.Deserialize(Path.Combine(pathToXml, "Data\\books.xml"));
        }

        /// <summary>
        /// Generate Catalog for serialize to xml.
        /// </summary>
        /// <returns></returns>
        private static Catalog GetCatalog()
        {
            var catalog = new Catalog() { Date = new DateTime(2021, 3, 20), Books = new List<Book>() };

            for (var i = 1; i <= 10; i++)
            {
                var book = new Book()
                {
                    Author = $"Author{i}",
                    Description = $"Some description{i}",
                    Genre = Genre.Computer,
                    Id = i.ToString(),
                    Isbn = $"isbn{i}", 
                    PublishDate = DateTime.Now, 
                    Publisher = $"Publisher{i}", 
                    RegistrationDate = DateTime.Now, 
                    Title = $"Title{i}"
                };

                catalog.Books.Add(book);
            };

            return catalog;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ConsoleApp.Configuration
{
    public class FolderElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new FolderElement();
        protected override object GetElementKey(ConfigurationElement element) => ((FolderElement) element).Name;
        public ICollection<string> GetFolders() => BaseGetAllKeys().Select(c => c.ToString()).ToList();
    }
}

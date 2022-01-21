using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ConsoleApp.Configuration
{
    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("name")] public string Name => (string) this["name"];
    }
}

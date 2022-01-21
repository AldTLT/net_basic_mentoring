using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ConsoleApp.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("name")] public string Name => (string)this["name"];
        [ConfigurationProperty("pattern")] public string Pattern => (string) this["pattern"];
        [ConfigurationProperty("destinationFolder")] public string FolderName => (string)this["destinationFolder"];
        [ConfigurationProperty("addSequenceNumber")] public bool AddSequence => (bool)this["addSequenceNumber"];
        [ConfigurationProperty("addDate")] public bool AddDate => (bool)this["addDate"];
    }
}

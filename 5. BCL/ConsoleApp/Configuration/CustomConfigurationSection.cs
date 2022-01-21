using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;

namespace ConsoleApp.Configuration
{
    public class CustomConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("appName")] public string ApplicationName => (string) base["appName"];

        [ConfigurationProperty("culture")] public CultureElement culture => (CultureElement) this["culture"];

        [ConfigurationCollection(typeof(FolderElement))]
        [ConfigurationProperty("folders")]
        public FolderElementCollection Folders => (FolderElementCollection) this["folders"];

        [ConfigurationCollection(typeof(RuleElement))]
        [ConfigurationProperty("rules")]
        public RulesElementCollection Rules => (RulesElementCollection)this["rules"];

    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;

namespace ConsoleApp.Configuration
{
    public class CultureElement : ConfigurationElement
    {
        [ConfigurationProperty("cultureInfo")] public CultureInfo Culture => (CultureInfo) this["cultureInfo"];
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ConsoleApp.Configuration
{
    public class RulesElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new RuleElement();
        protected override object GetElementKey(ConfigurationElement e) => ((RuleElement)e).Name;
        public ICollection<RuleElement> GetRules() => BaseGetAllKeys().Select(e => (RuleElement)e).ToList();
    }
}

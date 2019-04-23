using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BCL.Configuration
{
	public class RuleElement : ConfigurationElement
	{
		[ConfigurationProperty("template", IsKey = true)]
		public String Template
		{
			get { return (String)this["template"]; }
		}

		[ConfigurationProperty("folder")]
		public String TargetFolder
		{
			get { return (String)this["folder"]; }
		}

		[ConfigurationProperty("serial")]
		public Boolean Serial
		{
			get { return (Boolean)this["serial"]; }
		}

		[ConfigurationProperty("date")]
		public Boolean Date
		{
			get { return (Boolean)this["date"]; }
		}
	}
}


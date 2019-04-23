using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BCL.Configuration
{
	public class CultureElement : ConfigurationElement
	{
		[ConfigurationProperty("culture", IsKey = true)]
		public String Culture
		{
			get { return (String)this["culture"]; }
		}
	}
}


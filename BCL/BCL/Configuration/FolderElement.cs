using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BCL.Configuration
{
	public class FolderElement : ConfigurationElement
	{
		[ConfigurationProperty("path", IsKey = true)]
		public String Path
		{
			get { return (String)this["path"]; }
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BCL.Configuration
{
	public class BCLConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("appName")]
		public string ApplicationName
		{
			get { return (string)base["appName"]; }
		}

		[ConfigurationProperty("rules")]
		public RuleElementCollection Rules
		{
			get { return (RuleElementCollection)this["rules"]; }
		}

		[ConfigurationProperty("defaultfolder")]
		public DefaultFolderElement DefaultFolder
		{
			get { return (DefaultFolderElement) this["defaultfolder"]; }
		}

		[ConfigurationProperty("culture")]
		public CultureElement Culture
		{
			get { return (CultureElement)this["culture"]; }
		}

		[ConfigurationProperty("folders")]
		public FolderElementCollection Folders
		{
			get { return (FolderElementCollection)this["folders"]; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace BCL.Configuration
{
	public class FolderElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new FolderElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FolderElement)element).Path;
		}

		public FolderElement this[int idx]
		{
			get
			{
				return (FolderElement)BaseGet(idx);
			}

			set
			{
				if (BaseGet(idx) != null)
					BaseRemoveAt(idx);

				BaseAdd(idx, value);
			}
		}
	}
}

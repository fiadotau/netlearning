using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace BCL.Configuration
{
	public class RuleElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new RuleElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RuleElement)element).Template;
		}

		public RuleElement this[int idx]
		{
			get
			{
				return (RuleElement)BaseGet(idx);
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

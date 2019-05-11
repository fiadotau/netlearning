using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attributes
{
	public class ExportAttribute : System.Attribute
	{
		public Type DataType { get; set; }

		public ExportAttribute() { }

		public ExportAttribute(Type type) {
			DataType = type;
		}
	}
}

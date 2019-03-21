using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ParserTask.Exception
{
	[Serializable()]
	public class NotNumericException : System.Exception
	{
		public NotNumericException() { }

		public NotNumericException(string message) : base(message) { }

		public NotNumericException(string message, System.Exception innerException) : base(message, innerException) { }

		public NotNumericException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}

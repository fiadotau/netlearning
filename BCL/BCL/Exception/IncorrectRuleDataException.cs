using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BCL.Exception
{
	class IncorrectRuleDataException : FormatException
	{
		public IncorrectRuleDataException()
		{
		}

		public IncorrectRuleDataException(string message) : base(message)
		{
		}

		public IncorrectRuleDataException(string message, System.Exception innerException) : base(message, innerException)
		{
		}

		protected IncorrectRuleDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

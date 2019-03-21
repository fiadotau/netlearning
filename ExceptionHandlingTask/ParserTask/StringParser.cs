using ParserTask.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserTask
{
	class StringParser
	{
		public static int StringParse(String str)
		{
			try
			{
				return Convert.ToInt32(str);
			}
			catch (FormatException e)
			{
				throw new NotNumericException(str + " is not a number. ", e);
			}
		}
	}
}

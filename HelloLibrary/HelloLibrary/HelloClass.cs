using System;

namespace HelloLibrary
{
	public class HelloClass
	{
		public static string sayHello(String name)
		{
			return DateTime.Now.ToString("h:mm:ss tt") + $" Hello, {name}!";
		}
	}
}

using System;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(HelloLibrary.HelloClass.sayHello(args[0]));
		}
	}
}

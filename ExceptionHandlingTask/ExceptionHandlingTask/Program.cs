using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandlingTask
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter text:");
			String input = "";
			string s = "1";
			while (!string.IsNullOrEmpty(s))
			{
				s = Console.ReadLine();
				input += s + "\n";
			}
			input = input.Trim();
			int ind = -1;
			try
			{
				do
				{
					Console.WriteLine(input[ind + 1]);
				} while ((ind = input.IndexOf("\n", ind + 1)) != -1);
			} catch(IndexOutOfRangeException e)
			{
				Console.WriteLine(e.StackTrace + ": out of range");
			} catch
			{
				Console.WriteLine("Other exc");
			}
		}
	}
}

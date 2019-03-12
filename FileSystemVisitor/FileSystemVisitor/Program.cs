using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
	class Program
	{
		public static void Main(string[] args)
		{
			FileSystemVisitor fsv = new FileSystemVisitor("C:\\Users\\yauheni_fiadotau\\source\\repos\\FileSystemVisitor");
			fsv.DirectoryFinded += ShowMessage;
			fsv.FileFinded += ShowMessage;
			fsv.FilteredDirectoryFinded += ShowMessage;
			fsv.FilteredFileFinded += ShowMessage;
			fsv.Finish += ShowMessage;
			fsv.Start += ShowMessage;
			foreach(string s in fsv)
			{
				Console.WriteLine(s);
			}
			//fsv.GetEnumerator().MoveNext();
			
			//Console.WriteLine(fsv.GetEnumerator().Current);
			
		}

		private static void ShowMessage(string message)
		{
			Console.WriteLine(message);
		}

	}
}

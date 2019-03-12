using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystemVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace FileSystemVisitor.Tests
{
	[TestClass()]
	public class GetFileSystemEntriesTest
	{

		[TestMethod()]
		public void TestWithoutAlgorithm()
		{
			FileSystemVisitor fsv = new FileSystemVisitor(Directory.GetCurrentDirectory());
			fsv.DirectoryFinded += ShowMessage;
			fsv.FileFinded += ShowMessage;
			fsv.FilteredDirectoryFinded += ShowMessage;
			fsv.FilteredFileFinded += ShowMessage;
			fsv.Finish += ShowMessage;
			fsv.Start += ShowMessage;
			foreach(string path in fsv)
			{
				Assert.IsNotNull(path);
				break;
			}
		}

		[TestMethod()]
		public void TestWithAlgorithm()
		{
			FileSystemVisitor fsv = new FileSystemVisitor(Directory.GetCurrentDirectory(), IsExeFile);
			fsv.DirectoryFinded += ShowMessage;
			fsv.FileFinded += ShowMessage;
			fsv.FilteredDirectoryFinded += ShowMessage;
			fsv.FilteredFileFinded += ShowMessage;
			fsv.Finish += ShowMessage;
			fsv.Start += ShowMessage;
			foreach (string path in fsv)
			{
				Assert.IsTrue(IsExeFile(path));
				break;
			}
		}

		[TestMethod()]
		public void TestWithStop()
		{
			FileSystemVisitor fsv = new FileSystemVisitor(Directory.GetCurrentDirectory(), IsExeFile);
			fsv.DirectoryFinded += ShowMessage;
			fsv.FileFinded += ShowMessage;
			fsv.FilteredDirectoryFinded += ShowMessage;
			fsv.FilteredFileFinded += ShowMessage;
			fsv.Finish += ShowMessage;
			fsv.Start += ShowMessage;
			fsv.Stop = true;
			foreach (string path in fsv)
			{
				Assert.IsNull(path);
				break;
			}
		}

		private void ShowMessage(string message)
		{
			Console.WriteLine(message);
		}

		private Boolean IsExeFile(String path)
		{
			Regex regex = new Regex(@"\w*.exe");
			return regex.IsMatch(path);
		}
	}
}
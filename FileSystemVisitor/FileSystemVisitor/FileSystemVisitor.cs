using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystemVisitor
{
	public class FileSystemVisitor
	{
		private object locker = new object();

		private Func<string, bool> _algorithm;

		public delegate void ShowMessage(string message);

		public event ShowMessage Start;

		public event ShowMessage Finish;

		public event ShowMessage FileFinded;

		public event ShowMessage DirectoryFinded;

		public event ShowMessage FilteredFileFinded;

		public event ShowMessage FilteredDirectoryFinded;
		
		public FileSystemVisitor(string path)
		{
			Path = path;
		}

		public FileSystemVisitor(string path, Func<string, Boolean> algorithm)
		{
			Path = path;
			_algorithm = algorithm;
		}

		public string[] Paths { get; set; }

		public string Path { get; set; }

		public bool Stop { get; set; }

		public bool Include { get; set; }

		public IEnumerator GetEnumerator()
		{
			Start("========== Started ==========");
			Paths = Directory.GetFileSystemEntries(Path, "*", SearchOption.AllDirectories);
			for (int i = 0; i < Paths.Length; i++)
			{
				lock (locker)
				{
					if (Stop)
					{
						break;
					}
				}

				if (_algorithm == null)
				{
					if (System.IO.File.Exists(Paths[i]))
					{
						FileFinded("FileFinded: " + Paths[i]);
					}
					else
					{
						DirectoryFinded("DirectoryFinded: " + Paths[i]);
					}
					yield return Paths[i];
				}
				else
				{
					lock (locker)
					{
						if (Include)
						{
							continue;
						}
					}
					if (_algorithm(Paths[i]))
					{
						if (System.IO.File.Exists(Paths[i]))
						{
							FilteredFileFinded("FilteredFileFinded: " + Paths[i]);
						}
						else
						{
							FilteredDirectoryFinded("FilteredDirectoryFinded: " + Paths[i]);
						}
						yield return Paths[i];
					}
					else
					{
						yield break;
					}
				}
			}
			Finish("========== Finished ==========");
		}

		public override bool Equals(object obj)
		{
			var visitor = obj as FileSystemVisitor;
			return visitor != null &&
				   EqualityComparer<Func<string, bool>>.Default.Equals(_algorithm, visitor._algorithm) &&
				   EqualityComparer<string[]>.Default.Equals(Paths, visitor.Paths) &&
				   Stop == visitor.Stop &&
				   Include == visitor.Include;
		}

		public override int GetHashCode()
		{
			var hashCode = 982068838;
			hashCode = hashCode * -1521134295 + EqualityComparer<Func<string, bool>>.Default.GetHashCode(_algorithm);
			hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(Paths);
			hashCode = hashCode * -1521134295 + Stop.GetHashCode();
			hashCode = hashCode * -1521134295 + Include.GetHashCode();
			return hashCode;
		}
	}
}

/*
 * 		[TestMethod()]
		public void GetFileSystemEntriesTest()
		{
			FileSystemVisitor fsv = new FileSystemVisitor();
			fsv.DirectoryFinded += ShowMessage;
			fsv.FileFinded += ShowMessage;
			fsv.FilteredDirectoryFinded += ShowMessage;
			fsv.FilteredFileFinded += ShowMessage;
			fsv.Finish += ShowMessage;
			fsv.Start += ShowMessage;
			fsv.GetFileSystemEntries(Directory.GetCurrentDirectory());
			Assert.Fail();
		}

		private void ShowMessage(string message)
		{
			Console.WriteLine(message);
		}
	}
*/
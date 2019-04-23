using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCL
{
	class Program
	{
		static void Main(string[] args)
		{
			Watcher watcher = new Watcher("bclSection");
			watcher.Run();

		}
	}
}

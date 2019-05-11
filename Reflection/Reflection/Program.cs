using CustomerLibrary;
using CustomerLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
	class Program
	{
		static void Main(string[] args)
		{
			Container сontainer = new Container();
			сontainer.AddType(typeof(CustomerDAL));
			сontainer.AddType(typeof(Logger));
			сontainer.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
			сontainer.AddAssembly(Assembly.GetExecutingAssembly());
			var customerBLL = (CustomerBLL)сontainer.CreateInstance(typeof(CustomerBLL));
			Console.WriteLine(customerBLL);
		}
	}
}

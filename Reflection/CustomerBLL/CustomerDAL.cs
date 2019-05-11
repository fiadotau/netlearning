using Attributes;
using CustomerLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLibrary
{
	[Export(typeof(ICustomerDAL))]
	public class CustomerDAL : ICustomerDAL
	{
    }
}

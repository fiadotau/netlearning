using Attributes;
using CustomerLibrary.Infrastructure;

namespace CustomerLibrary
{
	[Export]
	public class CustomerBLL
	{
		[Import]
		public ICustomerDAL CustomerDAL { get; set; }

		[Import]
		public Logger Logger { get; set; }
	}
}

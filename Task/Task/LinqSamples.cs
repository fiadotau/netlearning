// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;
using System.Xml;
using System.Globalization;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		//private static string filename = "Customers.xml";

		private static List<Customer> customers = new List<Customer>();

		[Category("Restriction Operators")]
		[Title("Total order - Task 1")]
		[Description("List all customers whose total turnover (the sum of all orders) exceeds a certain amounts 80, 110, 150.")]
		public void Linq1()
		{
			List<Customer> customers = dataSource.Customers;
			decimal[] totals = { 80, 110, 150 };
			var lowOrders = from lowOrder in
				(from customer in customers.ToArray()
				 from order in customer.Orders.ToArray()
				 from total in totals
				 where order.Total < total
				 orderby total
				 select new { order, total })
							group lowOrder.order by lowOrder.total;
			foreach (var lowOrderData in lowOrders)
			{
				Console.WriteLine("Total order less than:" + lowOrderData.Key);
				foreach (var lowOrderList in lowOrderData)
				{
					Console.WriteLine("Order ID: " + lowOrderList.OrderID + "; Order total: " + lowOrderList.Total + ".");
				}
			}
		}

		[Category("Restriction Operators")]
		[Title("List of suppliers - Task 2")]
		[Description("For each customer, make a list of suppliers located in the same country and in the same city. Do tasks using grouping and without.")]
		public void Linq2()
		{
			List<Customer> customers = dataSource.Customers;
			List<Supplier> suppliers = dataSource.Suppliers;
			Console.WriteLine("------------WITH GROUPING-------------");
			var listOfSuppliersWithGroping = from suppliersElement in
			  (from customer in customers.ToArray()
			   from supplier in suppliers.ToArray()
			   where customer.City.Equals(supplier.City, StringComparison.OrdinalIgnoreCase)
			   select new { customer, supplier })
											 group suppliersElement.supplier by suppliersElement.customer;

			foreach (var suppliersElement in listOfSuppliersWithGroping)
			{
				Console.WriteLine("Customer Name: " + suppliersElement.Key.CompanyName + "; Customer City: " + suppliersElement.Key.City + ".");
				Console.WriteLine("Suppliers: ");
				foreach (var suppliersData in suppliersElement)
				{
					Console.WriteLine("Supplier Name: " + suppliersData.SupplierName + "; Supplier City: " + suppliersData.City + ".");
				}
				Console.WriteLine();
			}

			Console.WriteLine("------------WITHOUT GROUPING-------------");

			var listOfSuppliersWithoutGroping =
			  from customer in customers.ToArray()
			  from supplier in suppliers.ToArray()
			  where customer.City.Equals(supplier.City, StringComparison.OrdinalIgnoreCase)
			  select
			  new
			  {
				  customer,
				  supplier = from supplier in suppliers.ToArray()
							 where customer.City.Equals(supplier.City, StringComparison.OrdinalIgnoreCase)
							 select supplier
			  };

			foreach (var suppliersElement in listOfSuppliersWithoutGroping)
			{
				Console.WriteLine("Customer Name: " + suppliersElement.customer.CompanyName + "; Customer City: " + suppliersElement.customer.City + ".");
				Console.WriteLine("Suppliers: ");
				foreach (var suppliersData in suppliersElement.supplier)
				{
					Console.WriteLine("Supplier Name: " + suppliersData.SupplierName + "; Supplier City: " + suppliersData.City + ".");
				}
				Console.WriteLine();
			}
		}

		[Category("Restriction Operators")]
		[Title("Total order - Task 3")]
		[Description("List all customers whose total turnover (the sum of all orders) exceeds a certain amount 110.")]
		public void Linq3()
		{
			decimal totalX = 110;
			List<Customer> customers = dataSource.Customers;

			var lowOrders =
				from customer in customers.ToArray()
				from order in customer.Orders.ToArray()
				where order.Total < totalX
				select order;
			Console.WriteLine("Total order less than " + totalX + ":");
			foreach (Order order in lowOrders)
			{
				Console.WriteLine("Order ID: " + order.Total + "; Order total: " + order.Total + ".");
			}
		}

		[Category("Restriction Operators")]
		[Title("List of customers - Task 4")]
		[Description("Issue a list of customers, indicating from which month of what year they became customers (to accept the month and year of the first order as such).")]
		public void Linq4()
		{
			List<Customer> customers = dataSource.Customers;
			foreach (Customer customer in customers)
			{
				Console.WriteLine("Customer Name: " + customer.CompanyName + "; Customer Date: " + customer.Orders.OrderBy(order => order.OrderDate).First().OrderDate + ".");
			}
		}

		[Category("Restriction Operators")]
		[Title("Sorted list of customers - Task 5")]
		[Description("Do the previous task, but give the list sorted by year, month, customer turnover (from maximum to minimum) and customer name.")]
		public void Linq5()
		{
			List<Customer> customers = dataSource.Customers;
			var listOfCustomers =
				from customer in customers.ToArray()
				orderby customer.CompanyName
				from order in customer.Orders.ToArray()
				orderby order.OrderDate
				orderby order.Total
				select customer;
			foreach (var customer in listOfCustomers)
			{
				Console.WriteLine("Customer Name: " + customer.CompanyName + "; Customer Date: " + customer.Orders.First().OrderDate + ".");
			}
		}

		[Category("Restriction Operators")]
		[Title("Incomplete data customers - Task 6")]
		[Description("Indicate all customers who have a non-digital postal code, or not a region or an operator code is not specified in the phone (for simplicity, we assume that this is equivalent to “no round brackets at the beginning”).")]
		public void Linq6()
		{
			List<Customer> customers = dataSource.Customers;
			var customersWithoutCorrectPostalCode =
				from customer in customers.ToArray()
				where customer.PostalCode != null && !customer.PostalCode.Trim().All(char.IsNumber)
				select customer;
			var customersWithoutCorrectPhone =
				from customer in customers.ToArray()
				where customer.Phone != null && !customer.Phone.Trim().StartsWith("(")
				select customer;
			var customersWithoutCorrectRegion =
				from customer in customers.ToArray()
				where customer.Region != null && customer.Region.Trim().Equals("")
				select customer;
			var listOfCustomers = customersWithoutCorrectPostalCode.Union(customersWithoutCorrectPhone).Union(customersWithoutCorrectRegion);
			foreach (var customer in listOfCustomers)
			{
				Console.WriteLine("Customer Name: " + customer.CompanyName + "; Customer PostalCode: " + customer.PostalCode + "; Customer Phone: " + customer.Phone + "; Customer Region: " + customer.Region + ".");
			}
		}

		[Category("Restriction Operators")]
		[Title("Product groups - Task 7")]
		[Description("Group all products into categories, inside - by stock availability, within the last group, sort by cost.")]
		public void Linq7()
		{
			List<Product> products = dataSource.Products;
			var productGroups = from product in products.ToArray()
								group product by product.Category into productElement
								select new
								{
									Category = productElement.Key,
									innerProductGroups = from productInner in productElement
														 orderby productInner.UnitPrice
														 group productInner by productInner.UnitsInStock into elementInner
														 select new
														 {
															 UnitsInStock = elementInner.Key,
															 elementInner
														 }
								};


			foreach (var product in productGroups)
			{
				Console.WriteLine("Product Category: " + product.Category + ".");
				foreach (var productInner in product.innerProductGroups)
				{
					Console.WriteLine("Product UnitsInStock: " + productInner.UnitsInStock);
					foreach (var productInnerGroup in productInner.elementInner)
					{
						Console.WriteLine("Product Name: " + productInnerGroup.ProductName + ".");
					}
					Console.WriteLine();

				}
				Console.WriteLine("---------------------------------------------");
			}
		}

		[Category("Restriction Operators")]
		[Title("Group of products by price - Task 8")]
		[Description("Group products into groups of \"cheap\", \"average price\", \"expensive\". Define the boundaries of each group")]
		public void Linq8()
		{
			List<Product> products = dataSource.Products;
			var cheapProducts =
				from product in products.ToArray()
				where product.UnitPrice < 30
				select product;
			var expensiveProducts =
				from product in products.ToArray()
				where product.UnitPrice > 70
				select product;
			var mediumProducts = products.Except(expensiveProducts).Except(cheapProducts);
			Console.WriteLine("Cheap products: cheaper than 30");
			foreach (var cheapProduct in cheapProducts)
			{
				Console.WriteLine("Product name: " + cheapProduct.ProductName + ", Product price: " + cheapProduct.UnitPrice + ".");
			}
			Console.WriteLine();
			Console.WriteLine("Medium products: between 30 and 70");
			foreach (var mediumProduct in mediumProducts)
			{
				Console.WriteLine("Product name: " + mediumProduct.ProductName + ", Product price: " + mediumProduct.UnitPrice + ".");
			}
			Console.WriteLine();
			Console.WriteLine("Expensive products: expensive than 70");
			foreach (var expensiveProduct in expensiveProducts)
			{
				Console.WriteLine("Product name: " + expensiveProduct.ProductName + ", Product price: " + expensiveProduct.UnitPrice + ".");
			}
		}

		[Category("Restriction Operators")]
		[Title("Intensity and city - Task 9")]
		[Description("Calculate the average profitability of each city (average order amount for all customers from this city) and average intensity (average number of orders per customer from each city)")]
		public void Linq9()
		{
			List<Customer> customers = dataSource.Customers;
			var averagePricesByCity =
				from customer in customers.ToArray()
				group customer by customer.City into groupByCity
				select
				new
				{
					groupByCity.Key,
					averagePrice = (from customer in groupByCity
									from order in customer.Orders.ToArray()
									select order.Total).Sum() / (from customer in groupByCity
																 select customer.Orders.Length).Sum()

				};
			foreach (var dataByCity in averagePricesByCity)
			{
				Console.WriteLine("City: " + dataByCity.Key + ", Average Price: " + dataByCity.averagePrice + ".");
			}
		}

		[Category("Restriction Operators")]
		[Title("Statistics - Task 10")]
		[Description("Make average annual activity statistics of customers by months (excluding the year), statistics by year, by year and month (that is, when one month in different years has its own value).")]
		public void Linq10()
		{
			List<Customer> customers = dataSource.Customers;
			var statisticsByMonth =
				from customer in customers.ToArray()
				from order in customer.Orders.ToArray()
				group order by order.OrderDate.Month into groupByMonth
				orderby groupByMonth.Key
				select
				new
				{
					month = groupByMonth.Key,
					activity = groupByMonth.Count()

				};
			Console.WriteLine("Statistic by month");
			foreach (var groupByMonth in statisticsByMonth)
			{
				Console.WriteLine("Month: " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(groupByMonth.month) + ", Activity: " + groupByMonth.activity + ".");
			}
			var statisticsByYear =
				from customer in customers.ToArray()
				from order in customer.Orders.ToArray()
				group order by order.OrderDate.Year into groupByYear
				orderby groupByYear.Key
				select
				new
				{
					year = groupByYear.Key,
					activity = groupByYear.Count()

				};
			Console.WriteLine();
			Console.WriteLine("Statistic by year");
			foreach (var groupByYear in statisticsByYear)
			{
				Console.WriteLine("Year: " + groupByYear.year + ", Activity: " + groupByYear.activity + ".");
			}
			var statisticsByYearAndMonth =
				from customer in customers.ToArray()
				from order in customer.Orders.ToArray()
				group order by order.OrderDate.Year into groupByYear
				orderby groupByYear.Key
				select
				new
				{
					year = groupByYear.Key,
					statisticsByMonth = from groupedOrder in groupByYear.ToArray()
								   group groupedOrder by groupedOrder.OrderDate.Month into groupByYearAndMonth
								   orderby groupByYearAndMonth.Key
								   select
								   new
								   {
									   month = groupByYearAndMonth.Key,
									   activity = groupByYearAndMonth.Count()
								   }

				};
			Console.WriteLine();
			Console.WriteLine("Statistic by month and year");
			foreach (var groupByYear in statisticsByYearAndMonth)
			{
				Console.WriteLine("Year: " + groupByYear.year + ".");
				foreach (var groupByYearAndMonth in groupByYear.statisticsByMonth)
				{
					Console.WriteLine("YearAnd Month: " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(groupByYearAndMonth.month) + ", Activity: " + groupByYearAndMonth.activity + ".");
				}
			}
		}

	}
}

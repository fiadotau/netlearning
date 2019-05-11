using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
	class Program
	{
		static void Main(string[] args)
		{
			string con = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
			MongoClient client = new MongoClient(con);
			var database = client.GetDatabase("test");
			IMongoCollection<Book> collection = database.GetCollection<Book>("books");
			Console.WriteLine("_________ввод данных_________");
			SaveDocsTaskAsync(collection).GetAwaiter().GetResult();
			Console.WriteLine("____________работа с количеством экземпляров больше единиц___________");
			FindMoreThanOneCopyTaskAsync(collection).GetAwaiter().GetResult();
			Console.WriteLine("____________книги с максимальным/минимальным количеством___________");
			FindBookWithMaxMinCopies(collection);
			Console.WriteLine("____________cписок авторов___________");
			ListOfAuthorTask(collection);
			Console.WriteLine("____________книги без автора___________");
			ListWithoutAuthorTask(collection);
			Console.WriteLine("_________увеличение числа копий_________");
			IncreaseNumberOfCopiesTaskAsync(collection).GetAwaiter().GetResult();
			Console.WriteLine("_________добавление жанра favority_________");
			AddFavorityGenreTaskAsync(collection).GetAwaiter().GetResult();
			Console.WriteLine("_________удаление всех элементов с менее 3 экзеплярами_________");
			DeleteCopiesLessThanThreeTaskAsync(collection).GetAwaiter().GetResult();
			Console.WriteLine("_________удаление всех элементов_________");
			DeleteAllBooksTaskAsync(collection).GetAwaiter().GetResult();
			Console.ReadLine();
		}

		private static async Task SaveDocsTaskAsync(IMongoCollection<Book> collection)
		{
			Book hobbit = new Book
			{
				Name = "Hobbit",
				Author = "Tolkien",
				Count = 5,
				Genre = new HashSet<string> { "fantasy" },
				Year = 2014
			};
			Book lordOfTheRings = new Book
			{
				Name = "Lord of the rings",
				Author = "Tolkien",
				Count = 3,
				Genre = new HashSet<string> { "fantasy" },
				Year = 2015
			};
			Book kolobok = new Book
			{
				Name = "Kolobok",
				Author = null,
				Count = 10,
				Genre = new HashSet<string> { "kids" },
				Year = 2000
			};
			Book repka = new Book
			{
				Name = "Repka",
				Author = null,
				Count = 11,
				Genre = new HashSet<string> { "kids" },
				Year = 2000
			};
			Book dyadyaStiopa = new Book
			{
				Name = "Dyadya Stiopa",
				Author = "Mihalkov",
				Count = 1,
				Genre = new HashSet<string> { "kids" },
				Year = 2001
			};
			await collection.InsertManyAsync(new[] { hobbit, lordOfTheRings, kolobok, repka, dyadyaStiopa });
			var documents = await collection.Find(_ => true).ToListAsync();
			foreach (var document in documents)
			{
				Console.WriteLine(document.ToJson());
			}
		}

		private static void ListOfAuthorTask(IMongoCollection<Book> collection)
		{
			var authors = collection.Aggregate().Match<Book>(x => x.Author != null).Group(new BsonDocument { { "_id", "$Author" } }).ToList();
			//var documents = await collection.Find(_ => true).ToListAsync();
			foreach (var author in authors)
			{
				Console.WriteLine(author.GetValue("_id"));
			}
		}

		private static void ListWithoutAuthorTask(IMongoCollection<Book> collection)
		{
			var documents = collection.Aggregate().Match<Book>(x => x.Author == null).ToList();
			foreach (var document in documents)
			{
				Console.WriteLine(document.ToJson());
			}
		}

		private static async Task FindMoreThanOneCopyTaskAsync(IMongoCollection<Book> collection)
		{
			var books = await collection.Find<Book>(x => true).ToListAsync();
			Console.WriteLine("Названия книг с количество экземпляров больше единицы: ");
			foreach (var book in books)
			{
				Console.WriteLine(book.Name);
			}
			books = await collection.Find<Book>(x => true).SortBy(x => x.Name).ToListAsync();
			Console.WriteLine("Отсортированный набор: ");
			foreach (var book in books)
			{
				Console.WriteLine(book.Name);
			}
			books = await collection.Find<Book>(x => true).SortBy(x => x.Name).Limit(3).ToListAsync();
			Console.WriteLine("Отлимитированный и отсортированный(3) набор:");
			foreach (var book in books)
			{
				Console.WriteLine(book.Name);
			}
			long amount = collection.Find<Book>(x => true).Limit(3).SortBy(x => x.Name).CountDocuments();
			Console.WriteLine("Количество подобных: " + amount);
		}

		private static async Task WriteCollectionAsync(IMongoCollection<Book> collection)
		{
			var documents = await collection.Find(_ => true).ToListAsync();
			foreach (var document in documents)
			{
				Console.WriteLine(document.ToJson());
			}
		}

		private static void FindBookWithMaxMinCopies(IMongoCollection<Book> collection)
		{
			Book book = collection.Find(_ => true).SortBy(x => x.Count).First();
			Console.WriteLine("Книга с макимальным количеством: \n" + book.ToJson());
			book = collection.Find(_ => true).SortByDescending(x => x.Count).First();
			Console.WriteLine("Книга с макимальным количеством: \n" + book.ToJson());
		}

		private static async Task DeleteAllBooksTaskAsync(IMongoCollection<Book> collection)
		{
			collection.DeleteMany(_ => true);
			var documents = await collection.Find(_ => true).ToListAsync();
			foreach (var document in documents)
			{
				Console.WriteLine(document.ToJson());
			}
		}

		private static async Task DeleteCopiesLessThanThreeTaskAsync(IMongoCollection<Book> collection)
		{
			collection.DeleteMany(x => x.Count < 3);
			var documents = await collection.Find(_ => true).ToListAsync();
			foreach (var document in documents)
			{
				Console.WriteLine(document.ToJson());
			}
		}

		private static async Task IncreaseNumberOfCopiesTaskAsync(IMongoCollection<Book> collection)
		{
			var update = Builders<Book>.Update.Inc(s => s.Count, 1);
			collection.UpdateMany(
				_ => true,
				update,
				new UpdateOptions { IsUpsert = false });
			var documents = await collection.Find(_ => true).ToListAsync();
			foreach (var document in documents)
			{
				Console.WriteLine(document.ToJson());
			}
		}

		private static async Task AddFavorityGenreTaskAsync(IMongoCollection<Book> collection)
		{
			var update = Builders<Book>.Update.AddToSet(x => x.Genre, "favority");
			collection.UpdateMany(
				x => x.Genre.Contains("fantasy"),
				update,
				new UpdateOptions { IsUpsert = false });
			var documents = await collection.Find(_ => true).ToListAsync();
			foreach (var document in documents)
			{
				Console.WriteLine(document.ToJson());
			}
		}
	}
}

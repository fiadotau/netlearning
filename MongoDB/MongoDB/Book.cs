using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
	class Book
	{
		[BsonId]
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		[BsonIgnoreIfDefault]
		public string Author { get; set; }
		public int Count { get; set; }
		public HashSet<string> Genre { get; set; }
		public int Year { get; set; }
	}
}

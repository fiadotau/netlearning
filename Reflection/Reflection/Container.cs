using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
	class Container
	{
		private List<Type> RegisteredTypes = new List<Type>();

		public void AddType(params Type[] types)
		{
			foreach (Type type in types)
			{
				RegisteredTypes.Add(type);
			}
		}

		public void AddAssembly(Assembly assembly)
		{
			Console.WriteLine(Path.GetDirectoryName(assembly.Location));
			var files = Directory.GetFiles(Path.GetDirectoryName(assembly.Location), "*.dll");

			foreach(var file in files)
			{
				var asm = Assembly.LoadFrom(file);
				List<Type> pluginTypes = asm.GetTypes().Where(x => x.IsClass).ToList();
				RegisteredTypes.AddRange(pluginTypes);
			}
		}

		public Object CreateInstance(Type type)
		{
			Object obj = Activator.CreateInstance(RegisteredTypes.SingleOrDefault(t => t.IsEquivalentTo(type)));
			if (obj != null)
			{
				RegisteredTypes.Remove(type);
			}
			return obj;
		}

		public T CreateInstance<T>() where T: class
		{
			T obj = (T)Activator.CreateInstance(RegisteredTypes.SingleOrDefault(t => t.IsEquivalentTo(typeof(T))));
			if (obj != null)
			{
				RegisteredTypes.Remove(typeof(T));
			}
			return obj;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteEntryFromList
{
	class Program
	{
		public static List<Entry> Lijstje = new List<Entry>();

		static void Main(string[] args)
		{
			Lijstje.Add(new Entry { Name = "Kees", City = "Meerkerk" });
			Lijstje.Add(new Entry { Name = "Marjolein", City = "Soesterberg" });
			Lijstje.Add(new Entry { Name = "Nel", City = "Meerkerk" });

			PrintLijstje();
			Console.WriteLine();

			string ToFind = "Meerkerk";

			Lijstje.RemoveAll(x => x.City == ToFind);
			Console.WriteLine();

			PrintLijstje();
			Console.WriteLine();
			Console.Write("Press any key... ");
			Console.ReadKey();
		}

		static void PrintLijstje()
		{
			foreach (var item in Lijstje)
			{
				Console.WriteLine("{0}, {1}", item.Name, item.City);
			}
		}
	}

	class Entry
	{
		public string Name { get; set; }
		public string City { get; set; }
	}
}

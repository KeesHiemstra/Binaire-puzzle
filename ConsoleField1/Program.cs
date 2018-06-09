using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleField1
{
	class Program
	{
		public static Puzzle BinairPuzzle = new Puzzle();

		static void Main(string[] args)
		{
			BinairPuzzle.SetFixesCell(1, 0, '0');
			BinairPuzzle.SetFixesCell(8, 0, '0');
			BinairPuzzle.SetFixesCell(9, 0, '0');
			BinairPuzzle.SetFixesCell(0, 1, '0');
			BinairPuzzle.SetFixesCell(9, 1, '0');
			BinairPuzzle.SetFixesCell(1, 2, '1');
			BinairPuzzle.SetFixesCell(4, 2, '1');
			BinairPuzzle.SetFixesCell(2, 3, '0');
			BinairPuzzle.SetFixesCell(2, 4, '0');
			BinairPuzzle.SetFixesCell(5, 4, '0');
			BinairPuzzle.SetFixesCell(8, 4, '1');
			BinairPuzzle.SetFixesCell(9, 4, '1');
			BinairPuzzle.SetFixesCell(4, 5, '0');
			BinairPuzzle.SetFixesCell(7, 5, '0');
			BinairPuzzle.SetFixesCell(8, 5, '0');
			BinairPuzzle.SetFixesCell(3, 7, '0');
			BinairPuzzle.SetFixesCell(6, 7, '1');
			BinairPuzzle.SetFixesCell(7, 7, '1');
			BinairPuzzle.SetFixesCell(1, 8, '1');
			BinairPuzzle.SetFixesCell(2, 8, '1');
			BinairPuzzle.SetFixesCell(0, 9, '0');
			BinairPuzzle.SetFixesCell(1, 9, '0');
			BinairPuzzle.SetFixesCell(5, 9, '1');
			BinairPuzzle.SetFixesCell(7, 9, '0');
			BinairPuzzle.SetFixesCell(9, 9, '1');

			WritePuzzle();

			Console.Write("\nPress a key");
			Console.ReadKey();

			while (true)
			{
				bool result = (BinairPuzzle.CheckField());
				WritePuzzle();
				Console.WriteLine();
				Console.WriteLine(result);

				Console.Write("\nPress a key");
				Console.ReadKey();
			}
		}

		public static void WritePuzzle()
		{
			Console.Clear();
			Console.WriteLine("  | 0 1 2 3 4 5 6 7 8 9");
			Console.WriteLine("--+--------------------");
			for (int row = 0; row < 10; row++)
			{
				Console.Write($"{row} | ");
				for (int col = 0; col < 10; col++)
				{
					Console.Write(BinairPuzzle.GetCell(col, row).Content);
					Console.Write(' ');
				}
				Console.WriteLine();
			}
		}
	}
}

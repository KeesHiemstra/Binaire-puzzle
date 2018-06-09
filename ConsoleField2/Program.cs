using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleField2
{
	class Program
	{
		public enum PuzzleState { None, Fixed, Rules, Learn }
		public static Field Puzzle = new Field();
		public static int Left = 0;
		public static int Top = 0;

		[STAThread]
		static void Main(string[] args)
		{
			Console.Title = "Binaire puzzle";
			ConsoleKeyInfo Key = new ConsoleKeyInfo();
			PuzzleState CurrentStatus = PuzzleState.None;
			int Col = 0;
			int Row = 0;


			ShowPuzzle();

			do
			{
				Key = Console.ReadKey(true);

				string Path;
				Cell cell;
				switch (Key.Key)
				{
					case ConsoleKey.D0:
						cell = Puzzle.SetCell(Col, Row, '0', Cell.CellSource.pre);
						ShowCell(Col, Row, cell);
						break;
					case ConsoleKey.D1:
						cell = Puzzle.SetCell(Col, Row, '1', Cell.CellSource.pre);
						ShowCell(Col, Row, cell);
						break;
					case ConsoleKey.Delete:
						cell = Puzzle.SetCell(Col, Row, ' ', Cell.CellSource.empty);
						ShowCell(Col, Row, cell);
						Puzzle.RemoveFromHistory(Col, Row);
						break;
					case ConsoleKey.F:
						CurrentStatus = PuzzleState.Fixed;
						break;
					case ConsoleKey.R:
						CurrentStatus = PuzzleState.Rules;
						break;
					case ConsoleKey.L:
						CurrentStatus = PuzzleState.Learn;
						break;
					case ConsoleKey.E:
						#region Export history
						Path = string.Format("C:\\Temp\\Binair prefilled {0}.json", DateTime.Now.ToString("yyyy-MM-dd HHmm"));

						SaveFileDialog saveFile = new SaveFileDialog
						{
							FileName = Path,
							Filter = "Json files (*.json)|*.json",
							FilterIndex = 0,
						};

						if (saveFile.ShowDialog() == DialogResult.OK)
						{
							Path = saveFile.FileName;
						}

						Puzzle.ExportPreFilledCells(Path);
						break;
					#endregion
					case ConsoleKey.I:
						#region Import history
						OpenFileDialog openFile = new OpenFileDialog
						{

						};

						if (openFile.ShowDialog() == DialogResult.OK)
						{
							if (Puzzle.ImportPreFilledCells(openFile.FileName))
							{
								ShowField();
							}
						}

						break;
					#endregion
					case ConsoleKey.Enter:
						#region Execute rules
						if (Puzzle.CheckField())
						{
							for (int row = 0; row < 10; row++)
							{
								for (int col = 0; col < 10; col++)
								{
									ShowCell(col, row, Puzzle.GetCell(col, row));
								}
							}
						}
						break;
					#endregion
					case ConsoleKey.LeftArrow:
						if (Col > 0)
						{
							Col--;
						}
						break;
					case ConsoleKey.UpArrow:
						if (Row > 0)
						{
							Row--;
						}
						break;
					case ConsoleKey.RightArrow:
						if (Col < 9)
						{
							Col++;
						}
						break;
					case ConsoleKey.DownArrow:
						if (Row < 9)
						{
							Row++;
						}
						break;
					default:
						Console.SetCursorPosition(0, Console.WindowHeight - 1);
						Console.Write("Key: {0}                    ", Key.Key);
						break;
				}
				Console.SetCursorPosition(0, Console.WindowHeight - 2);
				Console.Write("{0} status", CurrentStatus);
				Console.SetCursorPosition(Left + Col * 2, Top + Row);
			} while (Key.Key != ConsoleKey.Q);
		}//Main

		static void ShowCell(int col, int row, Cell cell)
		{
			Console.SetCursorPosition(Left + col * 2, Top + row);
			switch (cell.Source)
			{
				case Cell.CellSource.empty:
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write(".");
					break;
				case Cell.CellSource.pre:
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write(cell.Content);
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case Cell.CellSource.rule:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(cell.Content);
					Console.ForegroundColor = ConsoleColor.White;
					break;
				default:
					break;
			}
		}

		static void ShowField()
		{
			Cell cell;

			for (int row = 0; row < 10; row++)
			{
				for (int col = 0; col < 10; col++)
				{
					cell = Puzzle.GetCell(col, row);
					ShowCell(col, row, cell);
				}
			}
			Console.SetCursorPosition(Left, Top);
		}

		static void ShowPuzzle()
		{
			Console.Clear();
			Console.WriteLine("   | 0 1 2 3 4 5 6 7 8 9");
			Console.WriteLine(" ========================");
			for (int i = 0; i < 10; i++)
			{
				Console.Write(" {0} | ", i);
				if (Left == 0 && Top == 0)
				{
					Left = Console.CursorLeft;
					Top = Console.CursorTop;
				}
				Console.WriteLine(". . . . . . . . . .", i);
			}

			ShowField();
		}

		static Field NewPuzzle(Field puzzle)
		{
			if (puzzle != null)
			{
				puzzle = null;
			}
			puzzle = new Field();
			return puzzle;
		}
	}
}

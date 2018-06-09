using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleField1.Tripple;

namespace ConsoleField1
{
	class Puzzle
	{
		public static Cell[] Field = new Cell[100];

		#region Initialize
		/// <summary>
		/// Initialize puzzle with empty cells
		/// </summary>
		public Puzzle()
		{
			for (int i = 0; i < Field.Length; i++)
			{
				Field[i] = new Cell() { Content = ' ', Source = Source.empty };
			}
		}//Puzzle
		#endregion

		#region Index
		/// <summary>
		/// Translate column and row to a cell
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		internal int Index(int col, int row)
		{
			return col + row * 10;
		}
		#endregion

		#region GetCell
		/// <summary>
		/// Get cell from column and row
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		public Cell GetCell(int col, int row)
		{
			return Field[Index(col, row)];
		}
		#endregion

		#region GetTripple
		public Tripple GetTripple(int col, int row, Direction direction)
		{
			Tripple tripple = new Tripple();

			if (direction == Direction.horizontal)
			{
				if (col >= 8)
				{
					return null;
				}
				for (int i = 0; i < 3; i++)
				{
					tripple.Examine[i] = GetCell(col + i, row);
				}
			}
			else
			{
				if (row >= 8)
				{
					return null;
				}
				for (int i = 0; i < 3; i++)
				{
					tripple.Examine[i] = GetCell(col, row + i);
				}
			}
			return tripple;
		}
		#endregion

		#region SetTrippleCell
		public void SetTrippleCell(int col, int row, Direction direction,
			Byte number, char content, Source source)
		{
			Tripple tripple = GetTripple(col, row, direction);
			tripple.Examine[number].Content = content;
			tripple.Examine[number].Source = source;
		}
		#endregion

		#region CheckTripple
		public bool CheckTripple(int col, int row, Direction direction)
		{
			Tripple tripple = GetTripple(col, row, direction);
			if (tripple == null)
			{
				return false;
			}
			return tripple.CountCellContent();
		}
		#endregion

		#region CheckDirection
		public bool CheckDirection(byte position, Direction direction)
		{
			bool Result = false;
			Cell cell = new Cell();
			byte OpenPosition = 10;
			byte Empty = 0;
			byte Zero = 0;
			byte One = 0;

			for (byte i = 0; i < 10; i++)
			{
				switch (direction)
				{
					case Direction.horizontal:
						cell = GetCell(i, position);
						break;
					case Direction.vertical:
						cell = GetCell(position, i);
						break;
					default:
						break;
				}

				switch (cell.Content)
				{
					case '0':
						Zero++;
						break;
					case '1':
						One++;
						break;
					default:
						Empty++;
						OpenPosition = i;
						break;
				}
			}

			if (Empty == 1)
			{
				if (Zero == 4 && One == 5)
				{
					switch (direction)
					{
						case Direction.horizontal:
							SetCell(OpenPosition, position, '0', Source.rulefilled);
							break;
						case Direction.vertical:
							SetCell(position, OpenPosition, '0', Source.rulefilled);
							break;
					}
				}
				else if (Zero == 5 && One == 4)
				{
					switch (direction)
					{
						case Direction.horizontal:
							SetCell(OpenPosition, position, '1', Source.rulefilled);
							break;
						case Direction.vertical:
							SetCell(position, OpenPosition, '1', Source.rulefilled);
							break;
					}
				}
				Result = true;
			}

			return Result;
		}
		#endregion

		#region CheckField
		public bool CheckField()
		{
			bool Result = false;
			for (int row = 0; row < 10; row++)
			{
				for (int col = 0; col < 10; col++)
				{
					bool result = CheckTripple(col, row, Direction.horizontal);
					Result = Result || result;
					result = CheckTripple(col, row, Direction.vertical);
					Result = Result || result;
				}
			}

			for (byte i = 0; i < 10; i++)
			{
				bool result = CheckDirection(i, Direction.horizontal);
				Result = Result || result;
				result = CheckDirection(i, Direction.vertical);
				Result = Result || result;
			}
			return Result;
		}
		#endregion

		#region SetCell
		public void SetCell(int col, int row, char content, Source source)
		{
			Field[Index(col, row)].Content = content;
			Field[Index(col, row)].Source = source;
		}
		#endregion

		public void SetFixesCell(int col, int row, char content)
		{
			SetCell(col, row, content, Source.prefilled);
		}
	}
}

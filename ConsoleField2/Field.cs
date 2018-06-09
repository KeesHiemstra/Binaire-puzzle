using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static ConsoleField2.Tripple;

namespace ConsoleField2
{
	class Field
	{
		public static Cell[] Play = new Cell[100];
		public static List<HistoryCell> History = new List<HistoryCell>();

		#region Initialize Field
		/// <summary>
		/// Initialize puzzle with empty cells
		/// </summary>
		public Field()
		{
			ClearField();
		}//Field
		#endregion

		#region ClearField
		/// <summary>
		/// Clear all cell in play field
		/// </summary>
		public void ClearField()
		{
			for (int i = 0; i < Play.Length; i++)
			{
				Play[i] = new Cell() { Content = ' ', Source = Cell.CellSource.empty };
			}
		}
		#endregion

		//#region ClearHistory
		//public void ClearHistory()
		//{
		//	History.Clear();
		//}
		//#endregion

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
			return Play[Index(col, row)];
		}
		#endregion

		#region GetTripple
		/// <summary>
		/// Get tripple from the field
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public Tripple GetTripple(int col, int row, TrippleDirection direction)
		{
			Tripple tripple = new Tripple();

			if (direction == TrippleDirection.horizontal)
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
		/// <summary>
		/// Update the tripple in the field
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		/// <param name="direction"></param>
		/// <param name="number"></param>
		/// <param name="content"></param>
		/// <param name="source"></param>
		public void SetTrippleCell(int col, int row, TrippleDirection direction,
			Byte number, char content, Cell.CellSource source)
		{
			Tripple tripple = GetTripple(col, row, direction);
			tripple.Examine[number].Content = content;
			tripple.Examine[number].Source = source;
		}
		#endregion

		#region CheckTripple
		public bool CheckTripple(int col, int row, TrippleDirection direction)
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
		/// <summary>
		/// Check the column or row
		/// </summary>
		/// <param name="position"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public bool CheckDirection(byte position, TrippleDirection direction)
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
					case TrippleDirection.horizontal:
						cell = GetCell(i, position);
						break;
					case TrippleDirection.vertical:
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
						case TrippleDirection.horizontal:
							SetCell(OpenPosition, position, '0', Cell.CellSource.rule);
							break;
						case TrippleDirection.vertical:
							SetCell(position, OpenPosition, '0', Cell.CellSource.rule);
							break;
					}
				}
				else if (Zero == 5 && One == 4)
				{
					switch (direction)
					{
						case TrippleDirection.horizontal:
							SetCell(OpenPosition, position, '1', Cell.CellSource.rule);
							break;
						case TrippleDirection.vertical:
							SetCell(position, OpenPosition, '1', Cell.CellSource.rule);
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
					bool result = CheckTripple(col, row, TrippleDirection.horizontal);
					Result = Result || result;
					result = CheckTripple(col, row, TrippleDirection.vertical);
					Result = Result || result;
				}
			}

			for (byte i = 0; i < 10; i++)
			{
				bool result = CheckDirection(i, TrippleDirection.horizontal);
				Result = Result || result;
				result = CheckDirection(i, TrippleDirection.vertical);
				Result = Result || result;
			}
			return Result;
		}
		#endregion

		#region SetCell
		/// <summary>
		/// Update the cell and save the update
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		/// <param name="content"></param>
		/// <param name="source"></param>
		public Cell SetCell(int col, int row, char content, Cell.CellSource source)
		{
			HistoryCell Update = new HistoryCell
			{
				Col = col,
				Row = row,
				Content = content,
				Source = source
			};

			int index = Index(col, row);
			Play[index].Content = content;
			Play[index].Source = source;

			History.Add(Update);

			return Update;
		}
		#endregion

		#region RemoveFromHistory
		public void RemoveFromHistory(int col, int row)
		{
			History.RemoveAll(x => x.Col == col && x.Row == row);
		}
		#endregion

		#region ExportPreFilledCells
		/// <summary>
		/// Save history to json file
		/// </summary>
		/// <param name="path"></param>
		public void ExportPreFilledCells(string path)
		{
			using (FileStream Json = File.Open(path, FileMode.OpenOrCreate))
			{
				DataContractJsonSerializer Export = new DataContractJsonSerializer(typeof(List<HistoryCell>));
				Export.WriteObject(Json, History);
			}
		}
		#endregion

		#region ImportPreFilledCells
		public bool ImportPreFilledCells(string path)
		{
			bool Result = false;
			try
			{
				using (FileStream JSon = File.Open(path, FileMode.Open))
				{
					DataContractJsonSerializer Import = new DataContractJsonSerializer(typeof(List<HistoryCell>));
					List<HistoryCell> TempHistory = new List<HistoryCell>();
					TempHistory = (List<HistoryCell>)Import.ReadObject(JSon);

					History.Clear();
					var PreFilled = from x in TempHistory
													where x.Source == Cell.CellSource.pre
													select x;

					ClearField();
					foreach (HistoryCell item in PreFilled)
					{
						SetCell(item.Col, item.Row, item.Content, item.Source);
					}

					Import = null;
					TempHistory = null;
					Result = true;
				}
			}
			catch (Exception)
			{
			}
			return Result;
		}
		#endregion

		public void SetCellWithFixedValue(int col, int row, char content)
		{
			SetCell(col, row, content, Cell.CellSource.pre);
		}
	}
}


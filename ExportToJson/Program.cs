using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace ExportToJson
{
	class Program
	{
		static List<CellHistory> History = new List<CellHistory>();

		[STAThread]
		static void Main(string[] args)
		{
			
			History.Add(new CellHistory { Col = 1, Row = 1, Content = '1', Source = Cell.CellSource.pre });
			History.Add(new CellHistory { Col = 2, Row = 1, Content = '1', Source = Cell.CellSource.pre });

			string Path = string.Format("C:\\Temp\\Binair prefilled {0}.json", DateTime.Now.ToString("yyyy-MM-dd HHmm"));

			SaveFileDialog saveFile = new SaveFileDialog
			{
				FileName = Path,
				Filter = "Json files (*.json)|*.json",
				FilterIndex = 0,
				//RestoreDirectory = true
			};

			if (saveFile.ShowDialog() == DialogResult.OK)
			{
				Path = saveFile.FileName;
			}

			using (FileStream Json = File.Open(Path, FileMode.OpenOrCreate))
			{
				DataContractJsonSerializer Export = new DataContractJsonSerializer(typeof(List<CellHistory>));
				Export.WriteObject(Json, History);
			}

			Console.WriteLine("Press any key... ");
			Console.ReadKey();
		}
	}


	[DataContract]
	class Cell
	{
		public enum CellSource { empty, pre, rule }

		[DataMember]
		public char Content { get; set; }
		[DataMember]
		public CellSource Source { get; set; }
	}

	[DataContract]
	class CellHistory : Cell
	{
		[DataMember]
		public int Col { get; set; }
		[DataMember]
		public int Row { get; set; }
	}
}

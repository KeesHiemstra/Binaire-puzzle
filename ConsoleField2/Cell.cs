using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleField2
{
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
	class HistoryCell : Cell
	{
		[DataMember]
		public int Col { get; set; }

		[DataMember]
		public int Row { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleField1
{
	class Tripple
	{
		public Cell[] Examine = new Cell[3];
		public byte Empty { get; private set; }
		public byte Zero { get; private set; }
		public byte One { get; private set; }
		public bool IsSame { get; private set; }
		public char SameContent { get; set; }

		public enum Direction { horizontal, vertical }

		public bool CountCellContent()
		{
			bool result = false;
			char FirstContent = ' ';
			byte EmptyCell = 3;
			Empty = 0;
			Zero = 0;
			One = 0;

			for (byte i = 0; i < 3; i++)
			{
				switch (Examine[i].Content)
				{
					case '0':
						Zero++;
						if (FirstContent == ' ')
						{
							FirstContent = '0';
						}
						else
						{
							IsSame = FirstContent == Examine[i].Content;
						}
						if (Zero > 2)
						{
							throw new ArgumentException("Invalidate tripple with 0");
						}
						break;
					case '1':
						One++;
						if (FirstContent == ' ')
						{
							FirstContent = '1';
						}
						else
						{
							IsSame = FirstContent == Examine[i].Content;
						}
						if (One > 2)
						{
							throw new ArgumentException("Invalidate tripple with 1");
						}
						break;
					default:
						Empty++;
						EmptyCell = i;
						break;
				}
			}//for
			if (Empty == 1 && IsSame)
			{
				Examine[EmptyCell].Content = FirstContent == '0' ? '1' : '0';
				Examine[EmptyCell].Source = Source.rulefilled;
				result = true;
			}
			return result;
		}
	}
}

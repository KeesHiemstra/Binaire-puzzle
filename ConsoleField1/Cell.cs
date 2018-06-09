using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleField1
{
	public enum Source { empty, prefilled, rulefilled }

	class Cell
	{
		public char Content { get; set; }
		public Source Source { get; set; }
	}
}

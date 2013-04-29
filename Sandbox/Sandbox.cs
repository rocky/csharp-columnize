using System;
using Columnize;
using Columnize.Opts;

namespace Columnize
{
	class MainClass
	{
		static RowColData MinRowsAndColwidths (string[] list, Opts.Opts opts)
		{
			return new Columnize(list, opts).minRowsAndColwidths();
		}

		public static void Main (string[] args)
		{
			// var data = new string[55];
			var data = new string[55];
			for (int i=0; i<55; i++) {
				data [i] = i.ToString ();
			}
			var opts = new Opts.Opts ();
			opts.DisplayWidth = 39;
			opts.ColSep = "  ";
			// horizontal
			opts.ArrangeVertical = false;

			var rowColData = MinRowsAndColwidths (data, opts);
			Console.WriteLine("{0}", rowColData.widths);
		}
	}
}

using System;
using Columnize;
using Columnize.Opts;

namespace Columnize
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// var data = new string[55];
			// for (int i=0; i<55; i++) {
			// 	data [i] = i.ToString ();
			// }
			// var opts = new Opts.Opts ();
			// opts.DisplayWidth = 39;
			// opts.ColSep = "  ";
			// // horizontal
			// opts.ArrangeVertical = false;

			// var rowcolData = new Columnize(data, opts).minRowsAndColwidths();
			// return;

			string [] theArray5 = {"1", "2", "3", "4", "5"};
			Columnize.Arrangement<string> stringArrange = new Columnize.Arrangement<string>();
			string[][] rows = stringArrange.ArrangeByRow(theArray5, 3, 2);
			for (int i=0; i<rows.GetLength(0); i++) {
				for (int j=0; j<rows[i].GetLength(0); j++) {
					Console.Write ("{0} ", rows[i][j]);
				}
				Console.WriteLine ();
			}
			Console.WriteLine ();

			rows = stringArrange.ArrangeByColumn(theArray5, 2, 3);
			for (int i=0; i<rows.GetLength(0); i++) {
				for (int j=0; j<rows[i].GetLength(0); j++) {;
					Console.Write ("{0} ", rows[i][j]);
				}
				Console.WriteLine();
			}

			string[] theArray = {"1", "2"};
			var opts = new Opts.Opts (DisplayWidth: 60);
			Console.WriteLine ("Displaywidth {0}", opts.DisplayWidth);
			Console.WriteLine ("Arrange Vertical {0}", opts.ArrangeVertical);

			opts.DisplayWidth = 1;
			Console.WriteLine (Columnize.columnize (theArray, opts));
			Console.WriteLine ("Displaywidth {0}", opts.DisplayWidth);

			opts = Opts.Opts.DefaultOpts();
			opts.ArrangeVertical = false;
			Console.WriteLine ("{0}", opts.DisplayWidth);
			Console.WriteLine ("{0}", opts.ArrangeVertical);

			Console.WriteLine (Columnize.columnize (theArray, opts));

			theArray = new string[] {};
			Console.Write (Columnize.columnize (theArray, opts));
			theArray = new string[] {"1"};
			Console.Write (Columnize.columnize (theArray, opts));
		}
	}
}

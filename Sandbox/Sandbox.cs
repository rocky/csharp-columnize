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
			// var data = new string[55];
			var theArray = new string[10];
			for (int i=0; i<10; i++) {
				theArray [i] = i.ToString ();
			}
			var opts = Opts.Opts.DefaultOpts ();
			opts.DisplayWidth = 10;
			opts.ArrangeArray = true;
			string got = Columnize.columnize (theArray, opts);
			string expect =
			  "1  5   9\n" +
			  "2  6  10\n" +
			  "3  7\n" +
			  "4  8";
			Console.WriteLine(got, expect);
		}
	}
}

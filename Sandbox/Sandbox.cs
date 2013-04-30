using System;
using Columnize;
using Columnize.Opts;

namespace Columnize
{
	class MainClass
	{
		static string RowsCols2Line (string[][] data, int[] colwidth, Opts.Opts opts)
		{
			string[] text = new string[data.GetLength(0)];
			string alignment_prefix = opts.LeftJustify ? "-" : "";
			for (int i=0;  i<data.GetLength (0); i++) {
				for (int j=0; j<data[i].GetLength(0); j++) {
					string alignment = alignment_prefix + colwidth[j].ToString();
					string fmt = "{0," + alignment + "}";
					data[i][j] = String.Format(fmt, data[i][j]);
				}
				text[i] = opts.LinePrefix + string.Join(opts.ColSep, data[i]) + opts.LineSuffix;


			}
			// text.first.sub!(/^#{@line_prefix}/, @array_prefix) unless @array_prefix.empty?
			// text.last.sub!(/#{@line_suffix}$/, @array_suffix) unless @array_suffix.empty?
			return string.Join("\n", text);
		}

		static RowColData MinRowsAndColwidths (string[] list, Opts.Opts opts)
		{
			return new Columnize(list, opts).minRowsAndColwidths();
		}

		public static void Main (string[] args)
		{

			// string[] theArray = {"1", "2", "3", "4", "5"};
			// Columnize.Arrangement<string> stringArrange = new Columnize.Arrangement<string>();
			// var got = stringArrange.ArrangeByColumn (theArray, 2, 3);
			// var expect = new string[][]{ new string[]{"1", "3", "5"}, new string[]{ "2", "4" }};
			// // Assert.AreEqual (expect, got, "ArrangeByColumn");


			var data = new string[55];
			for (int i=0; i<55; i++) {
				data [i] = i.ToString ();
			}
			var opts = Opts.Opts.DefaultOpts();
			opts.DisplayWidth = 39;
			opts.ColSep = "  ";
			opts.LeftJustify = false;
			// horizontal
			opts.ArrangeVertical = false;

			var rowcolData = MinRowsAndColwidths (data, opts);

			// int[] expect = {2,2,2,2,2,2,2,2,2,2};
			// Assert.AreEqual(expect, rowcolData.widths,
			// 		"colwidths - horizontal");
			// Assert.AreEqual(10, rowcolData.data.GetLength(0),
			//                 "number of rows - vertical");
			// Assert.AreEqual(6, rowcolData.data[0].GetLength(0),
			//                 "number of cols - horizontal");

			var data2d = rowcolData.data;
			var result = RowsCols2Line(data2d, rowcolData.widths, opts);
			Console.Write(result);

			// var data = new string[11];
			// for (int i=0; i<data.GetLength(0); i++) {
			// 	data [i] = i.ToString ();
			// }
			// var opts = new Opts.Opts ();
			// opts.DisplayWidth = 6;
			// opts.ColSep = "  ";
			// // horizontal
			// opts.ArrangeVertical = false;

			// var rowColData = MinRowsAndColwidths (data, opts);
			// var data2d = rowColData.data;
			// Console.WriteLine("{0} rows {1} cols", data2d.GetLength(0),
			// 		  data2d[0].GetLength(0));
		}
	}
}

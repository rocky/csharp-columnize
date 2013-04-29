using System;
using Columnize;
using Columnize.Opts;
using NUnit.Framework;

namespace Columnize
{
	[TestFixture]
	public class MinRowsTest
	{
		static RowColData MinRowsAndColwidths (string[] list, Opts.Opts opts)
		{
			return new Columnize(list, opts).minRowsAndColwidths();
		}

		[Test]
		//[Ignore("Ignore a fixture")]
		public void TestHorizontalVsVertical ()
		{
			var data = new string[55];
			for (int i=0; i<55; i++) {
				data [i] = i.ToString ();
			}
			var opts = new Opts.Opts ();
			opts.DisplayWidth = 39;
			opts.ColSep = "  ";
			// horizontal
			opts.ArrangeVertical = false;

			var rowcolData = MinRowsAndColwidths (data, opts);
			int[] expect = {2,2,2,2,2,2,2,2,2,2};
			Assert.AreEqual(expect, rowcolData.widths,
					"colwidths - vertical");
			Assert.AreEqual(10, rowcolData.data.GetLength(0),
			                "number of rows - vertical");
			Assert.AreEqual(6, rowcolData.data[0].GetLength(0),
			                "number of cols - horizontal");
			// vertical
			opts.ArrangeVertical = true;
			expect = new int[] {1,2,2,2,2,2,2,2,2,2};
			rowcolData = MinRowsAndColwidths (data, opts);
			Assert.AreEqual(expect, rowcolData.widths,
			                "colwidths - vertical");
			Assert.AreEqual(10, rowcolData.data.GetLength(0),
			                "number of rows - horizontal");
			Assert.AreEqual(6, rowcolData.data[0].GetLength(0),
			                "number of cols");
		}
	}
}

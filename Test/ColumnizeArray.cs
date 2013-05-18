using System;
using Columnize;
using Columnize.Opts;
using NUnit.Framework;

namespace Columnize
{
	[TestFixture]
	public class ColumnizeArray
	{
		[Test]
		public void TestArrangeArray ()
		{
			// var data = new string[55];
			var theArray = new string[10];
			for (int i=0; i<10; i++) {
				theArray [i] = (i+1).ToString ();
			}
			var opts = Opts.Opts.DefaultOpts ();
			opts.DisplayWidth = 10;
			// opts.ColSep = "  ";
			// opts.ArrangeVertical = true;
			// string got = Columnize.columnize (theArray, opts);
			// string expect =
			//   "1  5   9\n" +
			//   "2  6  10\n" +
			//   "3  7\n" +
			//   "4  8";
			// Assert.AreEqual(got, expect, "Array vertical");
			opts.ArrangeArray = true;
			var got = Columnize.columnize (theArray, opts);

			var expect =
				"[ 1, 2, 3,\n" +
				"  4, 5, 6,\n" +
				"  7, 8, 9,\n" +
				" 10]";
			Assert.AreEqual(expect, got, "Array horizontal");
		}
	}
}

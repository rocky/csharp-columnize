using System;
using Columnize;
using Columnize.Opts;
using NUnit.Framework;

namespace Columnize
{
	[TestFixture]
	public class ColumnizeTest
	{
		[Test]
		public void TestBasic ()
		{
			string [] theArray = {};
			var opts = new Opts.Opts ();
			Assert.AreEqual ("<empty>\n", Columnize.columnize (theArray, opts));
		}

		[Test]
		public void TestArrangeRowsandCols ()
		{
			string[] theArray = {"1", "2", "3", "4", "5"};
			Columnize.Arrangement<string> stringArrange = new Columnize.Arrangement<string>();
			string[][] got = stringArrange.ArrangeByRow (theArray, 3, 2);
			var expect = new string[][]{ new string[]{"1", "2"}, new string[]{ "3", "4" }, new string[]{ "5"}};
			Assert.AreEqual (expect, got, "ArrangeByRow");
			got = stringArrange.ArrangeByColumn (theArray, 2, 3);
			expect = new string[][]{ new string[]{"1", "3", "5"}, new string[]{ "2", "4" }};
			Assert.AreEqual (expect, got, "ArrangeByColumn");
		}

	}
}

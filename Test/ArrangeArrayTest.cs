using System;
using Columnize;
using NUnit.Framework;

namespace Columnize
{
	[TestFixture]
	public class ArrayArrangeTest
	{
		[Test]
		public void TestArrayArrange ()
		{
			int[] list = {1,2,3,4,5,6,7,8,9};
			Columnize.Arrangement<int> intArrange = new Columnize.Arrangement<int>();
			int[][] data = intArrange.ArrangeByRow (list, 3, 3);
			int[][] expect = {
				new int[] {1,2,3},
				new int[] {4,5,6},
				new int[] {7,8,9}
			};
			Assert.AreEqual(data, expect, "rows for (1..9), 3,3");
			data = intArrange.ArrangeByColumn (list, 3, 3);
			int[][] expect2 = {
				new int[] {1,4,7},
				new int[] {2,5,8},
				new int[] {3,6,9}
			};
			Assert.AreEqual(data, expect2, "cols for (1..9), 3,3");
			int[] list2 = {1,2,3,4,5};
			int[][] expect3 = {
				new int[] {1,3,5},
				new int[] {2,4}
			};
			data = intArrange.ArrangeByColumn (list2, 2, 3);
			Assert.AreEqual(data, expect3, "cols for (1..5), 2,3");
		}
	}
}

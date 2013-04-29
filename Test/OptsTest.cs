using System;
using Columnize.Opts;
using NUnit.Framework;

namespace Columnize
{
	[TestFixture]
	public class OptsTest
	{
		[Test]
		public void TestDefaultOpts ()
		{
			var opts = Opts.Opts.DefaultOpts();
			Assert.Greater(opts.DisplayWidth, 0, "DisplayWidth should be a reasonable number");
			Assert.AreNotEqual(opts.LinePrefix, null, "LinePrefix");
			Assert.AreNotEqual(opts.LineSuffix, null, "LineSuffix");
		}
		[Test]
		public void TestOptsNew ()
		{
			var opts = new Opts.Opts(DisplayWidth: 30);
			Assert.AreEqual(opts.DisplayWidth, 30, "Setting DisplayWidth");
			opts = new Opts.Opts(ArrangeVertical: true, ColSep: " | ");
			Assert.AreEqual(opts.ColSep, " | ", "ColSep");
			Assert.AreEqual(opts.ArrangeVertical, true, "ArrangeVertical");
		}
	}
}

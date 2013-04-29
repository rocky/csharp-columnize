using System;

namespace Columnize.Opts
{
	/// <summary>
	/// Opts - structure for all of the ways we can influence arrangement.
	/// </summary>
	public struct Opts
	{
		public Boolean ArrangeArray;
		public Boolean ArrangeVertical;
		public string ArrayPrefix;
		public string ArraySuffix;
		public string CellFmt;

		/// <summary>
		/// The string to insert between array elements.
		/// </summary>
		public string ColSep;

		/// <summary>
		/// The maximum number of characters that should exit before a newline in the string.
		/// </summary>
		public int DisplayWidth;
		/// <summary>
		/// The string to add at the beginning of every line.
		/// </summary>
		public string LinePrefix;

		/// <summary>
		/// The string to add at the end of very line.
		/// </summary>
		public string LineSuffix;

		public static Opts DefaultOpts() {
			return new Opts (80);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Columnize.Opts.Opts"/> struct.
		/// </summary>
		/// <param name='DisplayWidth'>
		/// The maximum number of characters that should exit before a newline in the string.
		/// </param>
		/// <param name='ArrangeArray'>
		/// If set to <c>true</c> arrange as an array.
		/// </param>
		/// <param name='ColSep'>
		/// The string to insert between array elements.
		/// </param>
		/// <param name='LinePrefix'>
		/// The string to add at the beginning of every line.
		/// </param>
		/// <param name='LineSuffix'>
		/// The string to add at the end of very line.
		/// </param>
		/// <param name='ArrangeVertical'>
		/// If set to <c>true</c> arrange Array elements vertically rather than horizontally.
		/// </param>
		public Opts (int DisplayWidth=80, Boolean ArrangeArray=false,
		             string ColSep = "  ", string LinePrefix ="", string LineSuffix="",
		             Boolean ArrangeVertical = true)
		{
			this.ArrangeArray = ArrangeArray;
			this.ArrangeVertical = ArrangeVertical;
			this.ArrayPrefix = "[";
			this.ArraySuffix = "]";
			this.CellFmt = "%s";
			this.ColSep = ColSep;
			this.DisplayWidth = DisplayWidth;
			this.LinePrefix = LinePrefix;
			this.LineSuffix = LineSuffix;
		}
	}
}

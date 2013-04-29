using System;
using Columnize.Opts;

namespace Columnize
{
	public struct RowColArrange
	{
		public string[][] rows;
		public string[][] cols;
	}

	public struct RowColData
	{
		public string[][] data;
		public int[] widths;

		public RowColData (string[][] d, int[] w)
		{
			data = d;
			widths = w;
		}
	}

	public class Columnize
	{
		public class Arrangement<T>
		{
			/// <summary>
			/// Arrange a one-dimensional list into a
			/// 2-dimensional Array organized by rows.
			///
			/// In either horizontal or vertical arrangement, we will need to
			/// access this for the list data or for the width
			/// information.

			/// Here is an example:
			/// ArrangeByRow(int string[] {'1', '2', '3', '4', '5'}, 3, 2) =>
			///  [[1,2], [3,4], [5]]
			/// </summary>
			/// <param name='list'>
			/// Array to arrange into a 2d list
			/// </param>
			/// <param name='nrows'>
			/// number of rows in 2D Array.
			/// </param>
			/// <param name='ncols'>
			/// number of columns in 2D Array.
			/// </param>
			public T [][] ArrangeByRow (T[] list, int nrows, int ncols)
			{
				T [][] rows = new T[nrows][];
				int allocated = 0;
				for (int i=0; i<nrows; i++) {
					int len = ncols;
					if (allocated + nrows > list.Length) {
					  len = list.Length - allocated;
					}
					rows[i] = new T[len];
					allocated += len;
					for (int j=0; j<len; j++) {
						int k = (i * ncols) + j;
						if (k >= list.Length)
							break;
						rows [i][j] = list [k];
					}
				}
				return rows;
			}

			/// <summary>
			/// Arrange a one-dimensional list into a
			/// 2-dimensional Array organized by columns.
			///
			/// In either horizontal or vertical arrangement, we will need to
			/// access this for the list data or for the width
			/// information.

			/// Here is an example:
			/// ArrangeByColumn(int string[] {'1', '2', '3', '4', '5'}, 2, 3) =>
			///   [[1,3,5], [2,4]]
			/// </summary>
			/// <param name='list'>
			/// Array to arrange into a 2d list
			/// </param>
			/// <param name='nrows'>
			/// number of rows in 2D Array.
			/// </param>
			/// <param name='ncols'>
			/// number of columns in 2D Array.
			/// </param>
			public T[][] ArrangeByColumn (T[] list, int nrows, int ncols)
			{
				T [][] rows = new T[nrows][];
				int allocated = 0;
				for (int i=0; i<nrows; i++) {
					int len = ncols;
					if (allocated + ncols > list.Length) {
						len = list.Length - allocated;
					}
					rows[i] = new T[len];
					allocated += len;
					for (int j=0; j<len; j++) {
						int k = i + (j * nrows);
						if (k >= list.Length)
							break;
						rows [i][j] = list [k];
					}
				}
				return rows;
			}

		}

		/// <summary>
		/// The list data to be arranged.
		/// </summary>
		string[] list;

		/// <summary>
		/// Various options controlling how to arrange data.
		/// </summary>
		Opts.Opts opts;

		public Columnize (string[] list, Opts.Opts opts)
		{
			this.list = list;
			this.opts = opts; // DEFAULT_OPTS.merge(opts)
		}

		public static string columnize (string[] list, Opts.Opts opts)
		{
			if (list.Length == 0) {
				return "<empty>\n";
			}
			if (list.Length == 1) {
				return string.Format ("{0}\n", list [0]);
			}
			if (opts.DisplayWidth - opts.LinePrefix.Length < 4) {
				opts.DisplayWidth = opts.LinePrefix.Length + 4;
			} else {
				opts.DisplayWidth -= opts.LinePrefix.Length;
			}

			var rowColData = new Columnize(list, opts).minRowsAndColwidths();
			var colwidths = rowColData.widths;
			var data      = rowColData.data;
			return (opts.ArrangeVertical) ?
				"vertical" : "horizontal";
		}

		// Compute the smallest number of rows and the max widths for each column.
		public RowColData minRowsAndColwidths ()
		{
			var list = this.list;
			var cell_widths = new int[list.Length];
			var cell_width_max = 0;
			for (int i=0; i<list.Length; i++) {
				if ((cell_widths [i] = list [i].Length) > cell_width_max) {
					cell_width_max = cell_widths [i];
				}
			}

			// Set default result: one atom per row
			Arrangement<string> stringArrange = new Arrangement<string>();

			string[][] data = stringArrange.ArrangeByRow (list, 1, list.Length);
			var result = new RowColData (data, new int[1] { cell_width_max });

			if (cell_width_max > this.opts.DisplayWidth)
				return result;


			// For horizontal arrangement, we want to *maximize* the number
			// of columns. Thus the candidate number of rows (+sizes+) starts
			// at the minumum number of rows, 1, and increases.
			//
			// For vertical arrangement, we want to *minimize* the number of
			// rows. So here the candidate number of columns (+sizes+) starts
			// at the maximum number of columns, list.length, and
			// decreases. Also the roles of columns and rows are reversed
			// from horizontal arrangement.


			// Loop from most compact arrangement to least compact, stopping
			// at the first successful packing.  The below code is tricky,
			// but very cool.
			Arrangement<int> intArrange = new Arrangement<int>();
			if (this.opts.ArrangeVertical) {
				for (int size=1; size <= list.Length; size++) {
					int other_size = (list.Length + size - 1) / size;
					int[][] cell_width2d = intArrange.ArrangeByRow(cell_widths, other_size, size);
					int[] colwidths = new int[other_size];
					for (int i=0; i<cell_width2d.GetLength(0); i++) {
						for (int j=0; j<cell_width2d[i].GetLength(0); j++)  {
							if (cell_width2d[i][j] > colwidths[i]) colwidths[i] = cell_width2d[i][j];
					  }
					}
					int totwidth = colwidths[0];
					for (int i=1; i<other_size; i++)  {
						totwidth += colwidths[i] + this.opts.ColSep.Length;
					}

					if (totwidth <= this.opts.DisplayWidth) {
					  Arrangement<string> strArrange = new Arrangement<string>();

					  return new RowColData (strArrange.ArrangeByColumn(list, size, other_size),
								 colwidths);
					  }
				}
			} else {
				for (int size=list.Length; size >= 1; size--) {
					int other_size = (list.Length + size - 1) / size;
					int[][] cell_width2d = intArrange.ArrangeByColumn(cell_widths, other_size, size);
					int[] colwidths = new int[size];
					for (int i=0; i<cell_width2d.GetLength(0); i++)  {
						for (int j=0; j<cell_width2d[i].GetLength(0); j++) {
							if (cell_width2d[i][j] > colwidths[i]) colwidths[i] = cell_width2d[i][j];
						}
					}
					int totwidth = colwidths[0];
					for (int i=1; i<size; i++)  {
						totwidth += colwidths[i] + this.opts.ColSep.Length;
					}
					if (totwidth <= this.opts.DisplayWidth) {
					  Arrangement<string> strArrange = new Arrangement<string>();

					  return new RowColData (strArrange.ArrangeByRow(list, size, other_size),
								 colwidths);

					  }
				}
			}
			return result;
		}
	}
}

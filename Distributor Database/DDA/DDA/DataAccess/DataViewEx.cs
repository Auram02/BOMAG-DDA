// Author: Christoph Wille
// (c) 2004, christophw AT alphasierrapapa DOT com

using System;
using System.Data;
using System.Text;
using System.Collections;

namespace DDA.DataAccess
{
	public class DataViewEx : DataView
	{
		#region Mapped Constructors
		public DataViewEx() : base()
		{
		}

		public DataViewEx(DataTable table) : base(table)
		{
		}

		public DataViewEx(DataTable table, string RowFilter, string Sort, DataViewRowState RowState) 
			: base(table, RowFilter, Sort, RowState)
		{
		}
		#endregion

		#region Simple Implementation of ToTable()
		/*
		public DataTable ToTable()
		{
			// short circuiting out here
			int nRowCount = this.Count;
			if (0 == nRowCount) return null;

			// #1: clone the schema
			DataTable tableNew = Table.Clone();

			// #2: get the column count, we need it repeatedly
			int nColumnCount = tableNew.Columns.Count;

			// #3: copy the values to the new table
			for (int iRow = 0; iRow < nRowCount; iRow++)
			{
				DataRow rowNew = tableNew.NewRow();

				for (int iColumn=0; iColumn < nColumnCount; iColumn++)
				{
					rowNew[iColumn] = this[iRow][iColumn];
				}
				tableNew.Rows.Add(rowNew);
			}
			return tableNew;
		}
		*/
		#endregion

		public DataTable ToTable()
		{
			return ToTable(false, null);
		}

		public DataTable ToTable(bool isDistinct, string[] columnNames)
		{
			// short circuiting out here
			int nRowCount = this.Count;
			if (0 == nRowCount) return null;

			// get the column count, we need it repeatedly
			int nColumnCount = Table.Columns.Count;
			int nTargetColumnCount = nColumnCount;

			// if second param == null, we copy the entire table
			if (null != columnNames) nTargetColumnCount = columnNames.Length;

			string[] tableColumnNames = new string[nColumnCount];
			
			for (int iColumn = 0; iColumn < nColumnCount; iColumn++)
				tableColumnNames[iColumn] = Table.Columns[iColumn].ColumnName;

			bool[] keepColumn = new bool[nColumnCount];
			int[] tableColumnIndexes = new int[nTargetColumnCount];
			int[] newtableColumnIndexes = new int[nTargetColumnCount];

			// check to see if the selected columns actually exist & map indexes
			if (null != columnNames)
			{
				for (int i=0; i < columnNames.Length; i++)
				{
					if (Table.Columns.Contains(columnNames[i]))
					{
						int colIndex = Table.Columns.IndexOf(columnNames[i]);
						tableColumnIndexes[i] = colIndex;
						keepColumn[colIndex] = true;
					}
					else
					{
						throw new ArgumentException("Column does not exist in base table");
					}
				}
			}
			else
			{
				for (int i=0; i < nColumnCount; i++)
				{
					tableColumnIndexes[i] = i;
					newtableColumnIndexes[i] = i; 
					keepColumn[i] = true;
				}
			}

			// clone the schema and remove unnecessary columns
			DataTable tableNew = Table.Clone();

			// now we can build the final table... all we need to do is map the string[] to the column indexes
			// in the new table that was now created
			if (null != columnNames)
			{
				// remove columns we no longer need
				for (int k = 0; k < nColumnCount; k++)
				{
					if (keepColumn[k] == false)
						tableNew.Columns.Remove(tableColumnNames[k]);
				}

				// map column names to column indexes
				for (int i=0; i < columnNames.Length; i++)
				{
					int colIndex = tableNew.Columns.IndexOf(columnNames[i]);
					newtableColumnIndexes[i] = colIndex;
				}
			}


			// both variables used for determining duplicate rows
			StringBuilder stb = new StringBuilder();
			Hashtable ht = new Hashtable();

			// copy the values to the new table
			for (int iRow = 0; iRow < nRowCount; iRow++)
			{
				DataRow rowNew = tableNew.NewRow();

				if (isDistinct)
					stb.Remove(0, stb.Length);

				for (int iColumn=0; iColumn < tableColumnIndexes.Length; iColumn++)
				{
					object currentValue = this[iRow][tableColumnIndexes[iColumn]];

					if (isDistinct && (null != currentValue))
						stb.Append(currentValue.ToString());

					rowNew[newtableColumnIndexes[iColumn]] = currentValue;
				}
				
				// do the DISTINCT checks before inserting row
				if (isDistinct)
				{
					string strRowKey = stb.ToString();
					if (!ht.ContainsKey(strRowKey))
					{
						ht.Add(strRowKey, null);
						tableNew.Rows.Add(rowNew);
					}
				}
				else
				{
					tableNew.Rows.Add(rowNew);
				}
			}

			// return the new table
			return tableNew;
		}
	}
}

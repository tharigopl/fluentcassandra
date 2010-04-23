﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Linq.Expressions;
using System.ComponentModel;

namespace FluentCassandra
{
	public class FluentColumnFamily : FluentColumnFamily<FluentColumn>
	{
		public FluentColumnFamily()
			: base(ColumnType.Normal)
		{
			Columns = new FluentColumnList(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="binder"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			var col = Columns.FirstOrDefault(c => c.Name == binder.Name);

			// if column doesn't exisit create it and add it to the columns
			if (col == null)
			{
				col = new FluentColumn();
				col.Name = binder.Name;

				Columns.Add(col);
			}

			// set the column value
			if (!(value is FluentSuperColumn))
				col.SetValue(value);

			// notify that property has changed
			OnPropertyChanged(binder.Name);

			return true;
		}
	}
}
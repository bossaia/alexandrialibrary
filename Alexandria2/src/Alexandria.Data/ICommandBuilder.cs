using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface ICommandBuilder
	{
		ICommandBuilder Set(IColumn column, object value);

		ICommandBuilder Where(IColumn column);
		ICommandBuilder And(IColumn column);
		ICommandBuilder Or(IColumn column);
		
		ICommandBuilder InnerJoin(IJoin join);
		ICommandBuilder LeftOuterJoin(IJoin join);
		ICommandBuilder RightOuterJoin(IJoin join);

		ICommandBuilder IsGreaterThan(object value);
		ICommandBuilder IsGreaterThanOrEqualTo(object value);
		ICommandBuilder IsEqualTo(object value);
		ICommandBuilder IsLessThan(object value);
		ICommandBuilder IsLessThanOrEqualTo(object value);
		ICommandBuilder IsNotEqualTo(object value);
		ICommandBuilder IsNotNull();
		ICommandBuilder IsNull();
		
		IDbCommand Build();
	}
}

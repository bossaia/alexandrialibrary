using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IQueryBuilder
		: ICommandBuilder
	{
		IQueryBuilder Column(IColumn column);
		IQueryBuilder Limit(int limit);
		IQueryBuilder Limit(int limit, int offset);
		IQueryBuilder OrderBy(IColumn column);
		IQueryBuilder OrderByDescending(IColumn column);
		IQueryBuilder GroupBy(IColumn column);
		IQueryBuilder Average(IColumn column);
		IQueryBuilder Count();
		IQueryBuilder Count(IColumn column);
		IQueryBuilder Min(IColumn column);
		IQueryBuilder Max(IColumn column);
		IQueryBuilder Sum(IColumn column);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Alexandria.Core
{
	public static class ReflectionHelper
	{
		private static MemberExpression GetMemberExpression(UnaryExpression expression)
		{
			return expression.Operand as MemberExpression;
		}

		private static MemberExpression GetMemberExpression<T, V>(Expression<Func<T, V>> expression)
		{
			return expression.Body as MemberExpression;
		}

		public static Predicate GetPredicate<T, V>(Expression<Func<T, V>> expression, V value)
		{
			PropertyInfo property = null;
			ComparisonOperator op = null;
			IList<string> methods = new List<string>();

			if (expression != null)
			{
				MemberExpression memberExpression = null;
				if (expression.Body.NodeType == ExpressionType.Call)
				{
					var body = expression.Body as MethodCallExpression;

					while (body != null)
					{
						op = body.Method.Name.ToComparisonOperator();
						
						if (body.Arguments != null && body.Arguments.Count > 0)
						{
							memberExpression = body.Arguments[0] as MemberExpression;
							if (memberExpression != null)
								break;

							body = body.Arguments[0] as MethodCallExpression;
							if (body != null)
							{
								methods.Add(body.Method.Name);

								memberExpression = body.Object as MemberExpression;
								if (memberExpression != null)
								{
									break;
								}
							}
						}
						else
						{
							memberExpression = body.Object as MemberExpression;
							if (memberExpression == null)
							{
								var unary = body.Object as UnaryExpression;
								if (unary != null)
									memberExpression = unary.Operand as MemberExpression;
							}

							break;
						}
					}	
				}

				if (memberExpression != null)
					property = memberExpression.Member as PropertyInfo;
			}

			return new Predicate(property, op, value, methods);
		}
	}
}

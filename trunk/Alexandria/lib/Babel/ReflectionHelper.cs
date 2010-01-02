using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Babel
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

		private static object GetArgument(MethodCallExpression expression, object context)
		{
			var arg = expression.Arguments[expression.Arguments.Count - 1]; //.ToString();

			if (arg is ConstantExpression)
				return ((ConstantExpression)arg).Value;
			else if (arg is NewExpression)
			{
				NewExpression ne = arg as NewExpression;

				object[] args = new object[ne.Arguments.Count];
				for (int i = 0; i < args.Length; i++)
				{
					var c = ne.Arguments[i] as ConstantExpression;
					args[i] = c.Value;
				}

				return ne.Constructor.Invoke(args);
			}
			else if (arg is MemberExpression)
			{
				MemberExpression me = arg as MemberExpression;
				if (me.Member is FieldInfo)
				{
					FieldInfo fi = me.Member as FieldInfo;
					return fi.GetValue(context);
				}
				else if (me.Member is PropertyInfo)
				{
					PropertyInfo pi = me.Member as PropertyInfo;
					return pi.GetValue(context, null);
				}
			}
			//if (arg.Length > 1 && arg.StartsWith("\"") && arg.EndsWith("\""))
				//arg = arg.Substring(1, arg.Length - 2);

			//return arg;
			return arg.ToString();
		}

		private static string GetTableName(Type type)
		{
			var name = type.Name;

			if (name.Length > 2 && name.StartsWith("I"))
			{
				if (name[1].ToString() == name[1].ToString().ToUpper())
					return name.Substring(1, name.Length - 1);
			}

			return name;
		}

		private static string GetProperty(PropertyInfo propertyInfo)
		{
			return string.Format("{0}.{1}", GetTableName(propertyInfo.DeclaringType), propertyInfo.Name);
		}

		public static Predicate GetPredicate<T>(Expression<Func<T, object>> expression, object context)
		{
			if (expression == null)
				return null;

			MemberExpression memberExpression = null;
			string property = null;
			ComparisonOperator op = null;
			object value = null;
			IList<string> methods = new List<string>();
			
			if (expression.Body.NodeType == ExpressionType.Call || expression.Body.NodeType == ExpressionType.Convert)
			{
				MethodCallExpression body = null;
				
				if (expression.Body.NodeType == ExpressionType.Call)
					body = expression.Body as MethodCallExpression;
				else if (expression.Body.NodeType == ExpressionType.Convert)
				{
					var unary = expression.Body as UnaryExpression;
					body = unary.Operand as MethodCallExpression;
				}

				while (body != null)
				{
					op = body.Method.Name.ToComparisonOperator();

					value = GetArgument(body, context);

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
			{
				property = GetProperty(memberExpression.Member as PropertyInfo);
			}

			return new Predicate(property, op, value, methods);
		}
	}
}
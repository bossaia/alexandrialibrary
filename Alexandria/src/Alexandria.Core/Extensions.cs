using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public static class DateTimeExtensions
	{
		public static DateTime IsEqualTo(this DateTime value)
		{
			return value;
		}

		public static DateTime IsGreaterThanOrEqualTo(this DateTime value)
		{
			return value;
		}
	}

	public static class EntityExtensions
	{
		public static IEntity IsIdenticalTo(this IEntity value)
		{
			return value;
		}

		public static IEntity IsNotIdenticalTo(this IEntity value)
		{
			return value;
		}
	}

	public static class IntExtensions
	{
		public static int IsEqualTo(this int value)
		{
			return value;
		}

		public static int IsGreaterThan(this int value)
		{
			return value;
		}
	}

	public static class ObjectExtensions
	{
		public static string ToValueString(this object value)
		{
			if (value == null)
				return "NULL";

			if (value.GetType() == typeof(string))
				return string.Format("'{0}'", value);
			if (value.GetType() == typeof(DateTime))
				return string.Format("'{0:yyyyMMdd}'", value);

			return value.ToString();
		}
	}

	public static class StringExtensions
	{
		public static ComparisonOperator ToComparisonOperator(this string value)
		{
			switch (value)
			{
				case "IsGreaterThan":
					return ComparisonOperator.GreaterThan;
				case "IsGreaterThanOrEqualTo":
					return ComparisonOperator.GreaterThanOrEqualTo;
				case "IsLike":
					return ComparisonOperator.Like;
				case "IsNotEqualTo":
					return ComparisonOperator.NotEqualTo;
				case "IsEqualTo":
				default:
					return ComparisonOperator.EqualTo;
			}
		}

		public static string IsEqualTo(this string value)
		{
			return value;
		}

		public static string IsLike(this string value)
		{
			return value;
		}

		public static string IsNotEqualTo(this string value)
		{
			return value;
		}

		public static string IsNotLike(this string value)
		{
			return value;
		}

		public static string UPPER(this string value)
		{
			return value;
		}

		public static string LOWER(this string value)
		{
			return value;
		}

		public static string TRIM(this string value)
		{
			return value;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public static class DateTimeExtensions
	{
		public static DateTime IsEqualTo(this DateTime value, DateTime date)
		{
			return value;
		}

		public static DateTime IsEqualTo(this DateTime value, string date)
		{
			return value;
		}

		public static DateTime IsGreaterThan(this DateTime value, DateTime date)
		{
			return value;
		}

		public static DateTime IsGreaterThan(this DateTime value, string date)
		{
			return value;
		}

		public static DateTime IsGreaterThanOrEqualTo(this DateTime value, DateTime date)
		{
			return value;
		}

		public static DateTime IsGreaterThanOrEqualTo(this DateTime value, string date)
		{
			return value;
		}

		public static DateTime IsLessThan(this DateTime value, DateTime date)
		{
			return value;
		}

		public static DateTime IsLessThan(this DateTime value, string date)
		{
			return value;
		}

		public static DateTime IsLessThanOrEqualTo(this DateTime value, DateTime date)
		{
			return value;
		}

		public static DateTime IsLessThanOrEqualTo(this DateTime value, string date)
		{
			return value;
		}
	}

	//public static class EntityExtensions
	//{
	//    public static IEntity IsIdenticalTo(this IEntity value, IEntity entity)
	//    {
	//        return value;
	//    }

	//    public static IEntity IsNotIdenticalTo(this IEntity value, IEntity entity)
	//    {
	//        return value;
	//    }
	//}

	public static class IntExtensions
	{
		public static int IsEqualTo(this int value, int number)
		{
			return value;
		}

		public static int IsGreaterThan(this int value, int number)
		{
			return value;
		}

		public static int IsGreaterThanOrEqualTo(this int value, int number)
		{
			return value;
		}

		public static int IsLessThan(this int value, int number)
		{
			return value;
		}

		public static int IsLessThanOrEqualTo(this int value, int number)
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
				case "IsLessThan":
					return ComparisonOperator.LessThan;
				case "IsLessThanOrEqualTo":
					return ComparisonOperator.LessThanOrEqualTo;
				case "IsLike":
					return ComparisonOperator.Like;
				case "IsNotEqualTo":
					return ComparisonOperator.NotEqualTo;
				case "IsNotLike":
					return ComparisonOperator.NotLike;
				case "IsEqualTo":
				default:
					return ComparisonOperator.EqualTo;
			}
		}

		public static string IsEqualTo(this string value, string text)
		{
			return value;
		}

		public static string IsLike(this string value, string text)
		{
			return value;
		}

		public static string IsNotEqualTo(this string value, string text)
		{
			return value;
		}

		public static string IsNotLike(this string value, string text)
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

		public static string SUBSTR(this string value, int index, int length)
		{
			return value;
		}
	}
}

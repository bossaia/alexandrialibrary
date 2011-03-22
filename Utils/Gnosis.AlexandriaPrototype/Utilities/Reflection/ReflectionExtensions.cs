﻿using System;
using System.Linq.Expressions;

namespace Gnosis.Alexandria.Utilities.Reflection
{
    public static class ReflectionExtensions
    {
        public static Member ToMember<TMapping, TReturn>(this Expression<Func<TMapping, TReturn>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }
    }
}

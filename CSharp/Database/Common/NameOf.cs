using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Sunrise.Radiology.Messenger.Database.Common
{
    public class NameOf
    {
        private static MemberInfo Member<T, TReturn>(Expression<Func<T, TReturn>> expression)
        {
            var body = (MemberExpression)expression.Body;
            if (body == null)
            {
                throw new ArgumentException("'expression' should be a member expression");
            }
            return body.Member;
        }


        public static string Property<T, TReturn>(Expression<Func<T, TReturn>> expression)
        {
            return Member(expression).Name;
        }
    }



    public class NameOf<T> : NameOf
    {
        public static string Property<TProp>(Expression<Func<T, TProp>> expression)
        {
            return NameOf.Property(expression);
        }
    }
}

using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation.Internal;

namespace EventsExpress.Validation
{
    public static class CamelCasePropertyNameResolver
    {
        public static string ResolvePropertyName(MemberInfo memberInfo, LambdaExpression expression)
        {
            return ToCamelCase(DefaultPropertyNameResolver(memberInfo, expression));
        }

        private static string DefaultPropertyNameResolver(MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression != null)
            {
                var chain = PropertyChain.FromExpression(expression);
                if (chain.Count > 0)
                {
                    return chain.ToString();
                }
            }

            return memberInfo != null ? memberInfo.Name : null;
        }

        private static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            var chars = s.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                var hasNext = i + 1 < chars.Length;
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }
    }
}

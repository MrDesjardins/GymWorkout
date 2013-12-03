using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared
{
    public class LambdaUtilities
    {
        public static string GetExpressionText(LambdaExpression expression)
        {
            var stack = new Stack<string>();
            Expression expression1 = expression.Body;
            while (expression1 != null)
            {
                if (expression1.NodeType == ExpressionType.Call)
                {
                    var methodCallExpression = (MethodCallExpression) expression1;
                    if (IsSingleArgumentIndexer(methodCallExpression))
                    {
                        stack.Push(GetIndexerInvocation((methodCallExpression.Arguments).Single(), (expression.Parameters).ToArray()));
                        expression1 = methodCallExpression.Object;
                    }
                    else
                        break;
                }
                else if (expression1.NodeType == ExpressionType.ArrayIndex)
                {
                    var binaryExpression = (BinaryExpression) expression1;
                    stack.Push(GetIndexerInvocation(binaryExpression.Right, (expression.Parameters).ToArray()));
                    expression1 = binaryExpression.Left;
                }
                else if (expression1.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpression = (MemberExpression) expression1;
                    stack.Push("." + memberExpression.Member.Name);
                    expression1 = memberExpression.Expression;
                }
                else if (expression1.NodeType == ExpressionType.Parameter)
                {
                    stack.Push(string.Empty);
                    expression1 = null;
                }
                else
                    break;
            }
            if (stack.Count > 0 && string.Equals(stack.Peek(), ".model", StringComparison.OrdinalIgnoreCase))
                stack.Pop();
            if (stack.Count <= 0)
                return string.Empty;
            return (stack).Aggregate(((left, right) => left + right)).TrimStart(new char[1] {'.'});
        }

        private static bool IsSingleArgumentIndexer(Expression expression)
        {
            var methodExpression = expression as MethodCallExpression;
            if (methodExpression == null || methodExpression.Arguments.Count != 1)
                return false;
            else
                return (methodExpression.Method.DeclaringType.GetDefaultMembers()).OfType<PropertyInfo>().Any((p => p.GetGetMethod() == methodExpression.Method));
        }


        private static string GetIndexerInvocation(Expression expression, ParameterExpression[] parameters)
        {
            return "";
            //Expression<Func<object, object>> lambdaExpression = Expression.Lambda<Func<object, object>>(Expression.Convert(expression, typeof (object)), new ParameterExpression[1] {Expression.Parameter(typeof (object), null)});
            //Func<object, object> func;
            //try
            //{
            //    func = CachedExpressionCompiler.Process<object, object>(lambdaExpression);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Invalid", new object[2] {expression, parameters[0].Name}), ex);
            //}
            //return "[" + Convert.ToString(func(null), CultureInfo.InvariantCulture) + "]";
        }


    }
}
using System.Linq.Expressions;

namespace Abd.Shared.Utils.LinqUtils;

public static class CombineExpression
{
    public static Expression<Func<TValue, bool>> And<TValue>(
        this Expression<Func<TValue, bool>> left,
        Expression<Func<TValue, bool>> right)
    {
        return Combine(left, right, Expression.And);
    }

    public static Expression<Func<TValue, bool>> Combine<TValue>(
        this Expression<Func<TValue, bool>> left,
        Expression<Func<TValue, bool>> right,
        Func<Expression, Expression, BinaryExpression> combination)
    {
        if (left == null)
        {
            left = value => true;
        }
        // rewrite the body of "right" using "left"'s parameter in place
        // of the original "right"'s parameter
        var newRight = new SwapVisitor(right.Parameters[0], left.Parameters[0])
            .Visit(right.Body);
        // combine via && / || etc and create a new lambda
        return Expression.Lambda<Func<TValue, bool>>(
            combination(left.Body, newRight), left.Parameters);
    }

    private class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression _from, _to;
        public SwapVisitor(Expression from, Expression to)
        {
            _from = from;
            _to = to;
        }
        public override Expression Visit(Expression node) =>
            node == _from ? _to : base.Visit(node);
    }
}
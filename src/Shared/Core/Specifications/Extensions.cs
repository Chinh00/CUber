using System.Linq.Expressions;

namespace Core.Specifications;

public static class Extensions
{

    public static Expression<Func<TEntity, bool>> BuildFilter<TEntity>(FilterModel filterModel) where TEntity : BaseEntity    
    {
        var param = Expression.Parameter(typeof(TEntity), "x");
        var property = filterModel.Field.Split(".").Aggregate((Expression)param, Expression.Property);
        return Expression.Lambda<Func<TEntity, bool>>(BuildComparision(property, filterModel.Comparision, filterModel.Value), param);


    }

    static Expression BuildComparision(Expression left, string comparison, string value)
    {
        return comparison switch
        {
             "==" => Expression.Equal(left, Expression.Constant(value)),
             ">" => Expression.GreaterThan(left, Expression.Constant(value)),
             "<" => Expression.LessThan(left, Expression.Constant(value)),
             _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null)
        };
    }

    
    
    public static Expression<Func<TEntity, bool>> And<TEntity>(this Expression<Func<TEntity, bool>> left,
        Expression<Func<TEntity, bool>> right)
        where TEntity : BaseEntity
    {
        var param = left.Parameters[0];
        var subExpression = new SubExpressionVisitor()
        {
            Subst =
            {
                { right.Parameters[0], param }
            }
        };

        return Expression.Lambda<Func<TEntity, bool>>(Expression.And(left, subExpression.Visit(right)), param);
    }


    public class SubExpressionVisitor : ExpressionVisitor 
    {
        public readonly Dictionary<Expression, Expression> Subst = new();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return Subst.TryGetValue(node, out var newValue) ? newValue : node;
        }
    }
}
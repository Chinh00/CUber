using System.Linq.Expressions;

namespace Core.Specifications;

public interface IRootSpecification
{
    
}
public interface ISpecification<TEntity> : IRootSpecification
    where TEntity : BaseEntity
{
    Expression<Func<TEntity, bool>> Predicate { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    List<Expression<Func<TEntity, object>>> OrderBy { get; }
    List<Expression<Func<TEntity, object>>> OrderDescBy { get; }
    int Take { get; }
    int Skip { get; }
}

public interface IListSpecification<TEntity> : IRootSpecification
    where TEntity : BaseEntity
{
    List<Expression<Func<TEntity, bool>>> Predicates { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    List<Expression<Func<TEntity, object>>> OrderBy { get; }
    List<Expression<Func<TEntity, object>>> OrderDescBy { get; }
    int Take { get; }
    int Skip { get; }
}


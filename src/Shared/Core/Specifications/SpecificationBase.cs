using System.Linq.Expressions;

namespace Core.Specifications;

public class SpecificationBase<TEntity> : ISpecification<TEntity>
    where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>> Predicate { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; }
    public List<string> IncludeStrings { get; }
    public List<Expression<Func<TEntity, object>>> OrderBy { get; }
    public List<Expression<Func<TEntity, object>>> OrderDescBy { get; }
    public int Take { get; }
    public int Skip { get; }
    
}
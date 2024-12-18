using System.Linq.Expressions;

namespace Core.Specifications;

public class ListSpecificationBase<TEntity> : IListSpecification<TEntity>
    where TEntity : BaseEntity
{
    public List<Expression<Func<TEntity, bool>>> Predicates { get; } = [];
    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
    public List<string> IncludeStrings { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderBy { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderDescBy { get; } = [];
    public int Take { get; } = 1;
    public int Skip { get; } = 15;

    void ApplyFilter(FilterModel filter)
    {
        Predicates.Add(Extensions.BuildFilter<TEntity>(filter));
    }
    
}
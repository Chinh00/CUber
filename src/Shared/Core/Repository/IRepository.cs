using System.Linq.Expressions;

namespace Core.Repository;

public interface IRootRepository
{
    
}

public interface IRepository<TEntity> : IRootRepository
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


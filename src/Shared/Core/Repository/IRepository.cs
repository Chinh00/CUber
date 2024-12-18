using System.Linq.Expressions;
using Core.Specifications;

namespace Core.Repository;

public interface IRootRepository
{
    
}

public interface IRepository<TEntity> : IRootRepository
    where TEntity : BaseEntity
{
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
    Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
}

public interface IListRepository<TEntity> : IRootRepository
    where TEntity : BaseEntity
{
    Task<List<TEntity>> FindAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken);
}

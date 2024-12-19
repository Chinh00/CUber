using Core.Specifications;

namespace Infrastructure.EfCore.Data;

public class RepositoryBase<TDbContext, TEntity> : IRepository<TEntity>, IListRepository<TEntity>
    where TDbContext : AppBaseContext
    where TEntity : BaseEntity
{
    private readonly TDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(TDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }


    public List<Expression<Func<TEntity, bool>>> Predicates { get; } = [];
    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
    public List<string> IncludeStrings { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderBy { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderDescBy { get; } = [];
    public int Take { get; } = 1;
    public int Skip { get; } = 15;

    private IQueryable<TEntity> GetQuery(IQueryable<TEntity> source, ISpecification<TEntity> specification)
    {
        source = source.Where(specification.Predicate);
        
        
        return source;
    }
    private IQueryable<TEntity> GetQuery(IQueryable<TEntity> source, IListSpecification<TEntity> specification)
    {
        if (specification.Predicates is not null)
        {
            var pre = specification.Predicates[0];
            for (var i = 1; i < specification.Predicates.Count; i++)
            {
                pre = pre.And(specification.Predicates[i]);
            }

            source = source.Where(pre);
        }

        return source;
    }

    
    
    
    public async Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        var query = GetQuery(_dbSet, specification);
        return await query.ToListAsync(cancellationToken);
    }

    public Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FindOneAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TEntity>> FindAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        var query = GetQuery(_dbSet, specification);
        return await query.ToListAsync(cancellationToken);
    }
}
using Core.Domain;
using Infrastructure.EfCore.Data;

namespace TripService.Infrastructure.Data;

public class TripRepository<TEntity>(TripContext context) : RepositoryBase<TripContext, TEntity>(context)
    where TEntity : BaseEntity;
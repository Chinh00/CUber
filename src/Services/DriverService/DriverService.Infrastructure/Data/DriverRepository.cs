using Core.Domain;
using Infrastructure.EfCore.Data;

namespace DriverService.Infrastructure.Data;

public class DriverRepository<TEntity>(DriverContext context) : RepositoryBase<DriverContext, TEntity>(context)
    where TEntity : BaseEntity;
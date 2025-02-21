using Core.Domain;
using Infrastructure.EfCore.Data;

namespace IdentityService.Api.Data;

public class DataRepository<TEntity>(DataContext context) : RepositoryBase<DataContext, TEntity>(context)
    where TEntity : BaseEntity;
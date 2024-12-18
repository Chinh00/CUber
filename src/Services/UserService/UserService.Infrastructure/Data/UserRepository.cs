using Core.Domain;
using Infrastructure.EfCore.Data;

namespace UserService.Infrastructure.Data;

public class UserRepository<TEntity>(UserContext context) : RepositoryBase<UserContext, TEntity>(context)
    where TEntity : BaseEntity;
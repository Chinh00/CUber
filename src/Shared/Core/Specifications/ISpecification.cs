namespace Core.Specifications;

public interface IRootSpecification
{
    
}

public interface ISpecification<T> : IRootSpecification
    where T : BaseEntity
{
    
}


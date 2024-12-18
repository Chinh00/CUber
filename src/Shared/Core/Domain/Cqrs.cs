namespace Core.Domain;

public interface ICommand<TResponse> : IRequest<IResult> where TResponse : notnull { } 
public interface IQuery<TResponse> : IRequest<IResult> where TResponse : notnull { } 

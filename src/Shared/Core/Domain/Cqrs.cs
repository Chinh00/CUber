namespace Core.Domain;

public interface ICommand<TResponse> : IRequest<IResult> where TResponse : notnull { } 
public interface IQuery<TResponse> : IRequest<IResult> where TResponse : notnull { }

public interface IListQuery<TResponse> : IQuery<TResponse> where TResponse : notnull
{
    List<FilterModel> Filters { get; set; }
}


public record FilterModel(string Field, string Comparision, string Value);

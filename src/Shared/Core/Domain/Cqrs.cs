namespace Core.Domain;

public interface ICommand<TResponse> : IRequest<ResultModel<TResponse>> where TResponse : notnull { } 
public interface IQuery<TResponse> : IRequest<IResult> where TResponse : notnull { }

public interface IListQuery<TResponse> : IQuery<TResponse> where TResponse : notnull
{
    List<FilterModel> Filters { get; set; }
}


public record FilterModel(string Field, string Comparision, string Value);

public record ResultModel<TResponse>(TResponse Data, bool IsError, string Message)
{
    public static ResultModel<TResponse> Create(TResponse data, bool isError = false, string message = default!) => new(data, isError, message);
}

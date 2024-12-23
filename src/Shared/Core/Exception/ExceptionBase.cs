namespace Core.Exception;

public class ExceptionBase : System.Exception
{
    public ExceptionBase()
    {
        
    }

    public ExceptionBase(string message) : base(message)
    {
        
    }
}
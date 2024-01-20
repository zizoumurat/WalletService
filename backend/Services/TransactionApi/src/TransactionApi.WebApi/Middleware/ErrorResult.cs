using System.Text.Json;

namespace TransactionApi.WebApi.Middleware;

public class ErrorResult : ErrorStatusCode
{
    public string Message { get; set; }
}
public class ErrorStatusCode
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class ValidationErrorDetails : ErrorStatusCode
{
    public IEnumerable<ValidatonError> Errors { get; set; }
}

public class ValidatonError
{
    public string Field { get; set; }
    public string Message { get; set; }
}
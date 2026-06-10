namespace GraGasJM.Application.Common;

public class OperationResult
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    protected OperationResult(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static OperationResult Success() => new(true, null);
    public static OperationResult Failure(string error) => new(false, error);
}

public class OperationResult<T> : OperationResult
{
    public T? Value { get; }

    private OperationResult(bool isSuccess, T? value, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static OperationResult<T> Success(T value) => new(true, value, null);
    public static new OperationResult<T> Failure(string error) => new(false, default, error);
}
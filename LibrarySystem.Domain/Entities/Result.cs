namespace LibrarySystem.Domain.Entities;
public sealed class Result<T>
{
    public Result(T value)
        => Data = value;
    public Result(Error error)
        => Error = error;
    public T? Data { get; set; } = default;
    public Error? Error { get; set; } = default;
    public bool IsSuccess => Error == null;
    public static Result<T> Success(T data) => new(data);
    public static Result<T> Failure(Error error) => new(error);
    public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure) => IsSuccess ? onSuccess(Data!) : onFailure(Error!);
}
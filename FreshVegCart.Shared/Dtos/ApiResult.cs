namespace FreshVegCart.Shared.Dtos;

public record ApiResult(bool IsSuccess, string? Error) { 
	public static ApiResult Success() => new(true, null);
	public static ApiResult Failure(string error) => new(false, error);
}
public record ApiResult<TData>(bool IsSuccess, TData Data, string? Error)
{
	public static ApiResult<TData> Success(TData data) => new(true,data, null);
	public static ApiResult<TData> Failure(string error) => new(false,default, error);
}
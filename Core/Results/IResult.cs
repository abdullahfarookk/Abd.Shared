public interface IResult<T>
{
    public T Value { get; set; }
    public bool IsValid { get; set; }
    public IEnumerable<IError>? Errors { get; set; }
}
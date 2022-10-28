﻿namespace Abd.Shared.Abstraction;

public interface IResult
{
    public bool IsSuccess { get; }
    public IEnumerable<IError> Errors { get;}
    public object? Value { get; }

}
public interface IResult<out T> : IResult
{
    public new T? Value { get;  }
}
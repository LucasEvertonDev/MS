using Microsoft.AspNetCore.Routing.Template;

namespace MS.Services.Auth.Core.Domain.Shared;

public class Result
{
    protected internal Result(bool isSucess, object error)
    {
        if(isSucess && error != null)
        {
            throw new Exception();
        }


    }

    public bool IsSucess { get; }

    public bool IsFailure => !IsSucess;

    public object Error { get; }

    //public static Result<TValue> Sucess<TValue>(TValue value) => new(value);

    public static Result Sucess() => new Result(true, null);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue? value, bool isSuccess, object error) : base(isSuccess, error) => _value = value;

    public TValue Value => IsSucess ? _value : throw new Exception("Fodeu");

    public void Teste()
    {
        // implicit faço uma classe virar outra
        string error = Error1.None;
    }

    /// <summary>
    /// https://medium.com/@gustavorestani/c-implicit-and-explicit-operators-a-comprehensive-guide-5e6972cc8671
    /// </summary>
    /// <param name="value"></param>
    //public static implicit operator Result<TValue>(TValue value) => Create(value);

    //public static Result<TValue> Create(TValue value)
    //{
    //    return new Result<TValue>(value, IsSucess, Error);
    //}
}


public class Error1 : IEquatable<Error1>
{
    public static readonly Error1 None = new(string.Empty, string.Empty);
    public static readonly Error1 NullValue = new("NullValue", "NullValue");
    public Error1(string code, string message)
    {
        this.Code = code; 
        this.Message = message;
    }

    public static implicit operator string(Error1 error) => error.Code;

    public string Code { get; }

    public string Message { get; }

    public bool Equals(Error1 other)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Error1 a, Error1 b)
    {
        return a == b;
    }

    public static bool operator !=(Error1 a, Error1 b)
    {
        return a != b;
    }
}
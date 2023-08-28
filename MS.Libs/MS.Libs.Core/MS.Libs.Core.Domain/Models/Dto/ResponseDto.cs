using MS.Libs.Core.Domain.Models.Error;

namespace MS.Libs.Core.Domain.Models.Dto;

public class ResponseDto<TResult>
{
    public bool Success { get; set; } = true;
    public int HttpCode { get; set; } = 200;
    public TResult Content { get; set; }
}
public class ResponseError<TResult>
{
    public bool Success { get; set; } = false;
    public int HttpCode { get; set; }
    public List<ErrorModel> Errors { get; set; }
}


using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Dto;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Exceptions.Base;
using MS.Libs.Infra.Utils.Exceptions;
using Refit;
using Newtonsoft.Json;

namespace MS.Services.Gateway.WebAPI.Infrastructure.Filters;

public class GatewayExceptionFilter : IExceptionFilter, IFilterMetadata
{
    private readonly ILogger<GatewayExceptionFilter> _logger;

    public GatewayExceptionFilter(ILogger<GatewayExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ApiException)
        {
            HandleRefitException(context);
        }
        else if (context.Exception is MSException)
        {
            HandleCustomExceptions(context);
            if (context.Exception is ValidatorException)
            {
                _logger.LogInformation(context.Exception, "Exception esperada controlando numero de recorrências -> " + context.Exception.Message);
            }
            else
            {
                _logger.LogWarning(context.Exception, "Exception esperada controlando numero de recorrências -> " + context.Exception.Message);
            }
        }
        else
        {
            HandleUnknownError(context);
            _logger.LogCritical(context.Exception, "Exception não esperada. Erro servero urgente internção. -> " + context.Exception.Message);
        }
    }

    private static void HandleRefitException(ExceptionContext context)
    {
        var exception = context.Exception as ApiException;

        if (!string.IsNullOrEmpty(exception.Content))
        {
            var error = JsonConvert.DeserializeObject<ResponseError<ErrorModel>>(exception.Content);

            context.HttpContext.Response.StatusCode = error.HttpCode;
            context.Result = new ObjectResult(error);
        }
    }

    private static void HandleCustomExceptions(ExceptionContext context)
    {
        if (context.Exception is BusinessException)
        {
            BusinessException ex = context.Exception as BusinessException;
            context.HttpContext.Response.StatusCode = 400;
            context.Result = new ObjectResult(new ResponseError<ErrorModel>
            {
                HttpCode = 400,
                Success = false,
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        Context = "Business",
                        Message = ex.Error.Message
                    }
                }
            });
        }
        else if (context.Exception is ValidatorException)
        {
            ValidatorException ex2 = context.Exception as ValidatorException;
            context.HttpContext.Response.StatusCode = 400;
            context.Result = new ObjectResult(new ResponseError<ErrorModel>
            {
                HttpCode = 400,
                Success = false,
                Errors = ex2.ErrorsMessages.Select((ValidatorErrorModel a) => new ErrorModel
                {
                    Context = a.Property,
                    Message = a.ErrorMessage
                }).ToList()
            });
        }
        else
        {
            MSException ex3 = context.Exception as MSException;
            context.HttpContext.Response.StatusCode = 400;
            context.Result = new ObjectResult(new ResponseError<ErrorModel>
            {
                HttpCode = 400,
                Success = false,
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        Context = "Business",
                        Message = ex3.Message
                    }
                }
            });
        }
    }

    private static void HandleUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = 500;
        context.Result = new ObjectResult(new ResponseError<ErrorModel>
        {
            HttpCode = 500,
            Success = false,
            Errors = new List<ErrorModel>
            {
                new ErrorModel
                {
                    Context = "InternalServerError",
                    Message = "Não foi possível processar sua solicitação! Por favor contate o administrador do sistema!"
                }
            }
        });
    }
}


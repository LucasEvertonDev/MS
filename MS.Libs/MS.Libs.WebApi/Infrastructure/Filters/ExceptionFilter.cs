using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MS.Libs.Core.Domain.Models.Dto;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Exceptions.Base;
using Prometheus;
using System;
using System.Diagnostics.Metrics;
using System.Net;

namespace MS.Libs.WebApi.Infrastructure.Filters;

public partial class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;
    private static readonly Counter _requisicoesErradasCliente = Metrics.CreateCounter("ms_exception_badrequest", "requisicoes erradas do clinte");
    private static readonly Counter _requisicoesBusiness = Metrics.CreateCounter("ms_exception_business", "validacao de negocio");
    private static readonly Counter _requisicoesErroo500 = Metrics.CreateCounter("ms_exception", "erro geral");
    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MSException)
        {
            HandleCustomExceptions(context);

            if (context.Exception is ValidatorException)
            {
                _requisicoesErradasCliente.Inc();
                _logger.LogInformation(context.Exception, "Exception esperada controlando numero de recorrências -> " + context.Exception.Message);
            }
            else
            {
                _requisicoesBusiness.Inc();
                _logger.LogWarning(context.Exception, "Exception esperada controlando numero de recorrências -> " + context.Exception.Message);
            }
        }
        else
        {
            _requisicoesErroo500.Inc();
            HandleUnknownError(context);
            _logger.LogCritical(context.Exception, "Exception não esperada. Erro servero urgente internção. -> " + context.Exception.Message);
        }
    }

    private static void HandleCustomExceptions(ExceptionContext context)
    {
        if (context.Exception is BusinessException)
        {
            var validationException = context.Exception as BusinessException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult
                (
                    new ResponseError<ErrorModel>()
                    { 
                        HttpCode = (int)HttpStatusCode.BadRequest,
                        Success = false,
                        Errors = new List<ErrorModel>() { new ErrorModel() { Context = "Business", Message = validationException.Error.Message  }  }
                    }
                );
        }
        else if (context.Exception is ValidatorException)
        {
            var exception = context.Exception as ValidatorException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult
                (
                    new ResponseError<ErrorModel>()
                    {
                        HttpCode = (int)HttpStatusCode.BadRequest,
                        Success = false,
                        Errors = exception.ErrorsMessages.Select(a => new ErrorModel () { Context = a.Property, Message = a.ErrorMessage }).ToList()
                    }
                );
        }
        else
        {
            var exception = context.Exception as MSException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult
                (
                    new ResponseError<ErrorModel>()
                    {
                        HttpCode = (int)HttpStatusCode.BadRequest,
                        Success = false,
                        Errors = new List<ErrorModel>() { new ErrorModel() { Context = "Business", Message = exception.Message } }
                    }
                );
        }
    }

    private static void HandleUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult
           (
                 new ResponseError<ErrorModel>()
                 {
                     HttpCode = (int)HttpStatusCode.InternalServerError,
                     Success = false,
                     Errors = new List<ErrorModel>() { new ErrorModel() { Context = "InternalServerError", Message = "Não foi possível processar sua solicitação! Por favor contate o administrador do sistema!" } }
                 }
           );
    }
}
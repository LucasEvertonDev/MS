﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Exceptions.Base;
using MS.Libs.WebApi.HttpContainers;
using System.Net;

namespace MS.Libs.WebApi.Infrastructure.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MSException)
        {
            HandleCustomExceptions(context);
        }
        else
        {
            HandleUnknownError(context);
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
                        new ResponseDTO<ErrorsModel>()
                        {
                            Sucess = false,
                            Content = new ErrorsModel(validationException.ErrorsMessages.ToArray())
                        }
                );
        }
        else
        {
            var exception = context.Exception as MSException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult
                (
                        new ResponseDTO<ErrorsModel>()
                        {
                            Sucess = false,
                            Content = new ErrorsModel(exception.Message)
                        }
                );
        }
    }

    private static void HandleUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult
           (
                   new ResponseDTO<ErrorsModel>()
                   {
                       Sucess = false,
                       Content = new ErrorsModel("Erro desconhecido")
                   }
           );
    }
}
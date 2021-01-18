using System;
using Educative.Domain.DTO;
using Educative.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Educative.Application.Utils
{
    public class ExceptionResultHandler
    {
        private static ErrorResponse error { get; set; }
        public static ObjectResult Handle(Exception exception)
        {
            switch (exception)
            {
                case NotFoundException nf:
                    error = new ErrorResponse(404, nf.Message);
                    break;
                case InvalidArgumentException ie:
                    error = new ErrorResponse(400, ie.Message);
                    break;
                default:
                    error = new ErrorResponse(500, exception.Message);
                    break;
            }
            ObjectResult result = new ObjectResult(new { Message = error.Message });
            result.StatusCode = error.StatusCode;
            return result;
        }
    }
}

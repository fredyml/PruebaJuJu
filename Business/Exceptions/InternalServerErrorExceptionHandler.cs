﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Business.Exceptions
{
    public class InternalServerErrorExceptionHandler : IExceptionHandler
    {
        public IActionResult Handle(Exception exception)
        {
            if (exception is InvalidOperationException operationException)
            {
                var errorDetails = $"An operation error occurred: {operationException.Message}";
                var errorResponse = new ObjectResult(errorDetails)
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };

                return errorResponse;
            }

            throw new ArgumentException("The exception type is not supported by this handler.", nameof(exception));
        }
    }
}

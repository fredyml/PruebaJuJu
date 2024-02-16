﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Business.Exceptions;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly Dictionary<Type, IExceptionHandler> _exceptionHandlers;
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;

            _exceptionHandlers = new Dictionary<Type, IExceptionHandler>
            {
                { typeof(SqlException), new SqlExceptionHandler() },
                { typeof(InvalidOperationException), new InternalServerErrorExceptionHandler() },
                { typeof(ArgumentException), new BadRequestExceptionHandler() },
                { typeof(NotFoundException), new NotFoundExceptionHandler() }
            };
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            Type exceptionType = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                context.Result = _exceptionHandlers[exceptionType].Handle(context.Exception);
            }
            else
            {
                context.Result = new ObjectResult("An unexpected error occurred.")
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            _logger.LogError(context.Exception.Message);
        }
    }
}

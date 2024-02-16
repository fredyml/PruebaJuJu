using Microsoft.AspNetCore.Mvc;
using System;

namespace Business.Exceptions
{
    public interface IExceptionHandler
    {
        IActionResult Handle(Exception exception);
    }
}

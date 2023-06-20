using MicroServiceProject.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServiceProject.Shared.ControllerBases
{
    public class CustomeBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)//tek tek controllerda 
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
    


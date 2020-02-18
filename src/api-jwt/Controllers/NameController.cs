using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api_jwt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class NameController : ControllerBase
    {
        readonly ITokkenManager TokkenManager;
        public NameController(ITokkenManager tokkenManager)
        {
            TokkenManager = tokkenManager;
        }

        [HttpGet]
        // Se puede poner a nivel de método o a nivel de clase
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var tokken = TokkenManager.GetTokken(this.HttpContext);
                /* Comprobations
                 if (tokken.Id != mySecretId)
                 {
                     throw new InvalidOperationException("Suplanted user");
                 }
                */

                return Ok(tokken.Name);
            }
            catch(Exception oEx)
            {
                return BadRequest(oEx);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_jwt.entities;
using JWTHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_jwt.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public class CustomLogin
        {
            public string Name { get; set; }
        }

        readonly ITokkenManager TokkenManager;
        public LoginController(ITokkenManager tokkenManager)
        {
            TokkenManager = tokkenManager;
        }

        // POST: api/Login
        [HttpPost]
        public IActionResult Post([FromBody] CustomLogin value)
        {
            try
            {
                var oToken = new JWTHelper.entities.CustomTokken
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = value.Name
                };
                var tokken = TokkenManager.Create(oToken, 10);

                return Ok(new CustomResponse<string>(tokken));
            }
            catch(Exception oEx)
            {
                return BadRequest(new CustomResponse<string>(null, oEx.ToString()));
            }
        }
    }
}

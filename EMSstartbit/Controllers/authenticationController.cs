using EMSstartbit.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMSstartbit.Models;
using BAL;
using System.Threading;

namespace EMSstartbit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authenticationController : ControllerBase
    {
        private readonly ITokenManager tokenManager;
        private readonly IuserPermissionData _userpermissiondata;

        public authenticationController(ITokenManager tokenManager,IuserPermissionData updata)
        {
            this.tokenManager = tokenManager;
            _userpermissiondata = updata;
        }
        [HttpPost]
        public async Task<IActionResult> Auth(AuthModel au)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    { 
                        Error="Invalid Input",
                        ErrorCode= "400"
                    });
                }
                if (au.name == "")
                {
                    return BadRequest(new ErrorResponse
                    {
                        Error = "Invalid Input",
                        ErrorCode = "400"
                    });
                }
                var result = await tokenManager.Authenticate(au);
                if (result.status.Code == 200)
                {
                    return Ok(new { Token = await tokenManager.NewToken(au.name),result.permissionlist });
                }
                else
                {
                    return StatusCode(result.status.Code,new ErrorResponse
                    {
                        Error = result.status.Message,
                        ErrorCode = result.status.Code.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = ex.Message,
                    ErrorCode = "400"
                });
            }
           
        }
    }
}

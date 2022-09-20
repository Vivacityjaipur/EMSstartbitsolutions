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
                        ErrorCode="L01"
                    });
                }
                var isNumeric = int.TryParse(au.name, out _);
                if (!isNumeric)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Error = "Invalid Input",
                        ErrorCode = "L01"
                    });
                }
                var eid = Convert.ToInt32(au.name);
                var result = await tokenManager.Authenticate(eid, au.password);
                if (result == "Authenticated")
                {
                    
                    var permissionlist = await _userpermissiondata.GetByEid(eid);
                    return Ok(new { Token = await tokenManager.NewToken(au.name),permissionlist });
                }
                else
                {
                    return Unauthorized(new ErrorResponse
                    {
                        Error = result,
                        ErrorCode = "L02"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = ex.Message,
                    ErrorCode = "L03"
                });
            }
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BAL;
using BOL;
using EMSstartbit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMSstartbit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class forgetController : ControllerBase
    {

        private readonly IemailControlData _iforgetdData;
        public forgetController(IemailControlData iforgetdData)
        {

            _iforgetdData = iforgetdData;
        }
        [Route("forgetpassword")]
        [HttpPost]

        public async Task<IActionResult> ForgetPassword(string id)
        {
            try
            {
                var message = await _iforgetdData.CheckId(id);
                if(message == null)
                {
                    throw new Exception("Internal Server Error");
                }
                return StatusCode(message.Code, new ErrorResponse { Error = message.Message, ErrorCode = message.Code.ToString() });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }
        [Route("confirmforgetpassword")]
        [HttpPost]
        public async Task<IActionResult> ConfirmForgetPassword(forget fo)
        {
            try
            {
                var message = await _iforgetdData.ChangePassword(fo);
                if (message == null)
                {
                    throw new Exception("Internal Server Error");
                }
                return StatusCode(message.Code, new ErrorResponse { Error = message.Message, ErrorCode = message.Code.ToString() });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }
    }
}

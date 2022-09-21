using BAL;
using BOL;
using EMSstartbit.Filters;
using EMSstartbit.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMSstartbit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class workmodeController : ControllerBase
    {
        private readonly IworkmodeData workmodedata;
        public workmodeController(IworkmodeData workmodedata)
        {

            this.workmodedata = workmodedata;
        }
        // GET: api/<workmodeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var val = await workmodedata.GetAll();
                if (val.Count() != 0 && val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return NotFound(new ErrorResponse { Error = "Data Not Found", ErrorCode = "404" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }



        // POST api/<workmodeController>
        [HttpPost]
   
        public async Task<IActionResult> Post(workmode value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                var val = await workmodedata.Insert(value);
                if (val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { Error = "Work Mode Insert Failed", ErrorCode = "500" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }


        // DELETE api/<workmodeController>/5
        [HttpDelete("{id}")]
    
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var val = await workmodedata.Delete(id);
                if (val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { Error = "Work Mode Delete Failed", ErrorCode = "404" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }
    }
}

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
    public class roleController : ControllerBase

    {
        private readonly IroleData roledata;
        public roleController(IroleData roledata)
        {
            this.roledata = roledata;
        }
        // GET: api/<roleController>
        [HttpGet]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 24,11 } })]
        public async Task<IActionResult> Get()
        {
            try
            {
                var val = await roledata.GetAll();
                if (val.Count() != 0 && val!=null)
                {
                    return Ok(val);
                }
                else
                {
                    return NotFound(new ErrorResponse { Error = "Data Not Found", ErrorCode = "404" });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500,new ErrorResponse { Error= ex.Message ,ErrorCode="500"});
            }
        }

        // GET api/<roleController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 24,11 } })]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest(new ErrorResponse {Error="Bad Request" ,ErrorCode="400"});
                }
                var val = await roledata.GetById(id);
                if (val != null)
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

        // POST api/<roleController>
        [HttpPost]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 24 } })]
        public async Task<IActionResult> Post(role value)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
              value.is_active = true;
                
                var val = await roledata.Insert(value);
                if (val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return StatusCode(500,new ErrorResponse { Error = "Role Insert Failed", ErrorCode = "500" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        // PUT api/<roleController>/5
        [HttpPut("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 24 } })]
        public async Task<IActionResult> Put(int id,role value)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                
                value.role_id = id;
                    var valnew = await roledata.Edit(value);
                    if (valnew != null)
                    {
                        return Ok(valnew);
                    }
                    else
                    {
                        return StatusCode(500, new ErrorResponse { Error = "Role Update Failed", ErrorCode = "500" });
                    }   
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        // DELETE api/<roleController>/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 24 } })]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var val = await roledata.Delete(id);
                    if (val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { Error = "Role Delete Failed", ErrorCode = "404" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }
    }
}

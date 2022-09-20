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

    public class departmentController : ControllerBase
    {
        private readonly IdepartmentData departmentdata;
        public departmentController(IdepartmentData departmentdata)
        {

            this.departmentdata = departmentdata;
        }
        // GET: api/<departmentController>
        [HttpGet]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 11,18 } })]
        public async Task<IActionResult> Get()
        {
            try
            {
                var val = await departmentdata.GetAll();
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

        // GET api/<departmentController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] {11,18} })]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                var val = await departmentdata.GetById(id);
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

        // POST api/<departmentController>
        [HttpPost]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 18 } })]
        public async Task<IActionResult> Post(department value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                value.is_active = true;
                //if (value.is_active == null)
                //{
                //    value.is_active = true;
                //}
                var val = await departmentdata.Insert(value);
                if (val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { Error = "Department Insert Failed", ErrorCode = "500" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        // PUT api/<departmentController>/5
        [HttpPut("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] {18 } })]
        public async Task<IActionResult> Put(int id, department value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                value.department_id = id;
                var valnew = await departmentdata.Update(value);
                if (valnew != null)
                {
                    return Ok(valnew);
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { Error = "Department Update Failed", ErrorCode = "500" });
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        // DELETE api/<departmentController>/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 18 } })]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var val = await departmentdata.Delete(id);
                if (val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { Error = "Department Delete Failed", ErrorCode = "404" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }
    }
}

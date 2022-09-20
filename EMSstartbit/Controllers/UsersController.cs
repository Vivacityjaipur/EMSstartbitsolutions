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
 /*
    [TokenAuthenticationFilter(1)]*/
    
    public class usersController : ControllerBase
    {
        private IemployeeData _ldata;
        public usersController(IemployeeData ldata)
        {
            _ldata = ldata;
        }
        // GET: api/<UsersController>
        [HttpGet]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 25 } })]
        public async Task<IActionResult> Get()
        {
            try
            {
                var val = await _ldata.GetAll();
                if (val.Count() != 0 && val != null)
                {
                    var empData = from res in val
                                  select new { res.employee_id,employee_name = res.firstname +" "+ res.middlename +" "+ res.lastname,role_id=res.role_id };
                    return Ok(empData);
                }
                else
                {
                    return NotFound(new ErrorResponse{ Error = "Data Not Found", ErrorCode = "404" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        //// GET api/<UsersController>/5
        //[TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { 2 })]
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UsersController>
        //[TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { 3 })]
        //[HttpPost]
        //public string Post([FromBody] string value)
        //{
        //    return value;
        //}

        //// PUT api/<UsersController>/5
        //[TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { 4 })]
        //[HttpPut("{id}")]
        //public string Put(int id, [FromBody] string value)
        //{
        //    return value;
        //}

        //// DELETE api/<UsersController>/5
        //[TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { 5 })]
        //[HttpDelete("{id}")]
        //public int Delete(int id)
        //{
        //    return id;
        //}
     
    }
}

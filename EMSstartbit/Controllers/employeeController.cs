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
    public class employeeController : ControllerBase

    {
    
        private readonly IemployeeData employeedata;
        private readonly IrolePermissionData rolepdata;
        private readonly IuserPermissionData userpdata;
        private readonly IloginData logindata;
        public employeeController( IemployeeData employeedata, IrolePermissionData rolepdata, IuserPermissionData userpdata, IloginData logindata)
        {
        
            this.employeedata = employeedata;
            this.rolepdata = rolepdata;
            this.userpdata = userpdata;
            this.logindata = logindata;
        }
        // GET: api/<employeeController>
        [HttpGet]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 14 } })]
        public async Task<IActionResult> Get()
        {
            try
            {
                var val = await employeedata.GetAll();
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

        // GET api/<employeeController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 14 } })]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                var val = await employeedata.GetById(id);
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

        // POST api/<employeeController>
        [HttpPost]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 11 } })]
        public async Task<IActionResult> Post(employee value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                value.is_active = true;
                value.created_date = DateTime.Now;
                //if (value.is_active == null)
                //{
                //    value.is_active = true;
                //}
                var PermissionsList = await rolepdata.GetByroleid(value.role_id);
                List<UserPermission> Ups = new List<UserPermission>();
                
                foreach (var el in PermissionsList)
                {
                    UserPermission up = new UserPermission();
                    up.employee_id = value.employee_id;
                    up.role_id = value.role_id;
                    up.permission_id = el.permission_id;
                    Ups.Add(up);
                }
                await userpdata.InsertMultiple(Ups);
                var val = await employeedata.Insert(value);
                if (val != null)
                {
                    var login = await logindata.Insert(new login {employee_id=val.employee_id,created_date=val.created_date,password="root@123",role_id=val.role_id,is_active=val.is_active });
                    return Ok(val);
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { Error = "Employee Insert Failed", ErrorCode = "500" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        // PUT api/<employeeController>/5
        [HttpPut("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 11} })]
        public async Task<IActionResult> Put(int id, employee value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }
                value.employee_id = id;
                    var valnew = await employeedata.Update(value);
                    if (valnew != null)
                    {
                        return Ok(valnew);
                    }
                    else
                    {
                        return StatusCode(500, new ErrorResponse { Error = "employee Update Failed", ErrorCode = "500" });
                    }
                
               

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        // DELETE api/<employeeController>/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 11 } })]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var val = await employeedata.Delete(id);
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

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
    public class rolePermissionController : ControllerBase
    {
        private readonly IrolePermissionData rolePermissiondata;
        private readonly IemployeeData employeedata;
        private readonly IuserPermissionData userPermissiondata;
        public rolePermissionController(IuserPermissionData userPermissiondata, IemployeeData employeedata, IrolePermissionData rolePermissiondata)
        {
            this.userPermissiondata = userPermissiondata;
            this.employeedata = employeedata;
            this.rolePermissiondata = rolePermissiondata;
        }
        // GET: api/<rolePermissionController>
        [HttpGet("{id}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 26 } })]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var val = await rolePermissiondata.GetByroleid(id);
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

        //// GET api/<roleController>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
        //        }
        //        var val = await roledata.GetById(id);
        //        if (val != null)
        //        {
        //            return Ok(val);
        //        }
        //        else
        //        {
        //            return NotFound(new ErrorResponse { Error = "Data Not Found", ErrorCode = "404" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
        //    }
        //}

        //// POST api/<roleController>
        //[HttpPost]
        //public async Task<IActionResult> Post(role value)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
        //        }

        //        value.created_date = DateTime.Now;
        //        value.is_active = true;

        //        var val = await roledata.Insert(value);
        //        if (val != null)
        //        {
        //            return Ok(val);
        //        }
        //        else
        //        {
        //            return StatusCode(500, new ErrorResponse { Error = "Role Insert Failed", ErrorCode = "500" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
        //    }
        //}

        //// PUT api/<roleController>/5
        [HttpPut()]
        [Route("{id:int}/{istrue:bool}")]
        [TypeFilter(typeof(TokenAuthenticationFilter), Arguments = new object[] { new int[] { 26 } })]
        public async Task<IActionResult> Put(int id,bool istrue,List<int> permission_receive)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse { Error = "Bad Request", ErrorCode = "400" });
                }

                //var emp = await employeedata.GetById(id);
                var roleids = await rolePermissiondata.GetPermissionIdsByroleid(id);
                var ids = permission_receive.Select(x => x).Intersect(roleids.Select(x => x));
                var toadd = permission_receive.Where(p => !ids.Any(p2 => p2 == p));
                var todelete = roleids.Where(p => !ids.Any(p2 => p2 == p));
                var deletedRolePremissions =await rolePermissiondata.DeleteByRoleAndPermissionids(id, todelete);
                var addedRolePremissions =await rolePermissiondata.AddByRoleAndPermissionids(id, toadd);
                if (istrue)
                {
                    var emploeidlist = await employeedata.GetEmployeeIdswithRoleids(id);
                    var deleteduserpermissionlist = await userPermissiondata.DeleteByRoleAndPermissionids(id, todelete);
                    var addeduserpermisionlist = await userPermissiondata.AddByRoleAndPermissionidsAndEmp(emploeidlist, id, toadd);

                }
                return Ok("Changed Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
            }
        }

        //// DELETE api/<roleController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var val = await roledata.Delete(id);
        //        if (val != null)
        //        {
        //            return Ok(val);
        //        }
        //        else
        //        {
        //            return StatusCode(500, new ErrorResponse { Error = "Role Delete Failed", ErrorCode = "404" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ErrorResponse { Error = ex.Message, ErrorCode = "500" });
        //    }
        //}
    }
}

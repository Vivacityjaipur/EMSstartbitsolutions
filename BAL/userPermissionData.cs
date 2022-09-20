using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BAL
{
    public class userPermissionData : IuserPermissionData
    {
        private readonly IUnitOfWork _unitofwork;
        public userPermissionData(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;

        }
        public async Task<IEnumerable<UserPermission>> GetAll()
        {
            await _unitofwork.permissions.GetData();
            await _unitofwork.roles.GetData();
            await _unitofwork.logins.GetData();
            return await _unitofwork.UserPermissions.GetData();
        }
        public async Task<UserPermission> Insert(UserPermission u)
        {
            var result = await _unitofwork.UserPermissions.AddData(u);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);
        }
        public async Task<IEnumerable<UserPermission>> InsertMultiple(IEnumerable<UserPermission> u)
        {
            var result  = await _unitofwork.UserPermissions.AddMultipleData(u);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);
        }
        public async Task<UserPermission> Update(UserPermission u)
        {
            var result =  await _unitofwork.UserPermissions.EditData(u);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);
        }
        public async Task<UserPermission> GetById(int id)
        {
            return await _unitofwork.UserPermissions.GetDataById(id);
        }
        public async Task<UserPermission> Delete(int id)
        {
            var result =  await _unitofwork.UserPermissions.DeleteData(id);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);
        }
        public async Task<IEnumerable<UserPermission>> DeleteMultiple(List<int> id)
        {
            var result= await _unitofwork.UserPermissions.DeleteMultipleData(id);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);
        }
        public async Task<List<int>> GetAllUserPermisssionIdByEid(int id)
        {

            List<int> vs = new List<int>();
            var x = await this.GetAll();
            await Task.Run(() =>
             {
                 foreach (var el in x)
                 {
                     if (el.employee_id == id)
                     {
                         vs.Add(el.user_permission_id);
                     }
                 }
             });
            return vs;
        }
     
        public async Task<List<permission>> GetByEid(int id)
        {

            List<permission> vs = new List<permission>();
            var x = await this.GetAll();
            await Task.Run(() =>
            {
                foreach (var el in x)
                {
                    if (el.employee_id == id)
                    {
                        if (el.permissions.is_active == true)
                        {
                            vs.Add(el.permissions);
                        }
                    }
                }
            });
            return vs;
        }
        public async Task<IEnumerable<UserPermission>> DeleteByRoleAndPermissionids(int roleid, IEnumerable<int> pids)
        {
            var x = await this.GetAll();
            var result = await Task.Run(() => { return x.Where(u => u.role_id == roleid && pids.Contains(u.permission_id)).Select(u => u.user_permission_id); });
            var resultedlist = await _unitofwork.UserPermissions.DeleteMultipleData(result);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? resultedlist : null);
        }
        public async Task<IEnumerable<UserPermission>> AddByRoleAndPermissionidsAndEmp(IEnumerable<int> emplist,int roleid, IEnumerable<int> pids)
        {
            List<UserPermission> rplist = new List<UserPermission>();
            await Task.Run(() =>
            {
                foreach (var y in emplist)
                {
                    foreach (var i in pids)
                    {
                        UserPermission r = new UserPermission();
                        r.permission_id = i;
                        r.role_id = roleid;
                        r.employee_id = y;
                        rplist.Add(r);
                    }

                }
            });
            var result = await _unitofwork.UserPermissions.AddMultipleData(rplist);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);
        }


    }
}

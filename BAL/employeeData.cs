using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using BOL;
using System.Threading.Tasks;
using System.Linq;

namespace BAL
{
    public class employeeData : IemployeeData
    {
        private readonly IUnitOfWork _unitofwork;
        public employeeData(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public async Task<IEnumerable<employee>> GetAll()
        {
            await _unitofwork.roles.GetData();
            await _unitofwork.departments.GetData();
            await _unitofwork.designations.GetData();
            return await _unitofwork.employees.GetData();
        }
        public async Task<employee> Insert(employee u)
        {
            var result = await _unitofwork.employees.AddData(u);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);

        }
        public async Task<employee> Update(employee u)
        {
            var result = await _unitofwork.employees.EditData(u);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);

        }
        public async Task<employee> GetById(int id)
        {
            var x = await _unitofwork.employees.GetByExpression(u => u.employee_id == id);
            return x;
        }
        public async Task<employee> GetByEmailId(string id)
        {
            var x = await _unitofwork.employees.GetByExpression(u => u.officeemail == id);
            return x;
        }
        public async Task<employee> Delete(int id)
        {
            var result= await _unitofwork.employees.DeleteData(id);
            var resultcheck = await _unitofwork.CompleteAsync();
            return await Task.Run(() => (resultcheck) ? result : null);
        }
        public async Task<IEnumerable<int>> GetEmployeeIdswithRoleids(int roleid)
        {
            var x = await _unitofwork.employees.GetData();
            var result = x.Where(u => u.role_id == roleid).Select(f=>f.employee_id);
            return result;
        }
    }
}

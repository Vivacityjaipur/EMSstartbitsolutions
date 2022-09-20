using BOL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class UnitOfWork :IUnitOfWork
    {
        private EMSDataContext _context;
        private Repository<login> _logins;
        private Repository<role> _roles;
        private Repository<permission> _permissions;
        private Repository<UserPermission> _UserPermissions;
        private Repository<RolePermission> _RolePermissions;
        private Repository<employee> _employees;
        private Repository<designation> _designations;
        private Repository<department> _departments;
        private Repository<shift> _shifts;
        private Repository<test> _tests;

        public UnitOfWork(EMSDataContext context)
        {
            _context = context;
        }
        public IRepository<employee> employees
        {
            get
            {
                return _employees ??
                    (_employees = new Repository<employee>(_context));
            }
        }
        public IRepository<test> tests
        {
            get
            {
                return _tests ??
                    (_tests = new Repository<test>(_context));
            }
        }
        public IRepository<shift> shifts
        {
            get
            {
                return _shifts ??
                    (_shifts = new Repository<shift>(_context));
            }
        }
        public IRepository<department> departments
        {
            get
            {
                return _departments ??
                    (_departments = new Repository<department>(_context));
            }
        }
        public  IRepository<designation> designations
        {
            get
            {
                return _designations ??
                    (_designations = new Repository<designation>(_context));
            }
        }
        public IRepository<permission> permissions
        {
            get
            {
                return _permissions ??
                    (_permissions = new Repository<permission>(_context));
            }
        }
        public IRepository<UserPermission> UserPermissions
        {
            get
            {
                return _UserPermissions ??
                    (_UserPermissions = new Repository<UserPermission>(_context));
            }
        }
        public IRepository<RolePermission> RolePermissions
        {
            get
            {
                return _RolePermissions ??
                    (_RolePermissions = new Repository<RolePermission>(_context));
            }
        }
        public IRepository<login> logins
        {
            get
            {
                return _logins ??
                    (_logins = new Repository<login>(_context));
            }
        }
        public IRepository<role> roles
        {
            get
            {
                return _roles ??
                    (_roles = new Repository<role>(_context));
            }
        }
        //public void Commit()
        //{
        //    _context.SaveChanges();
        //}

        public async Task<bool> CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}

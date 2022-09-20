using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public interface IUnitOfWork
    {
        IRepository<login> logins { get; }
        IRepository<role> roles { get; }
        IRepository<permission> permissions { get; }
        IRepository<UserPermission> UserPermissions { get; }
        IRepository<RolePermission> RolePermissions { get; }
        IRepository<employee> employees { get; }
        IRepository<department> departments { get; }
        IRepository<designation> designations { get; }
        IRepository<shift> shifts { get; }
        IRepository<test> tests { get; }
        // void Commit();
        Task<bool> CompleteAsync();
        void Dispose();

    }
}

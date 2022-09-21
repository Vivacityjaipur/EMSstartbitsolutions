using System;
using Microsoft.EntityFrameworkCore;
using BOL;

namespace DAL
{
    public class EMSDataContext : DbContext
    {
        public EMSDataContext(DbContextOptions<EMSDataContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<login>(entity => {
            //    // 1 - Many
            //    //entity.HasOne(t => t.role)
            //    //    .WithMany(d => d.logins)
            //    //    .HasForeignKey(x => x.role_id)
            //    //    .OnDelete(DeleteBehavior.Restrict)
            //    //    .HasConstraintName("login_role_id_fkey");

            //    // 1 - 1
            //    entity.HasOne(dm => dm.role)
            //            .WithMany(d => d.logins)
            //            .HasForeignKey(x => x.role_id);
            //});
        }   

        public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<permission> permissionss { get; set; }
        public virtual  DbSet<role> roles { get; set; }
        public virtual DbSet<RolePermission> role_permissions { get; set; }
        public virtual DbSet<UserPermission> user_permissions { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<designation> designations { get; set; }
        public virtual DbSet<employee> emlpoyees { get; set; }
        public virtual DbSet<shift> shifts { get; set; }
        public virtual DbSet<test> tests { get; set; }



        //Masters
        public virtual DbSet<bloodgroup> bloodgroups { get; set; }
        public virtual DbSet<workmode> workmodes { get; set; }

    }
}

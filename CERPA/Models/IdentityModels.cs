using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace CERPA.Models
{
    
    public class ApplicationUserLogin : IdentityUserLogin<string> { }
    public class ApplicationUserClaim : IdentityUserClaim<string> { }
    public class ApplicationUserRole : IdentityUserRole<string> { }

    
    public class ApplicationUser
        : IdentityUser<string, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            
        }
        


        public async Task<ClaimsIdentity>
            GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }


    
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        
    }


    
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public virtual IDbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public virtual DbSet<InventoryItem> Inventory { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PartProcess> PartProcesses { get; set; }
        public virtual DbSet<PartProperty> PartProperties { get; set; }
        public virtual DbSet<PartStructure> PartStructures { get; set; }
        public virtual DbSet<ConfigurableAssemblyVariable> ConfigurableAssemblyVariables {get;set;}
        public virtual DbSet<VariableExpression> VariableExpressions { get; set; }
        public virtual DbSet<VariableValue> VariableValues { get; set; }
        public virtual DbSet<QuantityExpressionValue> QuantityExpressionValues { get; set; }
        public virtual DbSet<PropertyValue> PropertyValues { get; set; }
        public virtual DbSet<PickOrder> PickOrders { get; set; }
        public virtual DbSet<ApplicationGroupRole> GroupRoles { get; set; }
        public virtual DbSet<ApplicationUserGroup> ApplicationUsers { get; set; }
        public virtual DbSet<InventoryTransaction> InventoryTansactions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            // Map Users to Groups:
            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)
                .WithRequired()
                .HasForeignKey<string>((ApplicationUserGroup ag) => ag.ApplicationGroupId);
            modelBuilder.Entity<ApplicationUserGroup>()
                .HasKey((ApplicationUserGroup r) =>
                    new
                    {
                        r.ApplicationUserId,
                        r.ApplicationGroupId
                    }).ToTable("ApplicationUserGroups");

            // Map Roles to Groups:
            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)
                .WithRequired()
                .HasForeignKey<string>((ApplicationGroupRole ap) => ap.ApplicationGroupId);
            modelBuilder.Entity<ApplicationGroupRole>().HasKey((ApplicationGroupRole gr) =>
                new
                {
                    gr.ApplicationRoleId,
                    gr.ApplicationGroupId
                }).ToTable("ApplicationGroupRoles");
        }

        public System.Data.Entity.DbSet<CERPA.Models.AssemblyProfile> AssemblyProfiles { get; set; }

        public System.Data.Entity.DbSet<CERPA.Models.ChildQuantityExpression> ChildQuantityExpressions { get; set; }

        
    }


    public class ApplicationUserStore
        : UserStore<ApplicationUser, ApplicationRole, string,
            ApplicationUserLogin, ApplicationUserRole,
            ApplicationUserClaim>, IUserStore<ApplicationUser, string>,
        IDisposable
    {
        public ApplicationUserStore()
            : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationRoleStore
    : RoleStore<ApplicationRole, string, ApplicationUserRole>,
    IQueryableRoleStore<ApplicationRole, string>,
    IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }

}
public class ApplicationGroup
{
    public ApplicationGroup()
    {
        this.Id = Guid.NewGuid().ToString();
        this.ApplicationRoles = new List<ApplicationGroupRole>();
        this.ApplicationUsers = new List<ApplicationUserGroup>();
    }

    public ApplicationGroup(string name)
        : this()
    {
        this.Name = name;
    }

    public ApplicationGroup(string name, string description)
        : this(name)
    {
        this.Description = description;
    }
    

    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
    public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
}


public class ApplicationUserGroup
{
    public string ApplicationUserId { get; set; }
    public string ApplicationGroupId { get; set; }
}

public class ApplicationGroupRole
{
    public string ApplicationGroupId { get; set; }
    public string ApplicationRoleId { get; set; }
}
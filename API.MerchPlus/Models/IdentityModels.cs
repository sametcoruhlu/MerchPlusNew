using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace API.MerchPlus.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string NameSurname { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string SocialSecurityNumber { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string DataNumber { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? HiringDate { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? LeavingDate { get; set; }

        [Column(TypeName = "BIT")]
        public bool IsActive { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(255)]
        public string Address { get; set; }

        [Column(TypeName = "INT")]
        public int? DeviceModelId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string IMEINumber { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(255)]
        public string ProfilePicturePath { get; set; }

        [Column(TypeName = "INT")]
        public int MemberTitleId { get; set; }

        [Column(TypeName = "INT")]
        public int? AddressCityId { get; set; }

        [Column(TypeName = "DECIMAL")]
        [DecimalPrecision(22, 8)]
        public decimal? AddressCoordinateX { get; set; }

        [Column(TypeName = "DECIMAL")]
        [DecimalPrecision(22, 8)]
        public decimal? AddressCoordinateY { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(255)]
        public string DirectReportId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<ApplicationUser>().ToTable("Member");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("MemberRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("MemberClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("MemberLogin");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class DecimalPrecisionAttribute : Attribute
{
    public DecimalPrecisionAttribute(byte precision, byte scale)
    {
        Precision = precision;
        Scale = scale;

    }

    public byte Precision { get; set; }
    public byte Scale { get; set; }

}
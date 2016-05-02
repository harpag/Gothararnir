using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mooshak2_Hopur5.Models.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Mooshak2_Hopur5.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AnnouncementEntity> Announcement { get; set; }
        public DbSet<AssignmentEntity> Assignment { get; set; }
        public DbSet<AssignmentPartEntity> AssignmentPart { get; set; }
        public DbSet<AssignmentTestCaseEntity> AssignmentTestCase { get; set; }
        public DbSet<CourseEntity> Course { get; set; }
        public DbSet<CourseTeacherEntity> CourseTeacher { get; set; }
        public DbSet<DiscussionEntity> Discussion { get; set; }
        public DbSet<ProgrammingLanguageEntity> ProgrammingLanguage { get; set; }
        public DbSet<SemesterEntity> Semester { get; set; }
        public DbSet<SubmissionEntity> Submission { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<UserAssignmentEntity> UserAssignment { get; set; }
        public DbSet<UserCourseEntity> UserCourse { get; set; }
        public DbSet<UserGroupEntity> UserGroup { get; set; }
        public DbSet<UserGroupMemberEntity> UserMember { get; set; }
        public DbSet<UserLoginEntity> UserLogin { get; set; }
        public DbSet<UserTypeEntity> UserType { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<AssignmentFile> AssignmentFile { get; set; }
        public virtual DbSet<AssignmentPartFile> AssignmentPartFile { get; set; }
        public virtual DbSet<AssignmentPart> AssignmentPart { get; set; }
        public virtual DbSet<AssignmentTestCase> AssignmentTestCase { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseTeacher> CourseTeacher { get; set; }
        public virtual DbSet<Discussion> Discussion { get; set; }
        public virtual DbSet<ProgrammingLanguage> ProgrammingLanguage { get; set; }
        public virtual DbSet<Semester> Semester { get; set; }
        public virtual DbSet<Submission> Submission { get; set; }
        public virtual DbSet<SubmissionFile> SubmissionFile { get; set; }
        public virtual DbSet<UserAssignment> UserAssignment { get; set; }
        public virtual DbSet<UserCourse> UserCourse { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserGroupMember> UserGroupMember { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>()
                .Property(e => e.announcement)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.CourseTeacher)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Discussion)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.UserAssignment)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.UserCourse)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.UserGroupMember)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Assignment>()
                .Property(e => e.assignmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Assignment>()
                .Property(e => e.assignmentDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.AssignmentFile1)
                .WithRequired(e => e.Assignment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.AssignmentPart)
                .WithRequired(e => e.Assignment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.Discussion)
                .WithRequired(e => e.Assignment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssignmentFile>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentFile>()
                .Property(e => e.pathThumb)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentFile>()
                .Property(e => e.fileType)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentFile>()
                .Property(e => e.fileExtension)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentPart>()
                .Property(e => e.assignmentPartName)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentPart>()
                .Property(e => e.assignmentPartDescription)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentPart>()
                .HasMany(e => e.AssignmentTestCase)
                .WithRequired(e => e.AssignmentPart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssignmentPart>()
                .HasMany(e => e.Submission)
                .WithRequired(e => e.AssignmentPart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssignmentTestCase>()
                .Property(e => e.input)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentTestCase>()
                .Property(e => e.output)
                .IsUnicode(false);

            modelBuilder.Entity<AssignmentTestCase>()
                .HasMany(e => e.Submission)
                .WithOptional(e => e.AssignmentTestCase)
                .HasForeignKey(e => e.testCaseFailId);

            modelBuilder.Entity<Course>()
                .Property(e => e.courseNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.courseName)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Assignment)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseTeacher)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.UserCourse)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.UserGroup)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discussion>()
                .Property(e => e.discussionText)
                .IsUnicode(false);

            modelBuilder.Entity<ProgrammingLanguage>()
                .Property(e => e.programmingLanguageName)
                .IsUnicode(false);

            modelBuilder.Entity<ProgrammingLanguage>()
                .HasMany(e => e.AssignmentPart)
                .WithRequired(e => e.ProgrammingLanguage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .Property(e => e.semesterNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Semester>()
                .Property(e => e.semesterName)
                .IsUnicode(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.Course)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Submission>()
                .Property(e => e.submissionComment)
                .IsUnicode(false);

            modelBuilder.Entity<Submission>()
                .Property(e => e.error)
                .IsUnicode(false);
            
            modelBuilder.Entity<UserAssignment>()
                .Property(e => e.gradeComment)
                .IsUnicode(false);

            modelBuilder.Entity<UserAssignment>()
                .HasMany(e => e.Submission)
                .WithRequired(e => e.UserAssignment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.userGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.UserAssignment)
                .WithRequired(e => e.UserGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.UserGroupMember)
                .WithRequired(e => e.UserGroup)
                .WillCascadeOnDelete(false);
        }
    }
}
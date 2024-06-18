using Microsoft.EntityFrameworkCore;
using Out_of_Office.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Infrastructure.Presistance
{
    public class Out_of_OfficeDbContext : DbContext
    {
        public Out_of_OfficeDbContext(DbContextOptions<Out_of_OfficeDbContext> options)
        : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FullName)
                      .IsRequired()
                      .IsUnicode(false)
                      .HasMaxLength(100);

                entity.Property(e => e.Subdivision)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Position)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasConversion<string>();

                entity.Property(e => e.OutOfOfficeBalance)
                      .IsRequired()
                      .HasDefaultValue(0);

                entity.Property(e => e.Photo)
                      .HasColumnType("varbinary(max)");
            });

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.HasOne(e => e.Employee)
                      .WithMany()
                      .HasForeignKey(e => e.EmployeeID)
                      .IsRequired();

                entity.Property(e => e.AbsenceReason)
                      .IsRequired();

                entity.Property(e => e.StartDate)
                      .IsRequired();

                entity.Property(e => e.EndDate)
                      .IsRequired();

                entity.Property(e => e.Comment)
                      .HasMaxLength(500);

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasConversion<string>();
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ProjectType)
                      .IsRequired();

                entity.Property(e => e.StartDate)
                      .IsRequired();

                entity.Property(e => e.EndDate)
                      .IsRequired(false);

                entity.HasOne(e => e.ProjectManager)
                      .WithMany()
                      .HasForeignKey(e => e.ProjectManagerID)
                      .IsRequired();

                entity.Property(e => e.Comment)
                      .HasMaxLength(500);

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasConversion<string>();  
            });

            modelBuilder.Entity<ApprovalRequest>(entity =>
            {
                entity.HasKey(e => e.ID);  

                entity.HasOne(e => e.Approver)
                      .WithMany() 
                      .HasForeignKey(e => e.ApproverID)  
                      .IsRequired();

                entity.HasOne(e => e.LeaveRequest)
                      .WithMany()  
                      .HasForeignKey(e => e.LeaveRequestID) 
                      .IsRequired();

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasConversion<string>(); 
                entity.Property(e => e.Comment)
                      .HasMaxLength(1000);  
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}


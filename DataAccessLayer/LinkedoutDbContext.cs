using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccessLayer
{ 

public partial class LinkedoutDbContext : IdentityDbContext<Anvandare>
{
    public LinkedoutDbContext()
    {
    }

    public LinkedoutDbContext(DbContextOptions<LinkedoutDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Competence> Competences { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfileHasEducation> ProfileHasEducations { get; set; }

    public virtual DbSet<ProfileHasExperience> ProfileHasExperiences { get; set; }
    public virtual DbSet<ProfileinProject> ProfileinProjects { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Anvandare> Anvandares { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Competence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__competen__3214EC27203929FD");

            entity.ToTable("competence");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__educatio__3214EC2721B925D8");

            entity.ToTable("education");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__experien__3214EC27D02F9207");

            entity.ToTable("experience");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__message__3214EC2753A3CFF9");

            entity.ToTable("message");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(200)
                .HasColumnName("content");
            entity.Property(e => e.Reciever).HasColumnName("reciever");
            entity.Property(e => e.Seen).HasColumnName("seen");
            entity.Property(e => e.Times)
                .HasColumnType("datetime")
                .HasColumnName("times");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.RecieverNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Reciever)
                .HasConstraintName("fk_message_reciever");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profile__3214EC27AD0E7C31");

            entity.ToTable("Profile");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.PicUrl).HasMaxLength(200);
            entity.Property(e => e.UserName).HasMaxLength(450);

            entity.HasOne(e => e.user).WithOne(a => a.Profile).HasPrincipalKey<Profile>(a => a.UserName);
            entity.HasMany(d => d.Competences).WithMany(p => p.Profiles)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfileHasCompetence",
                    r => r.HasOne<Competence>().WithMany()
                        .HasForeignKey("Competenceid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_competence_id"),
                    l => l.HasOne<Profile>().WithMany()
                        .HasForeignKey("Profileid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_profilesss_id"),
                    
                    j =>
                    {
                        j.HasKey("Profileid", "Competenceid").HasName("PK__profile___809453B2A4295BDF");
                        j.ToTable("profile_has_competence");
                    });
        });

        modelBuilder.Entity<ProfileHasEducation>(entity =>
        {
            entity.HasKey(e => new { e.Profileid, e.Educationid }).HasName("PK__profile___60E42F3DC720B0F8");

            entity.ToTable("profile_has_education");

            entity.Property(e => e.Profileid).HasColumnName("profileid");
            entity.Property(e => e.Educationid).HasColumnName("educationid");
            entity.Property(e => e.Enddate)
                .HasColumnType("date")
                .HasColumnName("enddate");
            entity.Property(e => e.Startdate)
                .HasColumnType("date")
                .HasColumnName("startdate");

            entity.HasOne(d => d.Education).WithMany(p => p.ProfileHasEducations)
                .HasForeignKey(d => d.Educationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_education_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.ProfileHasEducations)
                .HasForeignKey(d => d.Profileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_profilessss_id");
        });
        modelBuilder.Entity<ProfileinProject>(entity =>
        {
            entity.HasKey(e => new { e.Profileid, e.Projectid }).HasName("PK__profile_ProjectID");

            entity.ToTable("profile_in_Project");

            entity.Property(e => e.Profileid).HasColumnName("profileid");
            entity.Property(e => e.Projectid).HasColumnName("ProjectID");
            

            entity.HasOne(d => d.Project).WithMany(p => p.ProfileinProjects)
                .HasForeignKey(d => d.Projectid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.ProfileinProjects)
                .HasForeignKey(d => d.Profileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_profilessss_id");
        });

        modelBuilder.Entity<ProfileHasExperience>(entity =>
        {
            entity.HasKey(e => new { e.Profileid, e.Experienceid }).HasName("PK__profile___09F2273E03F81676");

            entity.ToTable("profile_has_experience");

            entity.Property(e => e.Profileid).HasColumnName("profileid");
            entity.Property(e => e.Experienceid).HasColumnName("experienceid");
            entity.Property(e => e.Enddate)
                .HasColumnType("date")
                .HasColumnName("enddate");
            entity.Property(e => e.Startdate)
                .HasColumnType("date")
                .HasColumnName("startdate");

            entity.HasOne(d => d.Experience).WithMany(p => p.ProfileHasExperiences)
                .HasForeignKey(d => d.Experienceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_experience_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.ProfileHasExperiences)
                .HasForeignKey(d => d.Profileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_profiless_id");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project__3214EC2729578260");

            entity.ToTable("project");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Creator).WithMany(p => p.ProjectsNavigation)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_creator_id");

            entity.HasMany(d => d.profiles).WithMany(p => p.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfileInProject",
                    r => r.HasOne<Profile>().WithMany()
                        .HasForeignKey("Profileid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_profiles_id"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("Projectid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_project_id"),
                    j =>
                    {
                        j.HasKey("Projectid", "Profileid").HasName("PK__profile___16385FD84E45517A");
                        j.ToTable("profile_in_project");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC2775F0E23F");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.ProfileId).HasColumnName("profileID");
            entity.Property(e => e.Username).HasMaxLength(200);

            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

}
using System;
using System.Collections.Generic;
using System.Text;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EVTHJÄLPEN.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<EventDetails> EventDetails { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Orderdetails> Orderdetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeDetails> RecipeDetails { get; set; }
        public virtual DbSet<RecipeType> RecipeType { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = TranbarDB; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDetails)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK__EventDeta__Event__3C69FB99");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.EventDetails)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__EventDeta__Recip__3B75D760");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EventName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EventType)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orderdetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OrdersId).HasColumnName("OrdersID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Orders)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrdersId)
                    .HasConstraintName("FK__Orderdeta__Order__2D27B809");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Orderdeta__Produ__2E1BDC42");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserID__25869641");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__2A4B4B5E");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecipeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeTypeId).HasColumnName("RecipeTypeID");

                entity.HasOne(d => d.RecipeType)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.RecipeTypeId)
                    .HasConstraintName("FK__Recipe__RecipeTy__32E0915F");
            });

            modelBuilder.Entity<RecipeDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RecipeDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__RecipeDet__Produ__36B12243");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeDetails)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__RecipeDet__Recip__35BCFE0A");
            });

            modelBuilder.Entity<RecipeType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecipeTypeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
            
        }
    }
}

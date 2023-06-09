﻿using System;
using System.Collections.Generic;
using BirdieDotnetAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BirdieDotnetAPI.Data;

/* 
*    Generated using dotnet CLI
*    dotnet ef dbcontext scaffold "(connection string here)" Pomelo.EntityFrameworkCore.MySql --context-dir Data --output-dir ModelsEF
*/

#nullable disable

//? 'Test' refers to the current database name.
//TODO migrate database to a more structured schema
public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
        
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<RefreshToken> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();


            string connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("10.4.28-mariadb"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("conversations");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("messages");

            entity.HasIndex(e => e.ConversationId, "conversation_id");

            entity.HasIndex(e => e.SenderId, "sender_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ConversationId)
                .HasColumnType("int(11)")
                .HasColumnName("conversation_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.SenderId)
                .HasColumnType("int(11)")
                .HasColumnName("sender_id");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_ibfk_1");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_ibfk_2");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("participants");

            entity.HasIndex(e => e.ConversationId, "conversation_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ConversationId)
                .HasColumnType("int(11)")
                .HasColumnName("conversation_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Participants)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("participants_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Participants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("participants_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });


        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("tokens");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int");
            
            entity.Property(e => e.JwtId)
            .IsRequired()
            .HasColumnType("longtext");
            
            entity.Property(e => e.ExpirationDate)
            .HasColumnType("datetime(6)");
            
            entity.Property(e => e.CreationDate)
            .HasColumnType("datetime(6)");
            
            entity.Property(e => e.UserId)
            .HasColumnType("int(11)");

            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

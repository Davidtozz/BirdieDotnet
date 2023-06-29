﻿// <auto-generated />
using System;
using BirdieDotnetAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BirdieDotnetAPI.Migrations
{
    [DbContext(typeof(TestContext))]
    partial class TestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("latin1_swedish_ci")
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "latin1");

            modelBuilder.Entity("BirdieDotnetAPI.Models.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("conversations", (string)null);
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<int>("ConversationId")
                        .HasColumnType("int(11)")
                        .HasColumnName("conversation_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<int>("SenderId")
                        .HasColumnType("int(11)")
                        .HasColumnName("sender_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ConversationId" }, "conversation_id");

                    b.HasIndex(new[] { "SenderId" }, "sender_id");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<int>("ConversationId")
                        .HasColumnType("int(11)")
                        .HasColumnName("conversation_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int(11)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ConversationId" }, "conversation_id")
                        .HasDatabaseName("conversation_id1");

                    b.HasIndex(new[] { "UserId" }, "user_id");

                    b.ToTable("participants", (string)null);
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tokens", (string)null);
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.Message", b =>
                {
                    b.HasOne("BirdieDotnetAPI.Models.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .IsRequired()
                        .HasConstraintName("messages_ibfk_1");

                    b.HasOne("BirdieDotnetAPI.Models.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("messages_ibfk_2");

                    b.Navigation("Conversation");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.Participant", b =>
                {
                    b.HasOne("BirdieDotnetAPI.Models.Conversation", "Conversation")
                        .WithMany("Participants")
                        .HasForeignKey("ConversationId")
                        .IsRequired()
                        .HasConstraintName("participants_ibfk_1");

                    b.HasOne("BirdieDotnetAPI.Models.User", "User")
                        .WithMany("Participants")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("participants_ibfk_2");

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.RefreshToken", b =>
                {
                    b.HasOne("BirdieDotnetAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.Conversation", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("BirdieDotnetAPI.Models.User", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}

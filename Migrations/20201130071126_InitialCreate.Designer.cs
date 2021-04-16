﻿// <auto-generated />
using System;
using A1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace A1.Migrations
{
    [DbContext(typeof(SchoolCommunityContext))]
    [Migration("20201130071126_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("A1.Models.Advertisement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CommunityID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CommunityID");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("A1.Models.Community", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("A1.Models.CommunityMembership", b =>
                {
                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<string>("CommunityID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StudentID", "CommunityID");

                    b.HasIndex("CommunityID");

                    b.ToTable("CommunityMemberships");
                });

            modelBuilder.Entity("A1.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("A1.Models.Advertisement", b =>
                {
                    b.HasOne("A1.Models.Community", "Community")
                        .WithMany("Advertisements")
                        .HasForeignKey("CommunityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");
                });

            modelBuilder.Entity("A1.Models.CommunityMembership", b =>
                {
                    b.HasOne("A1.Models.Community", "Community")
                        .WithMany("Membership")
                        .HasForeignKey("CommunityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("A1.Models.Student", "Student")
                        .WithMany("Membership")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("A1.Models.Community", b =>
                {
                    b.Navigation("Advertisements");

                    b.Navigation("Membership");
                });

            modelBuilder.Entity("A1.Models.Student", b =>
                {
                    b.Navigation("Membership");
                });
#pragma warning restore 612, 618
        }
    }
}
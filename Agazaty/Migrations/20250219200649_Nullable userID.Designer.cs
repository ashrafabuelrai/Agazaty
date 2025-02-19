﻿// <auto-generated />
using System;
using Agazaty.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agazaty.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250219200649_Nullable userID")]
    partial class NullableuserID
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Agazaty.Models.CasualLeave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("Year")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("CasualLeaves");
                });

            modelBuilder.Entity("Agazaty.Models.PermitLeave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("EmployeeNationalNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Hours")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PermitLeaves");
                });

            modelBuilder.Entity("Agazaty.Models.PermitLeaveImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeaveId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeaveId");

                    b.ToTable("PermitLeaveImages");
                });

            modelBuilder.Entity("Agazaty.Models.PermitLeaveImage", b =>
                {
                    b.HasOne("Agazaty.Models.PermitLeave", "PermitLeave")
                        .WithMany("PermitLeaveImages")
                        .HasForeignKey("LeaveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermitLeave");
                });

            modelBuilder.Entity("Agazaty.Models.PermitLeave", b =>
                {
                    b.Navigation("PermitLeaveImages");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Terminapp.Data;

#nullable disable

namespace Terminapp.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    partial class EmployeeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Terminapp.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("EmployeeEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Employment")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Terminapp.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ScheduleId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Terminapp.Models.ScheduleEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Shift")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleEntries");
                });

            modelBuilder.Entity("Terminapp.Models.VacationRequest", b =>
                {
                    b.Property<int>("VacationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("VacationId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("VacationId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("VacationRequests");
                });

            modelBuilder.Entity("Terminapp.Models.ScheduleEntry", b =>
                {
                    b.HasOne("Terminapp.Models.Schedule", null)
                        .WithMany("Entries")
                        .HasForeignKey("ScheduleId");
                });

            modelBuilder.Entity("Terminapp.Models.VacationRequest", b =>
                {
                    b.HasOne("Terminapp.Models.Employee", "Employee")
                        .WithMany("VacationRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Terminapp.Models.Schedule", null)
                        .WithMany("VacationRequests")
                        .HasForeignKey("ScheduleId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Terminapp.Models.Employee", b =>
                {
                    b.Navigation("VacationRequests");
                });

            modelBuilder.Entity("Terminapp.Models.Schedule", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("VacationRequests");
                });
#pragma warning restore 612, 618
        }
    }
}

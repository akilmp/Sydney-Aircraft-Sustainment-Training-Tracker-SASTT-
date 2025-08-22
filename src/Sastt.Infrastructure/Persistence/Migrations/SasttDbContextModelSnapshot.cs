using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sastt.Infrastructure.Persistence;

#nullable disable

namespace Sastt.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SasttDbContext))]
    partial class SasttDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Sastt.Domain.Entities.Aircraft", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<string>("Model")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("NVARCHAR2(100)");

                b.Property<string>("TailNumber")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("NVARCHAR2(20)");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.ToTable("Aircraft");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.AuditLog", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<string>("Action")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("NVARCHAR2(200)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<DateTime>("Timestamp")
                    .HasColumnType("TIMESTAMP");

                b.Property<Guid>("UserId")
                    .HasColumnType("RAW(16)");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AuditLogs");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.Defect", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<Guid>("AircraftId")
                    .HasColumnType("RAW(16)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<string>("Description")
                    .HasMaxLength(500)
                    .HasColumnType("NVARCHAR2(500)");

                b.Property<bool>("IsResolved")
                    .HasColumnType("NUMBER(1)");

                b.Property<int>("Priority")
                    .HasColumnType("NUMBER(10)");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.HasIndex("AircraftId");

                b.ToTable("Defects");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.Pilot", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("NVARCHAR2(200)");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.ToTable("Pilots");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.PilotCurrency", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<string>("CurrencyType")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("NVARCHAR2(100)");

                b.Property<DateTime>("ExpirationDate")
                    .HasColumnType("TIMESTAMP");

                b.Property<Guid>("PilotId")
                    .HasColumnType("RAW(16)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.HasIndex("PilotId");

                b.ToTable("PilotCurrencies");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.Task", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("NVARCHAR2(500)");

                b.Property<int>("Status")
                    .HasColumnType("NUMBER(10)");

                b.Property<Guid>("WorkOrderId")
                    .HasColumnType("RAW(16)");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.HasIndex("WorkOrderId");

                b.ToTable("Tasks");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.TrainingSession", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<bool>("Completed")
                    .HasColumnType("NUMBER(1)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<Guid>("PilotId")
                    .HasColumnType("RAW(16)");

                b.Property<DateTime>("ScheduledFor")
                    .HasColumnType("TIMESTAMP");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.HasIndex("PilotId");

                b.ToTable("TrainingSessions");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.User", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<string>("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("NVARCHAR2(200)");

                b.Property<int>("Role")
                    .HasColumnType("NUMBER(10)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("NVARCHAR2(100)");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.WorkOrder", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("RAW(16)");

                b.Property<Guid>("AircraftId")
                    .HasColumnType("RAW(16)");

                b.Property<int>("Priority")
                    .HasColumnType("NUMBER(10)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("TIMESTAMP");

                b.Property<int>("Status")
                    .HasColumnType("NUMBER(10)");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("TIMESTAMP");

                b.HasKey("Id");

                b.HasIndex("AircraftId");

                b.ToTable("WorkOrders");
            });

            modelBuilder.Entity("Sastt.Domain.Entities.AuditLog", b =>
            {
                b.HasOne("Sastt.Domain.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Sastt.Domain.Entities.Defect", b =>
            {
                b.HasOne("Sastt.Domain.Entities.Aircraft", null)
                    .WithMany("Defects")
                    .HasForeignKey("AircraftId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Sastt.Domain.Entities.PilotCurrency", b =>
            {
                b.HasOne("Sastt.Domain.Entities.Pilot", null)
                    .WithMany()
                    .HasForeignKey("PilotId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Sastt.Domain.Entities.Task", b =>
            {
                b.HasOne("Sastt.Domain.Entities.WorkOrder", null)
                    .WithMany("Tasks")
                    .HasForeignKey("WorkOrderId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Sastt.Domain.Entities.TrainingSession", b =>
            {
                b.HasOne("Sastt.Domain.Entities.Pilot", null)
                    .WithMany("TrainingSessions")
                    .HasForeignKey("PilotId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Sastt.Domain.Entities.WorkOrder", b =>
            {
                b.HasOne("Sastt.Domain.Entities.Aircraft", null)
                    .WithMany()
                    .HasForeignKey("AircraftId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Sastt.Domain.Entities.Aircraft", b => { b.Navigation("Defects"); });

            modelBuilder.Entity("Sastt.Domain.Entities.Pilot", b => { b.Navigation("TrainingSessions"); });

            modelBuilder.Entity("Sastt.Domain.Entities.WorkOrder", b => { b.Navigation("Tasks"); });
        }
    }
}

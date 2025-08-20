using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Sastt.Domain.Aircraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

                    b.Property<string>("Base")
                        .HasMaxLength(10)
                        .HasColumnType("NVARCHAR2(10)");

                    b.Property<string>("TailNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.HasKey("Id");

                    b.ToTable("Aircraft");
                });

            modelBuilder.Entity("Sastt.Domain.Defect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("Severity")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<int>("WorkOrderId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("WorkOrderId");

                    b.ToTable("Defects");
                });

            modelBuilder.Entity("Sastt.Domain.Pilot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.HasKey("Id");

                    b.ToTable("Pilots");
                });

            modelBuilder.Entity("Sastt.Domain.TrainingSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

                    b.Property<int>("Hours")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("PilotId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("PilotId");

                    b.ToTable("TrainingSessions");
                });

            modelBuilder.Entity("Sastt.Domain.WorkOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

                    b.Property<int>("AircraftId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.ToTable("WorkOrders");
                });

            modelBuilder.Entity("Sastt.Domain.WorkOrderTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("WorkOrderId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("WorkOrderId");

                    b.ToTable("WorkOrderTasks");
                });

            modelBuilder.Entity("Sastt.Domain.Defect", b =>
                {
                    b.HasOne("Sastt.Domain.WorkOrder", "WorkOrder")
                        .WithMany("Defects")
                        .HasForeignKey("WorkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkOrder");
                });

            modelBuilder.Entity("Sastt.Domain.TrainingSession", b =>
                {
                    b.HasOne("Sastt.Domain.Pilot", "Pilot")
                        .WithMany("TrainingSessions")
                        .HasForeignKey("PilotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pilot");
                });

            modelBuilder.Entity("Sastt.Domain.WorkOrder", b =>
                {
                    b.HasOne("Sastt.Domain.Aircraft", "Aircraft")
                        .WithMany("WorkOrders")
                        .HasForeignKey("AircraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aircraft");
                });

            modelBuilder.Entity("Sastt.Domain.WorkOrderTask", b =>
                {
                    b.HasOne("Sastt.Domain.WorkOrder", "WorkOrder")
                        .WithMany("Tasks")
                        .HasForeignKey("WorkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkOrder");
                });

            modelBuilder.Entity("Sastt.Domain.Aircraft", b => { });

            modelBuilder.Entity("Sastt.Domain.Pilot", b => { });

            modelBuilder.Entity("Sastt.Domain.WorkOrder", b => { });

            modelBuilder.Entity("Sastt.Domain.WorkOrderTask", b => { });

#pragma warning restore 612, 618
        }
    }
}

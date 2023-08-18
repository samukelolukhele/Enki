﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using server.Data;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(ServerDBContext))]
    [Migration("20230817111237_RestrictedChangesToCertainFieldsOnDayPlanModel")]
    partial class RestrictedChangesToCertainFieldsOnDayPlanModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("server.Model.DayPlan", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Userid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<bool>("is_completed")
                        .HasColumnType("boolean");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("DayPlans");
                });

            modelBuilder.Entity("server.Model.Milestone", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Taskid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<bool>("is_completed")
                        .HasColumnType("boolean");

                    b.Property<Guid>("task_id")
                        .HasColumnType("uuid");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("Taskid");

                    b.ToTable("Milestones");
                });

            modelBuilder.Entity("server.Model.Task", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DayPlanid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("day_plan_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<bool>("is_completed")
                        .HasColumnType("boolean");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("DayPlanid");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("server.Model.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("server.Model.DayPlan", b =>
                {
                    b.HasOne("server.Model.User", null)
                        .WithMany("day_plans")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("server.Model.Milestone", b =>
                {
                    b.HasOne("server.Model.Task", null)
                        .WithMany("milestones")
                        .HasForeignKey("Taskid");
                });

            modelBuilder.Entity("server.Model.Task", b =>
                {
                    b.HasOne("server.Model.DayPlan", null)
                        .WithMany("tasks")
                        .HasForeignKey("DayPlanid");
                });

            modelBuilder.Entity("server.Model.DayPlan", b =>
                {
                    b.Navigation("tasks");
                });

            modelBuilder.Entity("server.Model.Task", b =>
                {
                    b.Navigation("milestones");
                });

            modelBuilder.Entity("server.Model.User", b =>
                {
                    b.Navigation("day_plans");
                });
#pragma warning restore 612, 618
        }
    }
}

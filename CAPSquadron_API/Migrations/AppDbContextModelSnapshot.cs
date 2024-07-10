﻿// <auto-generated />
using System;
using CAPSquadron_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CAPSquadron_API.Models.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("AEDateP")
                        .HasColumnType("date")
                        .HasColumnName("aedate_p");

                    b.Property<DateOnly?>("AEInteractiveDate")
                        .HasColumnType("date")
                        .HasColumnName("aeinteractive_date");

                    b.Property<string>("AEInteractiveModule")
                        .HasColumnType("text")
                        .HasColumnName("aeinteractive_module");

                    b.Property<string>("AEModuleOrTest")
                        .HasColumnType("text")
                        .HasColumnName("aemodule_or_test");

                    b.Property<int?>("AEScore")
                        .HasColumnType("integer")
                        .HasColumnName("aescore");

                    b.Property<string>("AchvName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("achv_name");

                    b.Property<bool>("ActivePart")
                        .HasColumnType("boolean")
                        .HasColumnName("active_part");

                    b.Property<DateOnly?>("ActiveParticipationDate")
                        .HasColumnType("date")
                        .HasColumnName("active_participation_date");

                    b.Property<DateOnly?>("AprDate")
                        .HasColumnType("date")
                        .HasColumnName("apr_date");

                    b.Property<int>("CAPID")
                        .HasColumnType("integer")
                        .HasColumnName("capid");

                    b.Property<bool>("CadetOath")
                        .HasColumnType("boolean")
                        .HasColumnName("cadet_oath");

                    b.Property<DateOnly?>("CadetOathDate")
                        .HasColumnType("date")
                        .HasColumnName("cadet_oath_date");

                    b.Property<DateOnly?>("CharacterDevelopment")
                        .HasColumnType("date")
                        .HasColumnName("character_development");

                    b.Property<DateOnly?>("DrillDate")
                        .HasColumnType("date")
                        .HasColumnName("drill_date");

                    b.Property<int?>("DrillScore")
                        .HasColumnType("integer")
                        .HasColumnName("drill_score");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateOnly?>("EssayDate")
                        .HasColumnType("date")
                        .HasColumnName("essay_date");

                    b.Property<DateOnly>("JoinDate")
                        .HasColumnType("date")
                        .HasColumnName("join_date");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<DateOnly?>("LeadLabDateP")
                        .HasColumnType("date")
                        .HasColumnName("lead_lab_date_p");

                    b.Property<int?>("LeadLabScore")
                        .HasColumnType("integer")
                        .HasColumnName("lead_lab_score");

                    b.Property<DateOnly?>("LeadershipExpectationsDate")
                        .HasColumnType("date")
                        .HasColumnName("leadership_expectations_date");

                    b.Property<DateOnly?>("LeadershipInteractiveDate")
                        .HasColumnType("date")
                        .HasColumnName("leadership_interactive_date");

                    b.Property<string>("NameFirst")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name_first");

                    b.Property<string>("NameLast")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name_last");

                    b.Property<DateOnly?>("NextApprovalDate")
                        .HasColumnType("date")
                        .HasColumnName("next_approval_date");

                    b.Property<DateOnly?>("OralPresentationDate")
                        .HasColumnType("date")
                        .HasColumnName("oral_presentation_date");

                    b.Property<DateOnly?>("PhyFitTest")
                        .HasColumnType("date")
                        .HasColumnName("phy_fit_test");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("region");

                    b.Property<DateOnly?>("SpecialActivityDate")
                        .HasColumnType("date")
                        .HasColumnName("special_activity_date");

                    b.Property<DateOnly?>("SpeechDate")
                        .HasColumnType("date")
                        .HasColumnName("speech_date");

                    b.Property<DateOnly?>("StaffServiceDate")
                        .HasColumnType("date")
                        .HasColumnName("staff_service_date");

                    b.Property<string>("TechnicalWritingAssignment")
                        .HasColumnType("text")
                        .HasColumnName("technical_writing_assignment");

                    b.Property<DateOnly?>("TechnicalWritingAssignmentDate")
                        .HasColumnType("date")
                        .HasColumnName("technical_writing_assignment_date");

                    b.Property<DateOnly?>("UniformDate")
                        .HasColumnType("date")
                        .HasColumnName("uniform_date");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("unit");

                    b.Property<DateOnly?>("WelcomeCourseDate")
                        .HasColumnType("date")
                        .HasColumnName("welcome_course_date");

                    b.Property<string>("Wing")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("wing");

                    b.HasKey("Id")
                        .HasName("pk_achievements");

                    b.HasIndex("CAPID", "AchvName")
                        .IsUnique();

                    b.ToTable("achievements");
                });

            modelBuilder.Entity("CAPSquadron_API.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_flights");

                    b.ToTable("flights");
                });

            modelBuilder.Entity("CAPSquadron_API.Models.FlightMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CAPID")
                        .HasColumnType("integer")
                        .HasColumnName("capid");

                    b.Property<int>("FlightId")
                        .HasColumnType("integer")
                        .HasColumnName("flight_id");

                    b.Property<bool>("IsFlightCommander")
                        .HasColumnType("boolean")
                        .HasColumnName("is_flight_commander");

                    b.Property<bool>("IsFlightSergeant")
                        .HasColumnType("boolean")
                        .HasColumnName("is_flight_sergeant");

                    b.HasKey("Id")
                        .HasName("pk_flight_members");

                    b.HasIndex("CAPID");

                    b.HasIndex("FlightId")
                        .HasDatabaseName("ix_flight_members_flight_id");

                    b.ToTable("flight_members");
                });

            modelBuilder.Entity("CAPSquadron_API.Models.Member", b =>
                {
                    b.Property<int>("CAPID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("capid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CAPID"));

                    b.Property<bool>("EoCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("eo_completed");

                    b.Property<DateOnly>("Expiration")
                        .HasColumnType("date")
                        .HasColumnName("expiration");

                    b.Property<DateTimeOffset?>("InactiveDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("inactive_date");

                    b.Property<bool>("IsExecutiveStaff")
                        .HasColumnType("boolean")
                        .HasColumnName("is_executive_staff");

                    b.Property<bool>("IsOnLeave")
                        .HasColumnType("boolean")
                        .HasColumnName("is_on_leave");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<bool>("OpsecCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("opsec_completed");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("rank");

                    b.Property<bool>("SafetyCurrent")
                        .HasColumnType("boolean")
                        .HasColumnName("safety_current");

                    b.HasKey("CAPID")
                        .HasName("pk_members");

                    b.ToTable("members");
                });

            modelBuilder.Entity("CAPSquadron_API.Models.FlightMember", b =>
                {
                    b.HasOne("CAPSquadron_API.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("CAPID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_flight_members_members_member_capid");

                    b.HasOne("CAPSquadron_API.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_flight_members_flights_flight_id");

                    b.Navigation("Flight");

                    b.Navigation("Member");
                });
#pragma warning restore 612, 618
        }
    }
}

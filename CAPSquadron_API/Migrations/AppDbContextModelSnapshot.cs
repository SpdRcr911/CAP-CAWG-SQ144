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

            modelBuilder.Entity("CAPSquadron_API.Models.AttendanceSignIn", b =>
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
                        .HasName("pk_attendance_sign_ins");

                    b.ToTable("attendance_sign_ins");
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

                    b.HasIndex("CAPID")
                        .HasDatabaseName("ix_flight_members_capid");

                    b.HasIndex("FlightId")
                        .HasDatabaseName("ix_flight_members_flight_id");

                    b.ToTable("flight_members");
                });

            modelBuilder.Entity("CAPSquadron_API.Models.Member", b =>
                {
                    b.Property<int>("CAPID")
                        .HasColumnType("integer")
                        .HasColumnName("capid");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("CPhone")
                        .HasColumnType("text")
                        .HasColumnName("cphone");

                    b.Property<string>("CityStateZip")
                        .HasColumnType("text")
                        .HasColumnName("city_state_zip");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("text")
                        .HasColumnName("email_address");

                    b.Property<DateOnly>("Expiration")
                        .HasColumnType("date")
                        .HasColumnName("expiration");

                    b.Property<string>("FBIStatus")
                        .HasColumnType("text")
                        .HasColumnName("fbistatus");

                    b.Property<string>("FullName")
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<string>("Gender")
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("HPhone")
                        .HasColumnType("text")
                        .HasColumnName("hphone");

                    b.Property<DateOnly>("Joined")
                        .HasColumnType("date")
                        .HasColumnName("joined");

                    b.Property<string>("Rank")
                        .HasColumnType("text")
                        .HasColumnName("rank");

                    b.Property<DateOnly>("RankDate")
                        .HasColumnType("date")
                        .HasColumnName("rank_date");

                    b.Property<string>("Region1")
                        .HasColumnType("text")
                        .HasColumnName("region1");

                    b.Property<string>("Wing_Unit")
                        .HasColumnType("text")
                        .HasColumnName("wing_unit");

                    b.HasKey("CAPID")
                        .HasName("pk_members");

                    b.ToTable("members");
                });

            modelBuilder.Entity("CAPSquadron_API.Models.QualityCadetUnitReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AEAexOrStemKit")
                        .HasColumnType("text")
                        .HasColumnName("ae_aex_or_stem_kit");

                    b.Property<string>("AEX")
                        .HasColumnType("text")
                        .HasColumnName("aex");

                    b.Property<string>("AdultLeadership3PlusTlcGrads")
                        .HasColumnType("text")
                        .HasColumnName("adult_leadership_3_plus_tlc_grads");

                    b.Property<string>("CadetAchievement45Percent")
                        .HasColumnType("text")
                        .HasColumnName("cadet_achievement_45_percent");

                    b.Property<int>("CadetsJoined")
                        .HasColumnType("integer")
                        .HasColumnName("cadets_joined");

                    b.Property<int>("CadetsWithEncamp")
                        .HasColumnType("integer")
                        .HasColumnName("cadets_with_encamp");

                    b.Property<int>("CadetsWithGES")
                        .HasColumnType("integer")
                        .HasColumnName("cadets_with_ges");

                    b.Property<int>("CadetsWithOFlights")
                        .HasColumnType("integer")
                        .HasColumnName("cadets_with_oflights");

                    b.Property<int>("CadetsWithWB")
                        .HasColumnType("integer")
                        .HasColumnName("cadets_with_wb");

                    b.Property<string>("Charter")
                        .HasColumnType("text")
                        .HasColumnName("charter");

                    b.Property<int>("CurrentCadets")
                        .HasColumnType("integer")
                        .HasColumnName("current_cadets");

                    b.Property<int>("CurryIn8Weeks")
                        .HasColumnType("integer")
                        .HasColumnName("curry_in_8_weeks");

                    b.Property<string>("Encamp50Percent")
                        .HasColumnType("text")
                        .HasColumnName("encamp_50_percent");

                    b.Property<string>("Enrollment25PlusCadets")
                        .HasColumnType("text")
                        .HasColumnName("enrollment_25_plus_cadets");

                    b.Property<string>("GES60Percent")
                        .HasColumnType("text")
                        .HasColumnName("ges_60_percent");

                    b.Property<int>("NumberOfCriteriaMet")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_criteria_met");

                    b.Property<string>("OFlights70Percent")
                        .HasColumnType("text")
                        .HasColumnName("oflights_70_percent");

                    b.Property<string>("Onboarding70Percent")
                        .HasColumnType("text")
                        .HasColumnName("onboarding_70_percent");

                    b.Property<string>("OutsideActivities")
                        .HasColumnType("text")
                        .HasColumnName("outside_activities");

                    b.Property<string>("QCUA6PlusCriteriaMet")
                        .HasColumnType("text")
                        .HasColumnName("qcua_6_plus_criteria_met");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("report_date");

                    b.Property<string>("STEM")
                        .HasColumnType("text")
                        .HasColumnName("stem");

                    b.Property<int>("SeniorsWithCPSpecialtyTrackRating")
                        .HasColumnType("integer")
                        .HasColumnName("seniors_with_cp_specialty_track_rating");

                    b.Property<int>("SeniorsWithTLC")
                        .HasColumnType("integer")
                        .HasColumnName("seniors_with_tlc");

                    b.Property<string>("SpecialtyTrack2PlusSeniorsWithRating")
                        .HasColumnType("text")
                        .HasColumnName("specialty_track_2_plus_seniors_with_rating");

                    b.HasKey("Id")
                        .HasName("pk_quality_cadet_unit_reports");

                    b.ToTable("quality_cadet_unit_reports", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CAPSquadron_API.Models.FlightMember", b =>
                {
                    b.HasOne("CAPSquadron_API.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("CAPID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_flight_members_members_capid");

                    b.HasOne("CAPSquadron_API.Models.Flight", "Flight")
                        .WithMany("FlightMembers")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_flight_members_flights_flight_id");

                    b.Navigation("Flight");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("CAPSquadron_API.Models.Flight", b =>
                {
                    b.Navigation("FlightMembers");
                });
#pragma warning restore 612, 618
        }
    }
}

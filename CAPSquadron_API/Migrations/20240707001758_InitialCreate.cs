using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    capid = table.Column<int>(type: "integer", nullable: false),
                    name_last = table.Column<string>(type: "text", nullable: false),
                    name_first = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    achv_name = table.Column<string>(type: "text", nullable: false),
                    apr_date = table.Column<DateOnly>(type: "date", nullable: true),
                    join_date = table.Column<DateOnly>(type: "date", nullable: false),
                    region = table.Column<string>(type: "text", nullable: false),
                    wing = table.Column<string>(type: "text", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: false),
                    phy_fit_test = table.Column<DateOnly>(type: "date", nullable: true),
                    lead_lab_date_p = table.Column<DateOnly>(type: "date", nullable: true),
                    lead_lab_score = table.Column<int>(type: "integer", nullable: true),
                    aedate_p = table.Column<DateOnly>(type: "date", nullable: true),
                    aescore = table.Column<int>(type: "integer", nullable: true),
                    aemodule_or_test = table.Column<string>(type: "text", nullable: true),
                    character_development = table.Column<string>(type: "text", nullable: true),
                    active_part = table.Column<bool>(type: "boolean", nullable: false),
                    active_participation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    cadet_oath = table.Column<bool>(type: "boolean", nullable: false),
                    cadet_oath_date = table.Column<DateOnly>(type: "date", nullable: true),
                    leadership_expectations_date = table.Column<DateOnly>(type: "date", nullable: true),
                    uniform_date = table.Column<DateOnly>(type: "date", nullable: true),
                    special_activity_date = table.Column<DateOnly>(type: "date", nullable: true),
                    next_approval_date = table.Column<DateOnly>(type: "date", nullable: true),
                    staff_service_date = table.Column<DateOnly>(type: "date", nullable: true),
                    oral_presentation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    technical_writing_assignment_date = table.Column<DateOnly>(type: "date", nullable: true),
                    technical_writing_assignment = table.Column<string>(type: "text", nullable: true),
                    drill_date = table.Column<DateOnly>(type: "date", nullable: true),
                    drill_score = table.Column<int>(type: "integer", nullable: true),
                    welcome_course_date = table.Column<DateOnly>(type: "date", nullable: true),
                    essay_date = table.Column<DateOnly>(type: "date", nullable: true),
                    speech_date = table.Column<DateOnly>(type: "date", nullable: true),
                    aeinteractive_date = table.Column<DateOnly>(type: "date", nullable: true),
                    aeinteractive_module = table.Column<string>(type: "text", nullable: true),
                    leadership_interactive_date = table.Column<DateOnly>(type: "date", nullable: true),
                    last_modified = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    flight_commander_capid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flights", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    capid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    rank = table.Column<string>(type: "text", nullable: false),
                    expiration = table.Column<DateOnly>(type: "date", nullable: false),
                    eo_completed = table.Column<bool>(type: "boolean", nullable: false),
                    opsec_completed = table.Column<bool>(type: "boolean", nullable: false),
                    safety_current = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    flight_id = table.Column<int>(type: "integer", nullable: true),
                    is_executive_staff = table.Column<bool>(type: "boolean", nullable: false),
                    is_on_leave = table.Column<bool>(type: "boolean", nullable: false),
                    flight_sergeant_capid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_members", x => x.capid);
                    table.ForeignKey(
                        name: "FK_members_flights_flight_id",
                        column: x => x.flight_id,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_members_flights_flight_sergeant_capid",
                        column: x => x.flight_sergeant_capid,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievements_capid_achv_name",
                table: "achievements",
                columns: new[] { "capid", "achv_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flights_flight_commander_capid",
                table: "flights",
                column: "flight_commander_capid");

            migrationBuilder.CreateIndex(
                name: "IX_members_flight_id",
                table: "members",
                column: "flight_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_flight_sergeant_capid",
                table: "members",
                column: "flight_sergeant_capid");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_members_flight_commander_capid",
                table: "flights",
                column: "flight_commander_capid",
                principalTable: "members",
                principalColumn: "capid",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_members_flight_commander_capid",
                table: "flights");

            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "flights");
        }
    }
}

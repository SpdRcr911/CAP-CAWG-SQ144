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
                    character_development = table.Column<DateOnly>(type: "date", nullable: true),
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
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                    name = table.Column<string>(type: "text", nullable: false)
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
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inactive_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_executive_staff = table.Column<bool>(type: "boolean", nullable: false),
                    is_on_leave = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_members", x => x.capid);
                });

            migrationBuilder.CreateTable(
                name: "flight_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flight_id = table.Column<int>(type: "integer", nullable: false),
                    capid = table.Column<int>(type: "integer", nullable: false),
                    is_flight_commander = table.Column<bool>(type: "boolean", nullable: false),
                    is_flight_sergeant = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flight_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_flight_members_flights_flight_id",
                        column: x => x.flight_id,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_flight_members_members_member_capid",
                        column: x => x.capid,
                        principalTable: "members",
                        principalColumn: "capid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievements_capid_achv_name",
                table: "achievements",
                columns: new[] { "capid", "achv_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flight_members_capid",
                table: "flight_members",
                column: "capid");

            migrationBuilder.CreateIndex(
                name: "ix_flight_members_flight_id",
                table: "flight_members",
                column: "flight_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "flight_members");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "members");
        }
    }
}

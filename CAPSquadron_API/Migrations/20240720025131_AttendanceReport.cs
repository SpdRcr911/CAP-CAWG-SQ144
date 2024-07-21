using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class AttendanceReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attendance_reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    topics = table.Column<string>(type: "text", nullable: true),
                    is_drill_and_ceremony = table.Column<bool>(type: "boolean", nullable: false),
                    is_aet = table.Column<bool>(type: "boolean", nullable: false),
                    is_physical_fitness_testing = table.Column<bool>(type: "boolean", nullable: false),
                    is_sbt = table.Column<bool>(type: "boolean", nullable: false),
                    is_cadet_programs = table.Column<bool>(type: "boolean", nullable: false),
                    is_character_development = table.Column<bool>(type: "boolean", nullable: false),
                    is_community_service = table.Column<bool>(type: "boolean", nullable: false),
                    location = table.Column<string>(type: "text", nullable: true),
                    is_other = table.Column<bool>(type: "boolean", nullable: false),
                    other = table.Column<string>(type: "text", nullable: true),
                    section = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    website = table.Column<string>(type: "text", nullable: true),
                    meeting_info = table.Column<string>(type: "text", nullable: true),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    rank = table.Column<string>(type: "text", nullable: true),
                    capid = table.Column<int>(type: "integer", nullable: false),
                    expiration = table.Column<DateOnly>(type: "date", nullable: true),
                    is_present = table.Column<bool>(type: "boolean", nullable: false),
                    is_excused = table.Column<bool>(type: "boolean", nullable: false),
                    has_uniform = table.Column<bool>(type: "boolean", nullable: false),
                    has_capf160_161 = table.Column<bool>(type: "boolean", nullable: false),
                    eocurrent = table.Column<bool>(type: "boolean", nullable: false),
                    opseccurrent = table.Column<bool>(type: "boolean", nullable: false),
                    safety_current = table.Column<bool>(type: "boolean", nullable: false),
                    guest_name = table.Column<string>(type: "text", nullable: true),
                    guest_rank = table.Column<string>(type: "text", nullable: true),
                    guest_phone = table.Column<string>(type: "text", nullable: true),
                    guest_email = table.Column<string>(type: "text", nullable: true),
                    guest_notes = table.Column<string>(type: "text", nullable: true),
                    overall_notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendance_reports", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendance_reports");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class CadetPromotionsFullTrackUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.CreateTable(
                name: "cadet_promotions_full_tracks",
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
                    table.PrimaryKey("pk_cadet_promotions_full_tracks", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cadet_promotions_full_tracks_capid_achv_name",
                table: "cadet_promotions_full_tracks",
                columns: new[] { "capid", "achv_name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cadet_promotions_full_tracks");

            migrationBuilder.CreateTable(
                name: "achievements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aedate_p = table.Column<DateOnly>(type: "date", nullable: true),
                    aeinteractive_date = table.Column<DateOnly>(type: "date", nullable: true),
                    aeinteractive_module = table.Column<string>(type: "text", nullable: true),
                    aemodule_or_test = table.Column<string>(type: "text", nullable: true),
                    aescore = table.Column<int>(type: "integer", nullable: true),
                    achv_name = table.Column<string>(type: "text", nullable: false),
                    active_part = table.Column<bool>(type: "boolean", nullable: false),
                    active_participation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    apr_date = table.Column<DateOnly>(type: "date", nullable: true),
                    capid = table.Column<int>(type: "integer", nullable: false),
                    cadet_oath = table.Column<bool>(type: "boolean", nullable: false),
                    cadet_oath_date = table.Column<DateOnly>(type: "date", nullable: true),
                    character_development = table.Column<DateOnly>(type: "date", nullable: true),
                    drill_date = table.Column<DateOnly>(type: "date", nullable: true),
                    drill_score = table.Column<int>(type: "integer", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    essay_date = table.Column<DateOnly>(type: "date", nullable: true),
                    join_date = table.Column<DateOnly>(type: "date", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    lead_lab_date_p = table.Column<DateOnly>(type: "date", nullable: true),
                    lead_lab_score = table.Column<int>(type: "integer", nullable: true),
                    leadership_expectations_date = table.Column<DateOnly>(type: "date", nullable: true),
                    leadership_interactive_date = table.Column<DateOnly>(type: "date", nullable: true),
                    name_first = table.Column<string>(type: "text", nullable: false),
                    name_last = table.Column<string>(type: "text", nullable: false),
                    next_approval_date = table.Column<DateOnly>(type: "date", nullable: true),
                    oral_presentation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    phy_fit_test = table.Column<DateOnly>(type: "date", nullable: true),
                    region = table.Column<string>(type: "text", nullable: false),
                    special_activity_date = table.Column<DateOnly>(type: "date", nullable: true),
                    speech_date = table.Column<DateOnly>(type: "date", nullable: true),
                    staff_service_date = table.Column<DateOnly>(type: "date", nullable: true),
                    technical_writing_assignment = table.Column<string>(type: "text", nullable: true),
                    technical_writing_assignment_date = table.Column<DateOnly>(type: "date", nullable: true),
                    uniform_date = table.Column<DateOnly>(type: "date", nullable: true),
                    unit = table.Column<string>(type: "text", nullable: false),
                    welcome_course_date = table.Column<DateOnly>(type: "date", nullable: true),
                    wing = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievements", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievements_capid_achv_name",
                table: "achievements",
                columns: new[] { "capid", "achv_name" },
                unique: true);
        }
    }
}

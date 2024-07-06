using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class AddAchievementFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CapId = table.Column<int>(type: "integer", nullable: false),
                    NameLast = table.Column<string>(type: "text", nullable: false),
                    NameFirst = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AchvName = table.Column<string>(type: "text", nullable: false),
                    AprDate = table.Column<DateOnly>(type: "date", nullable: true),
                    JoinDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Wing = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    PhyFitTest = table.Column<DateOnly>(type: "date", nullable: true),
                    LeadLabDateP = table.Column<DateOnly>(type: "date", nullable: true),
                    LeadLabScore = table.Column<int>(type: "integer", nullable: true),
                    AEDateP = table.Column<DateOnly>(type: "date", nullable: true),
                    AEScore = table.Column<int>(type: "integer", nullable: true),
                    AEModuleOrTest = table.Column<string>(type: "text", nullable: true),
                    CharacterDevelopment = table.Column<string>(type: "text", nullable: true),
                    ActivePart = table.Column<string>(type: "text", nullable: true),
                    ActiveParticipationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CadetOath = table.Column<bool>(type: "boolean", nullable: false),
                    CadetOathDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LeadershipExpectationsDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UniformDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SpecialActivityDate = table.Column<DateOnly>(type: "date", nullable: true),
                    NextApprovalDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StaffServiceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OralPresentationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TechnicalWritingAssignmentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TechnicalWritingAssignment = table.Column<string>(type: "text", nullable: true),
                    DrillDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DrillScore = table.Column<int>(type: "integer", nullable: true),
                    WelcomeCourseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EssayDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SpeechDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AEInteractiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AEInteractiveModule = table.Column<string>(type: "text", nullable: true),
                    LeadershipInteractiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LastModified = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CapId_AchvName",
                table: "Achievements",
                columns: new[] { "CapId", "AchvName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");
        }
    }
}

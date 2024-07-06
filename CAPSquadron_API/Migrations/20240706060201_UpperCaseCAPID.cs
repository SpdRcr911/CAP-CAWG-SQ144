using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class UpperCaseCAPID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CapId",
                table: "Members",
                newName: "CAPID");

            migrationBuilder.RenameIndex(
                name: "IX_Members_CapId",
                table: "Members",
                newName: "IX_Members_CAPID");

            migrationBuilder.RenameColumn(
                name: "CapId",
                table: "Achievements",
                newName: "CAPID");

            migrationBuilder.RenameIndex(
                name: "IX_Achievements_CapId_AchvName",
                table: "Achievements",
                newName: "IX_Achievements_CAPID_AchvName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CAPID",
                table: "Members",
                newName: "CapId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_CAPID",
                table: "Members",
                newName: "IX_Members_CapId");

            migrationBuilder.RenameColumn(
                name: "CAPID",
                table: "Achievements",
                newName: "CapId");

            migrationBuilder.RenameIndex(
                name: "IX_Achievements_CAPID_AchvName",
                table: "Achievements",
                newName: "IX_Achievements_CapId_AchvName");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFlightSergeantLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_members_flights_flight_sergeant_capid",
                table: "members");

            migrationBuilder.RenameColumn(
                name: "flight_sergeant_capid",
                table: "members",
                newName: "flight_sergeant_for_flight_id");

            migrationBuilder.RenameIndex(
                name: "IX_members_flight_sergeant_capid",
                table: "members",
                newName: "IX_members_flight_sergeant_for_flight_id");

            migrationBuilder.AddForeignKey(
                name: "FK_members_flights_flight_sergeant_for_flight_id",
                table: "members",
                column: "flight_sergeant_for_flight_id",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_members_flights_flight_sergeant_for_flight_id",
                table: "members");

            migrationBuilder.RenameColumn(
                name: "flight_sergeant_for_flight_id",
                table: "members",
                newName: "flight_sergeant_capid");

            migrationBuilder.RenameIndex(
                name: "IX_members_flight_sergeant_for_flight_id",
                table: "members",
                newName: "IX_members_flight_sergeant_capid");

            migrationBuilder.AddForeignKey(
                name: "FK_members_flights_flight_sergeant_capid",
                table: "members",
                column: "flight_sergeant_capid",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

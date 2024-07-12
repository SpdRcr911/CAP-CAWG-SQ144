using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class RenameMemberToAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_flight_members_members_member_capid",
                table: "flight_members");

            migrationBuilder.DropPrimaryKey(
                name: "pk_members",
                table: "members");

            migrationBuilder.RenameTable(
                name: "members",
                newName: "attendance_sign_ins");

            migrationBuilder.AddPrimaryKey(
                name: "pk_attendance_sign_ins",
                table: "attendance_sign_ins",
                column: "capid");

            migrationBuilder.AddForeignKey(
                name: "fk_flight_members_attendance_sign_ins_member_capid",
                table: "flight_members",
                column: "capid",
                principalTable: "attendance_sign_ins",
                principalColumn: "capid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_flight_members_attendance_sign_ins_member_capid",
                table: "flight_members");

            migrationBuilder.DropPrimaryKey(
                name: "pk_attendance_sign_ins",
                table: "attendance_sign_ins");

            migrationBuilder.RenameTable(
                name: "attendance_sign_ins",
                newName: "members");

            migrationBuilder.AddPrimaryKey(
                name: "pk_members",
                table: "members",
                column: "capid");

            migrationBuilder.AddForeignKey(
                name: "fk_flight_members_members_member_capid",
                table: "flight_members",
                column: "capid",
                principalTable: "members",
                principalColumn: "capid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

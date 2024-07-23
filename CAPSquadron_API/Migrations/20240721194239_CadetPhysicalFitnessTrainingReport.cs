using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class CadetPhysicalFitnessTrainingReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cadet_physical_fitness_training_reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    capid = table.Column<int>(type: "integer", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    hfzcred = table.Column<string>(type: "text", nullable: true),
                    pacer_run_req = table.Column<int>(type: "integer", nullable: false),
                    mile_run_req = table.Column<TimeSpan>(type: "interval", nullable: false),
                    curl_up_req = table.Column<int>(type: "integer", nullable: false),
                    sit_and_reach_req = table.Column<int>(type: "integer", nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cadet_physical_fitness_training_reports", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cadet_physical_fitness_training_reports");
        }
    }
}

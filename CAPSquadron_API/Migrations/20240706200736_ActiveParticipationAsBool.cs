using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAPSquadron_API.Migrations
{
    /// <inheritdoc />
    public partial class ActiveParticipationAsBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Assuming "ActivePart" column holds values like "true" or "false"
            migrationBuilder.Sql("ALTER TABLE \"Achievements\" ALTER COLUMN \"ActivePart\" TYPE boolean USING \"ActivePart\"::boolean;");
            migrationBuilder.Sql("UPDATE \"Achievements\" SET \"ActivePart\" = FALSE WHERE \"ActivePart\" IS NULL;");
            migrationBuilder.Sql("ALTER TABLE \"Achievements\" ALTER COLUMN \"ActivePart\" SET NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE \"Achievements\" ALTER COLUMN \"ActivePart\" SET DEFAULT FALSE;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the migration here if necessary
        }
    }
}

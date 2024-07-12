using CAPSquadron_API.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CAPSquadron_API.Schemas;

public class SwaggerSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(AttendanceSignIn))
        {
            schema.Example = new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["capId"] = new OpenApiInteger(123456),
                ["name"] = new OpenApiString("John Doe"),
                ["rank"] = new OpenApiString("Captain"),
                ["expiration"] = new OpenApiDate(DateTime.UtcNow.AddYears(1)),
                ["eoCompleted"] = new OpenApiBoolean(true),
                ["opsecCompleted"] = new OpenApiBoolean(true),
                ["safetyCurrent"] = new OpenApiBoolean(true),
                ["lastModified"] = new OpenApiDate(DateTime.UtcNow),
                ["inactiveDate"] = new OpenApiNull()
            };
        }

        if (context.Type == typeof(Achievement))
        {
            schema.Example = new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["capId"] = new OpenApiInteger(123456),
                ["nameLast"] = new OpenApiString("Doe"),
                ["nameFirst"] = new OpenApiString("John"),
                ["email"] = new OpenApiString("john.doe@example.com"),
                ["achvName"] = new OpenApiString("Achievement 1"),
                ["aprDate"] = new OpenApiNull(),
                ["joinDate"] = new OpenApiDate(DateTime.UtcNow.AddYears(-1)),
                ["region"] = new OpenApiString("Region 1"),
                ["wing"] = new OpenApiString("Wing 1"),
                ["unit"] = new OpenApiString("Unit 1"),
                ["phyFitTest"] = new OpenApiString("Passed"),
                ["leadLabDateP"] = new OpenApiNull(),
                ["leadLabScore"] = new OpenApiInteger(90),
                ["aeDateP"] = new OpenApiNull(),
                ["aeScore"] = new OpenApiInteger(85),
                ["aeModuleOrTest"] = new OpenApiString("Module 1"),
                ["characterDevelopment"] = new OpenApiString("Completed"),
                ["activePart"] = new OpenApiString("Active"),
                ["activeParticipationDate"] = new OpenApiNull(),
                ["cadetOath"] = new OpenApiString("Oath"),
                ["cadetOathDate"] = new OpenApiNull(),
                ["leadershipExpectationsDate"] = new OpenApiNull(),
                ["uniformDate"] = new OpenApiNull(),
                ["specialActivityDate"] = new OpenApiNull(),
                ["nextApprovalDate"] = new OpenApiNull(),
                ["staffServiceDate"] = new OpenApiNull(),
                ["oralPresentationDate"] = new OpenApiNull(),
                ["technicalWritingAssignmentDate"] = new OpenApiNull(),
                ["technicalWritingAssignment"] = new OpenApiString("Completed"),
                ["drillDate"] = new OpenApiNull(),
                ["drillScore"] = new OpenApiInteger(95),
                ["welcomeCourseDate"] = new OpenApiNull(),
                ["essayDate"] = new OpenApiNull(),
                ["speechDate"] = new OpenApiNull(),
                ["aeInteractiveDate"] = new OpenApiNull(),
                ["aeInteractiveModule"] = new OpenApiString("Module 1"),
                ["leadershipInteractiveDate"] = new OpenApiNull(),
                ["lastModified"] = new OpenApiDate(DateTime.UtcNow)
            };
        }

        if (context.Type == typeof(AchievementCsvModel))
        {
            schema.Example = new OpenApiObject
            {
                ["capId"] = new OpenApiInteger(123456),
                ["nameLast"] = new OpenApiString("Doe"),
                ["nameFirst"] = new OpenApiString("John"),
                ["email"] = new OpenApiString("john.doe@example.com"),
                ["achvName"] = new OpenApiString("Achievement 1"),
                ["aprDate"] = new OpenApiString("None"),
                ["joinDate"] = new OpenApiDate(DateTime.UtcNow.AddYears(-1)),
                ["region"] = new OpenApiString("Region 1"),
                ["wing"] = new OpenApiString("Wing 1"),
                ["unit"] = new OpenApiString("Unit 1"),
                ["phyFitTest"] = new OpenApiString("Passed"),
                ["leadLabDateP"] = new OpenApiString("None"),
                ["leadLabScore"] = new OpenApiInteger(90),
                ["aeDateP"] = new OpenApiString("None"),
                ["aeScore"] = new OpenApiInteger(85),
                ["aeModuleOrTest"] = new OpenApiString("Module 1"),
                ["characterDevelopment"] = new OpenApiString("Completed"),
                ["activePart"] = new OpenApiString("Active"),
                ["activeParticipationDate"] = new OpenApiString("None"),
                ["cadetOath"] = new OpenApiString("Oath"),
                ["cadetOathDate"] = new OpenApiString("None"),
                ["leadershipExpectationsDate"] = new OpenApiString("None"),
                ["uniformDate"] = new OpenApiString("None"),
                ["specialActivityDate"] = new OpenApiString("None"),
                ["nextApprovalDate"] = new OpenApiString("None"),
                ["staffServiceDate"] = new OpenApiString("None"),
                ["oralPresentationDate"] = new OpenApiString("None"),
                ["technicalWritingAssignmentDate"] = new OpenApiString("None"),
                ["technicalWritingAssignment"] = new OpenApiString("Completed"),
                ["drillDate"] = new OpenApiString("None"),
                ["drillScore"] = new OpenApiInteger(95),
                ["welcomeCourseDate"] = new OpenApiString("None"),
                ["essayDate"] = new OpenApiString("None"),
                ["speechDate"] = new OpenApiString("None"),
                ["aeInteractiveDate"] = new OpenApiString("None"),
                ["aeInteractiveModule"] = new OpenApiString("Module 1"),
                ["leadershipInteractiveDate"] = new OpenApiString("None")
            };
        }
    }
}
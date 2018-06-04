using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Piffnium.Web.Infrastructure.EntityFramework.Migrations
{
    public partial class addcomparationstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.ProjectId);
                    table.UniqueConstraint("uk_projects_projectName", x => x.ProjectName);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<long>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comparisons",
                columns: table => new
                {
                    ComparisonId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComparisonKey = table.Column<string>(maxLength: 100, nullable: false),
                    SessionId = table.Column<long>(nullable: false),
                    DestinationFileName = table.Column<string>(nullable: false),
                    ComparedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comparions", x => x.ComparisonId);
                    table.ForeignKey(
                        name: "FK_Comparisons_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Differences",
                columns: table => new
                {
                    ComparisonId = table.Column<long>(nullable: false),
                    DifferenceRate = table.Column<double>(nullable: false),
                    SourceFileName = table.Column<string>(nullable: false),
                    DifferenceFileName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_differences", x => x.ComparisonId);
                    table.ForeignKey(
                        name: "FK_Differences_Comparisons_ComparisonId",
                        column: x => x.ComparisonId,
                        principalTable: "Comparisons",
                        principalColumn: "ComparisonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comparisons_SessionId",
                table: "Comparisons",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ProjectId",
                table: "Sessions",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Differences");

            migrationBuilder.DropTable(
                name: "Comparisons");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}

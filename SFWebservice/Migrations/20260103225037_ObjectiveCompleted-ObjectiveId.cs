using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFWebservice.Migrations
{
    /// <inheritdoc />
    public partial class ObjectiveCompletedObjectiveId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObjectiveId",
                table: "ObjectiveCompleted",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObjectiveCompleted_ObjectiveId",
                table: "ObjectiveCompleted",
                column: "ObjectiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveCompleted_Objective_ObjectiveId",
                table: "ObjectiveCompleted",
                column: "ObjectiveId",
                principalTable: "Objective",
                principalColumn: "ObjectiveID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveCompleted_Objective_ObjectiveId",
                table: "ObjectiveCompleted");

            migrationBuilder.DropIndex(
                name: "IX_ObjectiveCompleted_ObjectiveId",
                table: "ObjectiveCompleted");

            migrationBuilder.DropColumn(
                name: "ObjectiveId",
                table: "ObjectiveCompleted");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFWebservice.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hit_Entity",
                table: "Hit");


            migrationBuilder.AlterColumn<bool>(
                name: "EntityHitID",
                table: "Hit",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Hit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hit_PlayerId",
                table: "Hit",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hit_Player_PlayerId",
                table: "Hit",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hit_Player_PlayerId",
                table: "Hit");

            migrationBuilder.DropIndex(
                name: "IX_Hit_PlayerId",
                table: "Hit");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Hit");

            migrationBuilder.AlterColumn<int>(
                name: "EntityHitID",
                table: "Hit",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");


            migrationBuilder.AddForeignKey(
                name: "FK_Hit_Entity",
                table: "Hit",
                column: "EntityHitID",
                principalTable: "Player",
                principalColumn: "PlayerID");
        }
    }
}

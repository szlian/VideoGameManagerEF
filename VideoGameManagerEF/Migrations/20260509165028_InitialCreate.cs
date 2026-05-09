using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameManagerEF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Developers_Nintendo.Id",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Nintendo.Id",
                table: "Games",
                newName: "DeveloperId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Nintendo.Id",
                table: "Games",
                newName: "IX_Games_DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "Games",
                newName: "Nintendo.Id");

            migrationBuilder.RenameIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                newName: "IX_Games_Nintendo.Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Developers_Nintendo.Id",
                table: "Games",
                column: "Nintendo.Id",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

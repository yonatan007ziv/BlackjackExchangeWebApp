using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackjackExchangeWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class update6Ithink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_UserDbModelId",
                table: "Threads");

            migrationBuilder.RenameColumn(
                name: "UserDbModelId",
                table: "Threads",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UserDbModelId",
                table: "Threads",
                newName: "IX_Threads_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_UserId",
                table: "Threads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_UserId",
                table: "Threads");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Threads",
                newName: "UserDbModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UserId",
                table: "Threads",
                newName: "IX_Threads_UserDbModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_UserDbModelId",
                table: "Threads",
                column: "UserDbModelId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackjackExchangeWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class update4Ithink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Threads",
                newName: "UserDbModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_AuthorId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_UserDbModelId",
                table: "Threads");

            migrationBuilder.RenameColumn(
                name: "UserDbModelId",
                table: "Threads",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UserDbModelId",
                table: "Threads",
                newName: "IX_Threads_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

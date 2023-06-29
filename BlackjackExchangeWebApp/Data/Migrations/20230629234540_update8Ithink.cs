using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackjackExchangeWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class update8Ithink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Threads",
                newName: "ThreadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThreadId",
                table: "Threads",
                newName: "Id");
        }
    }
}

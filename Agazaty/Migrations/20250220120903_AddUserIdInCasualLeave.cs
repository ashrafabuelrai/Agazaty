using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agazaty.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInCasualLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CasualLeaves",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CasualLeaves");
        }
    }
}

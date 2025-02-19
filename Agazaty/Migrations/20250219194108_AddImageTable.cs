using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agazaty.Migrations
{
    /// <inheritdoc />
    public partial class AddImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "PermitLeaves",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "PermitLeaveImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitLeaveImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitLeaveImages_PermitLeaves_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "PermitLeaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermitLeaveImages_LeaveId",
                table: "PermitLeaveImages",
                column: "LeaveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermitLeaveImages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PermitLeaves",
                newName: "ImageUrl");
        }
    }
}

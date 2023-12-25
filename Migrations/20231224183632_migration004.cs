using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_AspNetUsers_DestinataireId",
                table: "Invitations");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_AspNetUsers_DestinataireId",
                table: "Invitations",
                column: "DestinataireId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_AspNetUsers_DestinataireId",
                table: "Invitations");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_AspNetUsers_DestinataireId",
                table: "Invitations",
                column: "DestinataireId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

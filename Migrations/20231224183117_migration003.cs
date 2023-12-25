using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amis_AspNetUsers_UtilisateurId",
                table: "Amis");

            migrationBuilder.AddForeignKey(
                name: "FK_Amis_AspNetUsers_UtilisateurId",
                table: "Amis",
                column: "UtilisateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amis_AspNetUsers_UtilisateurId",
                table: "Amis");

            migrationBuilder.AddForeignKey(
                name: "FK_Amis_AspNetUsers_UtilisateurId",
                table: "Amis",
                column: "UtilisateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

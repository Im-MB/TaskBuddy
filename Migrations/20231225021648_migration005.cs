using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UtilisateurId1",
                table: "Amis",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amis_UtilisateurId1",
                table: "Amis",
                column: "UtilisateurId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Amis_AspNetUsers_UtilisateurId1",
                table: "Amis",
                column: "UtilisateurId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amis_AspNetUsers_UtilisateurId1",
                table: "Amis");

            migrationBuilder.DropIndex(
                name: "IX_Amis_UtilisateurId1",
                table: "Amis");

            migrationBuilder.DropColumn(
                name: "UtilisateurId1",
                table: "Amis");
        }
    }
}

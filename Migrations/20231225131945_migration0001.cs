using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TachesPartagees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TacheId = table.Column<int>(type: "int", nullable: false),
                    ExpediteurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DestinataireId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TachesPartagees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TachesPartagees_AspNetUsers_DestinataireId",
                        column: x => x.DestinataireId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TachesPartagees_AspNetUsers_ExpediteurId",
                        column: x => x.ExpediteurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TachesPartagees_Tasks_TacheId",
                        column: x => x.TacheId,
                        principalTable: "Tasks",
                        principalColumn: "IdTask",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TachesPartagees_DestinataireId",
                table: "TachesPartagees",
                column: "DestinataireId");

            migrationBuilder.CreateIndex(
                name: "IX_TachesPartagees_ExpediteurId",
                table: "TachesPartagees",
                column: "ExpediteurId");

            migrationBuilder.CreateIndex(
                name: "IX_TachesPartagees_TacheId",
                table: "TachesPartagees",
                column: "TacheId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TachesPartagees");
        }
    }
}

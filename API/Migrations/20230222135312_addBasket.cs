using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Users_UserId",
                table: "Quests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Quests",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_Quests_UserId",
                table: "Quests",
                newName: "IX_Quests_BasketId");

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Baskets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Baskets_BasketId",
                table: "Quests",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Baskets_BasketId",
                table: "Quests");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "Quests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Quests_BasketId",
                table: "Quests",
                newName: "IX_Quests_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Users_UserId",
                table: "Quests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

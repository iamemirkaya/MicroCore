using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroCore.Basket.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountRate = table.Column<float>(type: "real", nullable: true),
                    Coupon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PriceByApplyDiscountRate = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    BasketUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketUserId",
                        column: x => x.BasketUserId,
                        principalTable: "Baskets",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketUserId",
                table: "BasketItems",
                column: "BasketUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Baskets");
        }
    }
}

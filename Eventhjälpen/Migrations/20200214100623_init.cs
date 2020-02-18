using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventhjälpen.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    EventType = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeTypeName = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Lastname = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Phonenumber = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    CategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Products__Catego__2A4B4B5E",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeTypeID = table.Column<int>(nullable: true),
                    RecipeName = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    EstimatedTime = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Recipe__RecipeTy__32E0915F",
                        column: x => x.RecipeTypeID,
                        principalTable: "RecipeType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: true),
                    SumToPay = table.Column<int>(nullable: true),
                    CurrentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Orders__UserID__25869641",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(nullable: true),
                    EventID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK__EventDeta__Event__3C69FB99",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__EventDeta__Recip__3B75D760",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK__RecipeDet__Produ__36B12243",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__RecipeDet__Recip__35BCFE0A",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orderdetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersID = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orderdetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Orderdeta__Order__2D27B809",
                        column: x => x.OrdersID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Orderdeta__Produ__2E1BDC42",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventDetails_EventID",
                table: "EventDetails",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetails_RecipeID",
                table: "EventDetails",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orderdetails_OrdersID",
                table: "Orderdetails",
                column: "OrdersID");

            migrationBuilder.CreateIndex(
                name: "IX_Orderdetails_ProductID",
                table: "Orderdetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RecipeTypeID",
                table: "Recipe",
                column: "RecipeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_ProductID",
                table: "RecipeDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_RecipeID",
                table: "RecipeDetails",
                column: "RecipeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventDetails");

            migrationBuilder.DropTable(
                name: "Orderdetails");

            migrationBuilder.DropTable(
                name: "RecipeDetails");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "RecipeType");
        }
    }
}

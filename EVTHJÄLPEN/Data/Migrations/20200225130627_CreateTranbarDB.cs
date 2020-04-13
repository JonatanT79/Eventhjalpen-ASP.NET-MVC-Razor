using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EVTHJÄLPEN.Data.Migrations
{
    public partial class CreateTranbarDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Measurement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
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
                name: "UserAdress",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CareOf = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    EstimatedTime = table.Column<int>(nullable: true),
                    RecipeTypeID = table.Column<int>(nullable: true)
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
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Lastname = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Phonenumber = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    UserAdressID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_UserAdress_UserAdressID",
                        column: x => x.UserAdressID,
                        principalTable: "UserAdress",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    ProductQuantity = table.Column<decimal>(nullable: false),
                    RecipeID = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true),
                    MeasurementUnitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecipeDetails_MeasurementUnit_MeasurementUnitID",
                        column: x => x.MeasurementUnitID,
                        principalTable: "MeasurementUnit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "RecipeSteps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stepnumber = table.Column<int>(nullable: false),
                    Instructions = table.Column<string>(nullable: true),
                    RecipeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSteps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecipeSteps_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SumToPay = table.Column<int>(nullable: true),
                    CurrentDate = table.Column<DateTime>(nullable: true),
                    AspUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orderdetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersID = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true),
                    Amount = table.Column<int>(nullable: true)
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
                name: "IX_Recipe_RecipeTypeID",
                table: "Recipe",
                column: "RecipeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_MeasurementUnitID",
                table: "RecipeDetails",
                column: "MeasurementUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_ProductID",
                table: "RecipeDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_RecipeID",
                table: "RecipeDetails",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_RecipeID",
                table: "RecipeSteps",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserAdressID",
                table: "Users",
                column: "UserAdressID");
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
                name: "RecipeSteps");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "MeasurementUnit");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RecipeType");

            migrationBuilder.DropTable(
                name: "UserAdress");
        }
    }
}

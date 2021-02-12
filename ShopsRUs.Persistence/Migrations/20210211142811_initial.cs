using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopsRUs.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsAnAffliate = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAnEmployee = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Percentage = table.Column<double>(type: "REAL", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "IsAnAffliate", "IsAnEmployee", "Name" },
                values: new object[] { new Guid("e59c73c3-cfca-42e0-8e5a-59efcc2eba8e"), new DateTime(2021, 2, 11, 15, 28, 10, 877, DateTimeKind.Local).AddTicks(9515), true, false, "John" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "IsAnAffliate", "IsAnEmployee", "Name" },
                values: new object[] { new Guid("a136f64e-f9ac-46fb-8db8-cbef60af30cd"), new DateTime(2021, 2, 11, 15, 28, 10, 878, DateTimeKind.Local).AddTicks(8715), false, true, "Alice" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "IsAnAffliate", "IsAnEmployee", "Name" },
                values: new object[] { new Guid("a50a1f19-4d5d-490f-8083-f63a23856f5b"), new DateTime(2021, 2, 11, 15, 28, 10, 878, DateTimeKind.Local).AddTicks(8732), false, false, "Doe" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "IsAnAffliate", "IsAnEmployee", "Name" },
                values: new object[] { new Guid("47a1ee21-2409-476d-8cc9-6c1af7431ebd"), new DateTime(2018, 2, 11, 15, 28, 10, 878, DateTimeKind.Local).AddTicks(8735), false, false, "Max" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Amount", "CreatedAt", "Name", "Percentage", "Type" },
                values: new object[] { new Guid("babeeb39-ef8d-464a-aae7-cdcb511bf75d"), 0m, new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4595), "Affliate Discount", 10.0, "affliatediscount" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Amount", "CreatedAt", "Name", "Percentage", "Type" },
                values: new object[] { new Guid("25489664-525c-4540-906d-54b86056b4d6"), 0m, new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4622), "Employee Discount", 30.0, "employeediscount" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Amount", "CreatedAt", "Name", "Percentage", "Type" },
                values: new object[] { new Guid("bcc7c913-1233-4136-9abd-1dd88d647d0a"), 0m, new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4625), "Old Customer Discount", 5.0, "oldCustomerdiscount" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Amount", "CreatedAt", "Name", "Percentage", "Type" },
                values: new object[] { new Guid("c8847be1-1206-4131-ac3a-bfc9ae516f14"), 0m, new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4627), "Percent Per $100 Bill Discount", 5.0, "PercentPer100DollarDiscount" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

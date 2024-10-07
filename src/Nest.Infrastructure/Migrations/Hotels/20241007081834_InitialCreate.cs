using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nest.Infrastructure.Migrations.Hotels
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HotelName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationCode = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfos_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HotelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "HotelName" },
                values: new object[,]
                {
                    { new Guid("27dd1fe5-c906-4347-bfa0-67a109082b4f"), "Test 5" },
                    { new Guid("31de4b1d-fa48-4594-b85f-f43af0621dde"), "Test 6" },
                    { new Guid("37c26834-c2fd-4574-843a-38fdd9474eb6"), "Test 1" },
                    { new Guid("53950c18-cb4e-462b-81e4-00aa4fd8dd62"), "Test 9" },
                    { new Guid("5988e09b-38e0-4769-b180-73fffbbd8d6b"), "Test 3" },
                    { new Guid("873d9ced-962f-4ab2-94a5-5e19d401e604"), "Test 8" },
                    { new Guid("b9dc2b4c-5ab5-46bc-b523-6cd9e698e47c"), "Test 2" },
                    { new Guid("c7ea298b-ff64-4688-a172-2f581a1115bb"), "Test 4" },
                    { new Guid("d8c31825-06d1-4a9e-b8a3-01aa3ba87299"), "Test 7" },
                    { new Guid("dec8da0e-903c-4cab-9afc-0487f77d7069"), "Test 10" }
                });

            migrationBuilder.InsertData(
                table: "ContactInfos",
                columns: new[] { "Id", "Content", "HotelId", "LocationCode", "Type" },
                values: new object[,]
                {
                    { new Guid("0fc19e82-5d70-4998-91e1-bcafbbfe9a4b"), "İstanbul", new Guid("27dd1fe5-c906-4347-bfa0-67a109082b4f"), 34, 3 },
                    { new Guid("5243226f-6501-4a0f-af3a-dcc98668095d"), "İstanbul", new Guid("37c26834-c2fd-4574-843a-38fdd9474eb6"), 34, 3 },
                    { new Guid("55b66f8f-1466-48b0-8254-d1f3861cd0e7"), "İstanbul", new Guid("c7ea298b-ff64-4688-a172-2f581a1115bb"), 34, 3 },
                    { new Guid("7bbcedba-54a8-441e-a08b-14818c1ec8ee"), "İstanbul", new Guid("b9dc2b4c-5ab5-46bc-b523-6cd9e698e47c"), 34, 3 },
                    { new Guid("c97de36f-d632-42bc-b4d4-d90f35dbb1e0"), "İstanbul", new Guid("5988e09b-38e0-4769-b180-73fffbbd8d6b"), 34, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_HotelId",
                table: "ContactInfos",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_HotelId",
                table: "Managers",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}

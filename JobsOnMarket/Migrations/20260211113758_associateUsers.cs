using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobsOnMarket.Migrations
{
    /// <inheritdoc />
    public partial class associateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-0000-4543-0000-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-1111-4543-1111-9443d048cdb9");

            migrationBuilder.CreateTable(
                name: "ContractorUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContractorUser_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerUser_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445800-0000-4543-0000-9443d048cdb9", 0, "8e445865-0000-aaaa-0000-9443d048cd00", "customer@customer.com", false, false, null, "CUSTOMER@CUSTOMER.COM", "CUSTOMER@CUSTOMER.COM", "aPassword123!", null, false, "8e445865-0000-aaaa-0000-9443d048cdb9", false, "customer@customer.com" },
                    { "8e445800-1111-4543-1111-9443d048cdb9", 0, "8e445865-1111-aaaa-1111-9443d048cd00", "Contractor@Contractor.com", false, false, null, "CONTRACTOR@CONTRACTOR.COM", "CONTRACTOR@CONTRACTOR.COM", "bPassword123!", null, false, "8e445865-1111-aaaa-1111-9443d048cdb9", false, "Contractor@Contractor.com" },
                    { "8e445801-0000-4543-0000-9443d048cdb9", 0, "8e445865-0000-aaaa-0000-9443d048cdaa", "acustomer@customer.com", false, false, null, "ACUSTOMER@CUSTOMER.COM", "ACUSTOMER@CUSTOMER.COM", "aPassword123!", null, false, "8e445865-0000-aaaa-0000-9443d048cdaa", false, "acustomer@customer.com" },
                    { "8e445801-1111-4543-1111-9443d048cdb9", 0, "8e445865-1111-aaaa-1111-9443d048cd11", "acontractor@Contractor.com", false, false, null, "ACONTRACTOR@CONTRACTOR.COM", "ACONTRACTOR@CONTRACTOR.COM", "bPassword123!", null, false, "8e445865-1111-aaaa-1111-9443d048cd11", false, "acontractor@Contractor.com" }
                });

            migrationBuilder.InsertData(
                table: "ContractorUser",
                columns: new[] { "ID", "ContractorId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "8e445801-0000-4543-0000-9443d048cdb9" },
                    { 2, 2, "8e445801-1111-4543-1111-9443d048cdb9" }
                });

            migrationBuilder.InsertData(
                table: "CustomerUser",
                columns: new[] { "ID", "CustomerId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "8e445800-0000-4543-0000-9443d048cdb9" },
                    { 2, 2, "8e445800-1111-4543-1111-9443d048cdb9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorUser_ContractorId",
                table: "ContractorUser",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUser_CustomerId",
                table: "CustomerUser",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorUser");

            migrationBuilder.DropTable(
                name: "CustomerUser");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445800-0000-4543-0000-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445800-1111-4543-1111-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445801-0000-4543-0000-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445801-1111-4543-1111-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-0000-4543-0000-9443d048cdb9", 0, "8e445865-0000-aaaa-0000-9443d048cd00", "customer@customer.com", false, false, null, "CUSTOMER@CUSTOMER.COM", "CUSTOMER@CUSTOMER.COM", "aPassword123!", null, false, "8e445865-0000-aaaa-0000-9443d048cdb9", false, "customer@customer.com" },
                    { "8e445865-1111-4543-1111-9443d048cdb9", 0, "8e445865-1111-aaaa-1111-9443d048cd00", "Contractor@Contractor.com", false, false, null, "CONTRACTOR@CONTRACTOR.COM", "CONTRACTOR@CONTRACTOR.COM", "bPassword123!", null, false, "8e445865-1111-aaaa-1111-9443d048cdb9", false, "Contractor@Contractor.com" }
                });
        }
    }
}

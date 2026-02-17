using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobsOnMarket.Migrations
{
    /// <inheritdoc />
    public partial class associatedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e445865-0000-0000-0000-9443d048cdb9", "8e445865-0000-4543-0000-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e445865-aaaa-aaaa-aaaa-9443d048cdb9", "8e445865-1111-4543-1111-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8e445865-0000-0000-0000-9443d048cdb9", "8e445800-0000-4543-0000-9443d048cdb9" },
                    { "8e445865-aaaa-aaaa-aaaa-9443d048cdb9", "8e445800-1111-4543-1111-9443d048cdb9" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445800-1111-4543-1111-9443d048cdb9",
                columns: new[] { "Email", "UserName" },
                values: new object[] { "Contractor@contractor.com", "Contractor@contractor.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445801-1111-4543-1111-9443d048cdb9",
                columns: new[] { "Email", "UserName" },
                values: new object[] { "acontractor@contractor.com", "acontractor@contractor.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e445865-0000-0000-0000-9443d048cdb9", "8e445800-0000-4543-0000-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e445865-aaaa-aaaa-aaaa-9443d048cdb9", "8e445800-1111-4543-1111-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8e445865-0000-0000-0000-9443d048cdb9", "8e445865-0000-4543-0000-9443d048cdb9" },
                    { "8e445865-aaaa-aaaa-aaaa-9443d048cdb9", "8e445865-1111-4543-1111-9443d048cdb9" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445800-1111-4543-1111-9443d048cdb9",
                columns: new[] { "Email", "UserName" },
                values: new object[] { "Contractor@Contractor.com", "Contractor@Contractor.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445801-1111-4543-1111-9443d048cdb9",
                columns: new[] { "Email", "UserName" },
                values: new object[] { "acontractor@Contractor.com", "acontractor@Contractor.com" });
        }
    }
}

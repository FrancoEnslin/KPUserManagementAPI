using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPUserManagementAPI.Migrations
{
    public partial class SecondSeedingForGroupPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupPermissionId", "GroupId", "PermissionId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupPermissionId", "GroupId", "PermissionId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupPermissionId", "GroupId", "PermissionId" },
                values: new object[] { 3, 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "GroupPermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "GroupPermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "GroupPermissionId",
                keyValue: 3);
        }
    }
}

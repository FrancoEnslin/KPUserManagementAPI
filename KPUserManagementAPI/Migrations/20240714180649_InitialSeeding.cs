using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPUserManagementAPI.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "GroupName" },
                values: new object[,]
                {
                    { 1, "Admin Group" },
                    { 2, "User Group" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "PermissionName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2);
        }
    }
}

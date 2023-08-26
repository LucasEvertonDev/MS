using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MS.Services.Auth.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncludeUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Situation" },
                values: new object[] { new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), "CHANGE_STUDENTS", 1 });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Id", "Description", "Name", "Situation" },
                values: new object[,]
                {
                    { new Guid("2c2ab8a3-3665-42ef-b4ef-bbec05ac02a5"), "Usuario do sistema", "Customer", 1 },
                    { new Guid("f97e565d-08af-4281-bc11-c0206eae06fa"), "Administrador do sistema", "Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("317979f2-0ea0-4c25-9f77-1a9e8318e586"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("317979f2-0ea0-4c25-9f77-1a9e8318e586"));

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("2c2ab8a3-3665-42ef-b4ef-bbec05ac02a5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"));

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("f97e565d-08af-4281-bc11-c0206eae06fa"));
        }
    }
}

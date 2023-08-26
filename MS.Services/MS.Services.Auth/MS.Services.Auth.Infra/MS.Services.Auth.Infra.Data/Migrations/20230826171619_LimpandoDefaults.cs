using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Services.Auth.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class LimpandoDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("e021c8c2-c01c-4cbe-b5fb-b160dbad1e4d"));

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("b94afe49-6630-4bf8-a19d-923af259f475"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("b94afe49-6630-4bf8-a19d-923af259f475"));

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("e021c8c2-c01c-4cbe-b5fb-b160dbad1e4d"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }
    }
}

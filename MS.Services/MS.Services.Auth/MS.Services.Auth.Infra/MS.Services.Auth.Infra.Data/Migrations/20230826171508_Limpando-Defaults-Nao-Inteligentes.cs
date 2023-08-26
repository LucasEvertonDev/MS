using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Services.Auth.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class LimpandoDefaultsNaoInteligentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientsCredentials",
                keyColumn: "Id",
                keyValue: new Guid("0c645cf4-baae-4ee4-ba88-67291c7331c1"));

            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f79a0a1-3d21-453f-824f-715ec7e7e051"));

            migrationBuilder.InsertData(
                table: "ClientsCredentials",
                columns: new[] { "Id", "ClientDescription", "ClientId", "ClientSecret", "CreateDate", "Situation", "UpdateDate" },
                values: new object[] { new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"), "Cliente padrão da aplicação", "7064bbbf-5d11-4782-9009-95e5a6fd6822", "dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910", new DateTime(2023, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null });

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("b94afe49-6630-4bf8-a19d-923af259f475"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientsCredentials",
                keyColumn: "Id",
                keyValue: new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"));

            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("b94afe49-6630-4bf8-a19d-923af259f475"));

            migrationBuilder.InsertData(
                table: "ClientsCredentials",
                columns: new[] { "Id", "ClientDescription", "ClientId", "ClientSecret", "CreateDate", "Situation", "UpdateDate" },
                values: new object[] { new Guid("0c645cf4-baae-4ee4-ba88-67291c7331c1"), "Cliente padrão da aplicação", "7064bbbf-5d11-4782-9009-95e5a6fd6822", "dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910", new DateTime(2023, 8, 26, 14, 12, 52, 731, DateTimeKind.Local).AddTicks(8680), 1, null });

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("3f79a0a1-3d21-453f-824f-715ec7e7e051"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }
    }
}

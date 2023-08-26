using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Services.Auth.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncludeDataCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("e202323c-890e-4598-839d-19ef9a24f459"));

            migrationBuilder.InsertData(
                table: "ClientsCredentials",
                columns: new[] { "Id", "ClientDescription", "ClientId", "ClientSecret", "CreateDate", "Situation", "UpdateDate" },
                values: new object[] { new Guid("c8ae3d5a-521f-4278-a977-431c84a51aa5"), "Cliente padrão da aplicação", "7064bbbf-5d11-4782-9009-95e5a6fd6822", "dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910", new DateTime(2023, 8, 26, 14, 0, 9, 681, DateTimeKind.Local).AddTicks(275), 1, null });

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("8a758628-40f1-4df5-a57f-5dfe9c8257e8"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientsCredentials",
                keyColumn: "Id",
                keyValue: new Guid("c8ae3d5a-521f-4278-a977-431c84a51aa5"));

            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("8a758628-40f1-4df5-a57f-5dfe9c8257e8"));

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("e202323c-890e-4598-839d-19ef9a24f459"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }
    }
}

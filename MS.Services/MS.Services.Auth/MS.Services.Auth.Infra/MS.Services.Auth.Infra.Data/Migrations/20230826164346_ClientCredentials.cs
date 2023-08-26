using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Services.Auth.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClientCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("317979f2-0ea0-4c25-9f77-1a9e8318e586"));

            migrationBuilder.CreateTable(
                name: "ClientsCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientSecret = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ClientDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Situation = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsCredentials", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("e202323c-890e-4598-839d-19ef9a24f459"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientsCredentials");

            migrationBuilder.DeleteData(
                table: "MapUserGroupRoles",
                keyColumn: "Id",
                keyValue: new Guid("e202323c-890e-4598-839d-19ef9a24f459"));

            migrationBuilder.InsertData(
                table: "MapUserGroupRoles",
                columns: new[] { "Id", "RoleId", "Situation", "UserGroupId" },
                values: new object[] { new Guid("317979f2-0ea0-4c25-9f77-1a9e8318e586"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1, new Guid("f97e565d-08af-4281-bc11-c0206eae06fa") });
        }
    }
}

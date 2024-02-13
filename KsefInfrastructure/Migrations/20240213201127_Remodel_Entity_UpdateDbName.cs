using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KsefInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remodel_Entity_UpdateDbName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientSession",
                table: "ClientSession");

            migrationBuilder.RenameTable(
                name: "ClientSession",
                newName: "AuthorizationToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorizationToken",
                table: "AuthorizationToken",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorizationToken",
                table: "AuthorizationToken");

            migrationBuilder.RenameTable(
                name: "AuthorizationToken",
                newName: "ClientSession");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientSession",
                table: "ClientSession",
                column: "Id");
        }
    }
}

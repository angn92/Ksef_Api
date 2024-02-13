using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KsefInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewProperty_AuthToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AuthorizationToken",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AuthorizationToken");
        }
    }
}

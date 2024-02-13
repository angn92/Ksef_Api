using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KsefInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remodel_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Contractor");

            migrationBuilder.DropColumn(
                name: "SessionStatus",
                table: "ClientSession");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "ClientSession",
                newName: "Token");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ClientSession",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "ClientSession",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nip",
                table: "ClientSession",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "ClientSession");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "ClientSession");

            migrationBuilder.DropColumn(
                name: "Nip",
                table: "ClientSession");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "ClientSession",
                newName: "SessionId");

            migrationBuilder.AddColumn<int>(
                name: "SessionStatus",
                table: "ClientSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Contractor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ContractorId",
                table: "Address",
                column: "ContractorId",
                unique: true);
        }
    }
}

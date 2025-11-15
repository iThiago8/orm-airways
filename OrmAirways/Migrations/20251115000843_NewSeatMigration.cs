using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrmAirways.Migrations
{
    /// <inheritdoc />
    public partial class NewSeatMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AircraftID1",
                table: "Seat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Seat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_AircraftID1",
                table: "Seat",
                column: "AircraftID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Aircraft_AircraftID1",
                table: "Seat",
                column: "AircraftID1",
                principalTable: "Aircraft",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Aircraft_AircraftID1",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_AircraftID1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "AircraftID1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Seat");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrmAirways.Migrations
{
    /// <inheritdoc />
    public partial class ChangingNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isVIP",
                table: "Customer",
                newName: "IsVIP");

            migrationBuilder.RenameColumn(
                name: "Seat",
                table: "Booking",
                newName: "SeatID");

            migrationBuilder.RenameColumn(
                name: "Seats",
                table: "Aircraft",
                newName: "SeatNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SeatID",
                table: "Booking",
                column: "SeatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Seat_SeatID",
                table: "Booking",
                column: "SeatID",
                principalTable: "Seat",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Seat_SeatID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_SeatID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "IsVIP",
                table: "Customer",
                newName: "isVIP");

            migrationBuilder.RenameColumn(
                name: "SeatID",
                table: "Booking",
                newName: "Seat");

            migrationBuilder.RenameColumn(
                name: "SeatNumber",
                table: "Aircraft",
                newName: "Seats");
        }
    }
}

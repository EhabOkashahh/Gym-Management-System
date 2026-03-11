using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSessionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservedSeats",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Sessions",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedSeats",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Sessions");
        }
    }
}

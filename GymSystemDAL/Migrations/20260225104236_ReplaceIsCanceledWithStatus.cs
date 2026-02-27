using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceIsCanceledWithStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "MemberShips");

            migrationBuilder.AddColumn<int>(
                name: "MemberShipStatus",
                table: "MemberShips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberShipStatus",
                table: "MemberShips");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "MemberShips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

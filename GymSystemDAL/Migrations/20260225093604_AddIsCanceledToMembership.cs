using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCanceledToMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "MemberShips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "MemberShips");
        }
    }
}

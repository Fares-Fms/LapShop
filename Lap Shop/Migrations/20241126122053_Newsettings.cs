using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lap_Shop.Migrations
{
    /// <inheritdoc />
    public partial class Newsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TbUserModel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "TbHomeSettings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbHomeSettings",
                table: "TbHomeSettings",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TbHomeSettings",
                table: "TbHomeSettings");

            migrationBuilder.DropColumn(
                name: "id",
                table: "TbHomeSettings");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TbUserModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}

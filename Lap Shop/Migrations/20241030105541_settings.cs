using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lap_Shop.Migrations
{
    /// <inheritdoc />
    public partial class settings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems");

            migrationBuilder.AlterColumn<int>(
                name: "RamSize",
                table: "TbItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OsId",
                table: "TbItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TbHomeSettings",
                columns: table => new
                {
                    FirstSlide = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecoundSlide = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdSlide = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeSlide = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems",
                column: "OsId",
                principalTable: "TbOs",
                principalColumn: "OsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems");

            migrationBuilder.DropTable(
                name: "TbHomeSettings");

            migrationBuilder.AlterColumn<int>(
                name: "RamSize",
                table: "TbItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OsId",
                table: "TbItems",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems",
                column: "OsId",
                principalTable: "TbOs",
                principalColumn: "OsId");
        }
    }
}

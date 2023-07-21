using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTS_EDU_FINAL.Migrations
{
    /// <inheritdoc />
    public partial class update_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHoc_TaiKhoan_TaiKhoanID",
                table: "DangKyHoc");

            migrationBuilder.AlterColumn<int>(
                name: "TaiKhoanID",
                table: "DangKyHoc",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHoc_TaiKhoan_TaiKhoanID",
                table: "DangKyHoc",
                column: "TaiKhoanID",
                principalTable: "TaiKhoan",
                principalColumn: "TaiKhoanID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHoc_TaiKhoan_TaiKhoanID",
                table: "DangKyHoc");

            migrationBuilder.AlterColumn<int>(
                name: "TaiKhoanID",
                table: "DangKyHoc",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHoc_TaiKhoan_TaiKhoanID",
                table: "DangKyHoc",
                column: "TaiKhoanID",
                principalTable: "TaiKhoan",
                principalColumn: "TaiKhoanID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTS_EDU_FINAL.Migrations
{
    /// <inheritdoc />
    public partial class update_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhoaHoc_LoaiKhoaHoc_LoaiKhoaHocID",
                table: "KhoaHoc");

            migrationBuilder.AlterColumn<int>(
                name: "ThoiGianHoc",
                table: "KhoaHoc",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenKhoaHoc",
                table: "KhoaHoc",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SoLuongMon",
                table: "KhoaHoc",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "KhoaHoc",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoaiKhoaHocID",
                table: "KhoaHoc",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HocPhi",
                table: "KhoaHoc",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HinhAnh",
                table: "KhoaHoc",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GioiThieu",
                table: "KhoaHoc",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KhoaHoc_LoaiKhoaHoc_LoaiKhoaHocID",
                table: "KhoaHoc",
                column: "LoaiKhoaHocID",
                principalTable: "LoaiKhoaHoc",
                principalColumn: "LoaiKhoaHocID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhoaHoc_LoaiKhoaHoc_LoaiKhoaHocID",
                table: "KhoaHoc");

            migrationBuilder.AlterColumn<int>(
                name: "ThoiGianHoc",
                table: "KhoaHoc",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TenKhoaHoc",
                table: "KhoaHoc",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "SoLuongMon",
                table: "KhoaHoc",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "KhoaHoc",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LoaiKhoaHocID",
                table: "KhoaHoc",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "HocPhi",
                table: "KhoaHoc",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "HinhAnh",
                table: "KhoaHoc",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GioiThieu",
                table: "KhoaHoc",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_KhoaHoc_LoaiKhoaHoc_LoaiKhoaHocID",
                table: "KhoaHoc",
                column: "LoaiKhoaHocID",
                principalTable: "LoaiKhoaHoc",
                principalColumn: "LoaiKhoaHocID");
        }
    }
}

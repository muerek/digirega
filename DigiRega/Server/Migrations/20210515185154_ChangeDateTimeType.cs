using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiRega.Server.Migrations
{
    public partial class ChangeDateTimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SentAt",
                table: "Entries",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SentAt",
                table: "EmailMessages",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "EmailMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 234, 194, 168, 170, 219, 23, 171, 153, 7, 3, 232, 160, 200, 142, 100, 36, 215, 153, 32, 35, 239, 245, 112, 124, 192, 120, 17, 61, 227, 103, 171, 138, 206, 110, 50, 139, 78, 198, 52, 227, 133, 151, 59, 66, 5, 218, 40, 25, 158, 170, 127, 4, 62, 80, 72, 96, 10, 30, 240, 217, 192, 50, 47, 86 }, new byte[] { 208, 101, 190, 118, 176, 2, 21, 99, 21, 209, 73, 15, 84, 207, 158, 82, 64, 42, 33, 75, 244, 87, 9, 58, 163, 12, 59, 245, 119, 181, 174, 33, 107, 53, 29, 17, 185, 160, 48, 192, 141, 64, 229, 84, 81, 187, 185, 58, 106, 52, 223, 77, 174, 153, 21, 67, 63, 43, 195, 190, 134, 234, 177, 104, 68, 200, 22, 86, 154, 148, 200, 41, 62, 177, 108, 179, 134, 71, 221, 11, 205, 17, 113, 109, 195, 103, 73, 52, 23, 187, 83, 43, 0, 148, 204, 211, 198, 35, 112, 115, 160, 1, 102, 29, 69, 247, 203, 90, 241, 16, 82, 93, 38, 122, 78, 70, 205, 103, 218, 73, 12, 127, 235, 14, 61, 22, 76, 49 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                table: "Entries",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                table: "EmailMessages",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EmailMessages",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 91, 244, 113, 71, 192, 65, 116, 26, 224, 75, 68, 206, 57, 252, 195, 130, 92, 97, 233, 154, 136, 154, 246, 187, 170, 58, 122, 75, 111, 220, 240, 174, 145, 21, 77, 126, 170, 183, 80, 87, 59, 65, 192, 144, 20, 57, 125, 5, 113, 254, 250, 193, 164, 82, 64, 109, 193, 24, 220, 77, 104, 125, 245, 173 }, new byte[] { 138, 189, 26, 38, 34, 109, 222, 1, 85, 35, 222, 162, 39, 128, 123, 174, 189, 201, 146, 7, 80, 78, 233, 28, 23, 3, 249, 188, 237, 171, 88, 242, 18, 228, 251, 57, 234, 136, 31, 220, 54, 183, 42, 120, 30, 116, 63, 241, 47, 120, 14, 64, 113, 42, 85, 90, 175, 25, 248, 147, 46, 183, 47, 230, 254, 173, 88, 45, 201, 137, 6, 169, 27, 210, 204, 44, 186, 127, 21, 237, 197, 10, 112, 1, 23, 155, 236, 231, 29, 184, 16, 32, 217, 119, 150, 185, 173, 105, 60, 46, 206, 101, 221, 59, 220, 236, 0, 255, 59, 89, 141, 197, 16, 111, 22, 151, 120, 203, 192, 30, 203, 77, 63, 175, 74, 40, 242, 122 } });
        }
    }
}

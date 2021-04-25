using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiRega.Server.Migrations
{
    public partial class RemoveRaceLongName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongName",
                table: "Races");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Races",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 91, 244, 113, 71, 192, 65, 116, 26, 224, 75, 68, 206, 57, 252, 195, 130, 92, 97, 233, 154, 136, 154, 246, 187, 170, 58, 122, 75, 111, 220, 240, 174, 145, 21, 77, 126, 170, 183, 80, 87, 59, 65, 192, 144, 20, 57, 125, 5, 113, 254, 250, 193, 164, 82, 64, 109, 193, 24, 220, 77, 104, 125, 245, 173 }, new byte[] { 138, 189, 26, 38, 34, 109, 222, 1, 85, 35, 222, 162, 39, 128, 123, 174, 189, 201, 146, 7, 80, 78, 233, 28, 23, 3, 249, 188, 237, 171, 88, 242, 18, 228, 251, 57, 234, 136, 31, 220, 54, 183, 42, 120, 30, 116, 63, 241, 47, 120, 14, 64, 113, 42, 85, 90, 175, 25, 248, 147, 46, 183, 47, 230, 254, 173, 88, 45, 201, 137, 6, 169, 27, 210, 204, 44, 186, 127, 21, 237, 197, 10, 112, 1, 23, 155, 236, 231, 29, 184, 16, 32, 217, 119, 150, 185, 173, 105, 60, 46, 206, 101, 221, 59, 220, 236, 0, 255, 59, 89, 141, 197, 16, 111, 22, 151, 120, 203, 192, 30, 203, 77, 63, 175, 74, 40, 242, 122 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Races",
                newName: "ShortName");

            migrationBuilder.AddColumn<string>(
                name: "LongName",
                table: "Races",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 247, 76, 209, 160, 53, 214, 107, 180, 58, 27, 155, 226, 254, 95, 60, 157, 101, 197, 0, 82, 221, 22, 181, 145, 50, 10, 198, 186, 49, 42, 82, 218, 215, 86, 189, 95, 212, 223, 14, 197, 46, 155, 47, 169, 234, 251, 65, 151, 54, 160, 101, 233, 31, 169, 59, 44, 215, 22, 254, 249, 60, 167, 44, 5 }, new byte[] { 99, 139, 186, 68, 36, 17, 190, 173, 48, 238, 57, 149, 11, 6, 17, 74, 55, 136, 233, 113, 129, 155, 57, 13, 83, 207, 201, 76, 239, 215, 52, 179, 137, 164, 46, 66, 228, 76, 78, 73, 211, 124, 56, 165, 162, 73, 209, 73, 74, 160, 243, 186, 0, 142, 157, 9, 204, 202, 172, 41, 180, 169, 17, 25, 94, 9, 103, 194, 117, 45, 18, 135, 90, 1, 119, 57, 249, 118, 200, 217, 165, 15, 22, 11, 189, 89, 12, 102, 152, 142, 137, 137, 185, 112, 28, 189, 208, 9, 233, 107, 217, 87, 191, 177, 179, 2, 49, 46, 233, 171, 119, 163, 108, 28, 160, 109, 250, 198, 89, 162, 79, 231, 190, 163, 221, 139, 145, 183 } });
        }
    }
}

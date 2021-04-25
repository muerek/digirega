using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiRega.Server.Migrations
{
    public partial class RemoveEntryComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Users_SentById",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ManagerComment",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "OcComment",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "SentById",
                table: "Entries",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 247, 76, 209, 160, 53, 214, 107, 180, 58, 27, 155, 226, 254, 95, 60, 157, 101, 197, 0, 82, 221, 22, 181, 145, 50, 10, 198, 186, 49, 42, 82, 218, 215, 86, 189, 95, 212, 223, 14, 197, 46, 155, 47, 169, 234, 251, 65, 151, 54, 160, 101, 233, 31, 169, 59, 44, 215, 22, 254, 249, 60, 167, 44, 5 }, new byte[] { 99, 139, 186, 68, 36, 17, 190, 173, 48, 238, 57, 149, 11, 6, 17, 74, 55, 136, 233, 113, 129, 155, 57, 13, 83, 207, 201, 76, 239, 215, 52, 179, 137, 164, 46, 66, 228, 76, 78, 73, 211, 124, 56, 165, 162, 73, 209, 73, 74, 160, 243, 186, 0, 142, 157, 9, 204, 202, 172, 41, 180, 169, 17, 25, 94, 9, 103, 194, 117, 45, 18, 135, 90, 1, 119, 57, 249, 118, 200, 217, 165, 15, 22, 11, 189, 89, 12, 102, 152, 142, 137, 137, 185, 112, 28, 189, 208, 9, 233, 107, 217, 87, 191, 177, 179, 2, 49, 46, 233, 171, 119, 163, 108, 28, 160, 109, 250, 198, 89, 162, 79, 231, 190, 163, 221, 139, 145, 183 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Users_SentById",
                table: "Entries",
                column: "SentById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Users_SentById",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "SentById",
                table: "Entries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ManagerComment",
                table: "Entries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OcComment",
                table: "Entries",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 36, 241, 97, 207, 65, 14, 211, 94, 86, 121, 247, 30, 211, 119, 161, 61, 249, 52, 145, 193, 120, 186, 103, 37, 159, 145, 151, 218, 110, 24, 198, 203, 127, 68, 28, 247, 180, 84, 133, 249, 20, 20, 176, 230, 122, 31, 247, 61, 57, 45, 157, 94, 138, 63, 141, 185, 252, 4, 255, 173, 126, 20, 109, 76 }, new byte[] { 146, 130, 208, 136, 10, 72, 214, 3, 77, 164, 199, 253, 63, 183, 17, 213, 211, 253, 12, 205, 104, 121, 208, 136, 174, 252, 172, 70, 59, 165, 212, 3, 18, 159, 218, 35, 22, 253, 100, 82, 70, 48, 109, 105, 135, 11, 60, 126, 81, 217, 135, 129, 231, 153, 182, 17, 115, 239, 164, 208, 50, 24, 192, 253, 89, 246, 118, 199, 20, 54, 235, 27, 160, 40, 60, 115, 255, 1, 118, 181, 205, 232, 153, 186, 187, 144, 50, 187, 77, 162, 133, 6, 20, 17, 148, 135, 187, 144, 190, 53, 63, 67, 215, 148, 50, 108, 58, 179, 174, 88, 222, 106, 142, 11, 228, 253, 7, 186, 70, 129, 251, 78, 58, 11, 245, 119, 192, 152 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Users_SentById",
                table: "Entries",
                column: "SentById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

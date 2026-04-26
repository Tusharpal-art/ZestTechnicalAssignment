using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZestTechnicalAssignment.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _02_Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7012418b-8130-4533-b214-e0e449cd43aa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f90518a7-5e33-443f-b67b-895ee6499c74", "AQAAAAIAAYagAAAAEMHOU0ljZF4uKsvL+x+hTbWutGvjl5plOpHiAK6Db0tHwMGdx2zENOfQckn1+VM2gw==", "f7d0f668-637f-4ced-9d17-764ee9e79319" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7012418b-8130-4533-b214-e0e449cd43aa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e3bf7a3-d54a-4f6a-a3da-3f9a98f04bde", "AQAAAAIAAYagAAAAEFjfTsmBae7OsW9nlWTTzO95e+6u7ywiTLIY1GUSDEoH2lMm4nuKHpYN3t2ydj8VFQ==", "11573251-774e-4ffa-94fd-d5f43249f6b0" });
        }
    }
}

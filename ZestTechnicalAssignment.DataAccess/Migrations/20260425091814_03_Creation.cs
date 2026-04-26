using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZestTechnicalAssignment.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _03_Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7012418b-8130-4533-b214-e0e449cd43aa"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7012418b-8130-4533-b214-e0e449cd43aa", "7012418b-8130-4533-b214-e0e449cd43aa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7012418b-8130-4533-b214-e0e449cd43aa"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f90518a7-5e33-443f-b67b-895ee6499c74", "f7d0f668-637f-4ced-9d17-764ee9e79319" });
        }
    }
}

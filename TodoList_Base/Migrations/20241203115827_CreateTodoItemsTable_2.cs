using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList_Base.Migrations
{
    /// <inheritdoc />
    public partial class CreateTodoItemsTable_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 13, 58, 26, 581, DateTimeKind.Local).AddTicks(5341),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 13, 56, 43, 870, DateTimeKind.Local).AddTicks(9182));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 13, 56, 43, 870, DateTimeKind.Local).AddTicks(9182),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 13, 58, 26, 581, DateTimeKind.Local).AddTicks(5341));
        }
    }
}

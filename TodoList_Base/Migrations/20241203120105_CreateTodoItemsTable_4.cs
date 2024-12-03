using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList_Base.Migrations
{
    /// <inheritdoc />
    public partial class CreateTodoItemsTable_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 14, 0, 6, 920, DateTimeKind.Local).AddTicks(5238));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 14, 0, 6, 920, DateTimeKind.Local).AddTicks(5238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}

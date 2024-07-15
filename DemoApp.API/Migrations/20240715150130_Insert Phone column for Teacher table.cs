using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApp.API.Migrations
{
    /// <inheritdoc />
    public partial class InsertPhonecolumnforTeachertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

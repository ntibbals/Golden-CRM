using Microsoft.EntityFrameworkCore.Migrations;

namespace Golden_CRM.Migrations
{
    public partial class todo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "ToDos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "ToDos");
        }
    }
}

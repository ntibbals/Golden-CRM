using Microsoft.EntityFrameworkCore.Migrations;

namespace Golden_CRM.Migrations
{
    public partial class mvc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Notes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CustomerID",
                table: "Notes",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Customers_CustomerID",
                table: "Notes",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Customers_CustomerID",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CustomerID",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Notes");
        }
    }
}

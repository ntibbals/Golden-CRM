using Microsoft.EntityFrameworkCore.Migrations;

namespace Golden_CRM.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 1, "jd@doe.com", "John", "Doe", "555-555-5555" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}

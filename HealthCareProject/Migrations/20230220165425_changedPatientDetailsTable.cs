using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthCareProject.Migrations
{
    public partial class changedPatientDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PatientDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PatientDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PatientDetails");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PatientDetails");
        }
    }
}

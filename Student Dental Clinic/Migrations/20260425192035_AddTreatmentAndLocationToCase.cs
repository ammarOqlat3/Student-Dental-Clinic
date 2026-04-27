using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class AddTreatmentAndLocationToCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TreatmentType",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "TreatmentType",
                table: "Cases");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxGrade",
                table: "Reports",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxGrade",
                table: "Reports");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_StudentId",
                table: "Patients",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_StudentId",
                table: "Patients",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_StudentId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_StudentId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Patients");
        }
    }
}

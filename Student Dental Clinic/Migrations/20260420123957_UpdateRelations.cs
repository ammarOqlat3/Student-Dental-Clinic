using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_StudentId",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Students_StudentId",
                table: "Patients",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Students_StudentId",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_StudentId",
                table: "Patients",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

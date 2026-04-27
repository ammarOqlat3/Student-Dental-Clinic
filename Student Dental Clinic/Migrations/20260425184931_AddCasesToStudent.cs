using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class AddCasesToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_StudentId1",
                table: "Cases",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Students_StudentId1",
                table: "Cases",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Students_StudentId1",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_StudentId1",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Cases");
        }
    }
}

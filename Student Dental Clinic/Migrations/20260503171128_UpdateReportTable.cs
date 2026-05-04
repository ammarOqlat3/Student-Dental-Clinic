using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "DoctorFeedback",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AfterImagePath",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeforeImagePath",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChiefComplaint",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complications",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisNotes",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FollowUpPlan",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TreatmentDone",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterImagePath",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "BeforeImagePath",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ChiefComplaint",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Complications",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "DiagnosisNotes",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "FollowUpPlan",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "TreatmentDone",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorFeedback",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}

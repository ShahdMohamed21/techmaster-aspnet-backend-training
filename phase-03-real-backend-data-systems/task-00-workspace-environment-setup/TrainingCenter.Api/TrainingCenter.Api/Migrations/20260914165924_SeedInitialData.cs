using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingCenter.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 5, "Ahmed Hassan" },
                    { 6, "Sara Mohamed" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive" },
                values: new object[,]
                {
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "shahd@example.com", "Shahd Mohamed", true },
                    { 6, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed@example.com", "Ahmed Ali", true },
                    { 7, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariam@example.com", "Mariam Hassan", true },
                    { 8, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar@example.com", "Omar Khaled", true },
                    { 9, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "salma@example.com", "Salma Ahmed", true }
                });

            migrationBuilder.InsertData(
                table: "TrainingTracks",
                columns: new[] { "Id", "InstructorId", "Name" },
                values: new object[,]
                {
                    { 5, 5, "Backend Development" },
                    { 6, 6, "Frontend Development" },
                    { 7, 5, "AI Fundamentals" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "EnrollmentDate", "FinalGrade", "Status", "StudentId", "TrainingTrackId" },
                values: new object[,]
                {
                    { 8, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Active", 5, 5 },
                    { 9, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Active", 5, 7 },
                    { 10, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Active", 6, 5 },
                    { 11, new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Active", 7, 6 },
                    { 12, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Active", 8, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TrainingTracks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TrainingTracks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TrainingTracks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}

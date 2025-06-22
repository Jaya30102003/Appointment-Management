using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_Trial.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatientName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimeSlot = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}

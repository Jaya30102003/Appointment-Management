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
                    PatientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DoctorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimeSlot = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RecipientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NotificationTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NotificationMessage = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Recipient = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AppointmentId",
                table: "Notifications",
                column: "AppointmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sastt.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aircraft",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TailNumber = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircraft", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Username = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Role = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    AircraftId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Priority = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    AircraftId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    Priority = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IsResolved = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Defects_Aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PilotId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ScheduledFor = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Completed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSessions_Pilots_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    UserId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Action = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PilotCurrencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PilotId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CurrencyType = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PilotCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PilotCurrencies_Pilots_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AircraftId",
                table: "WorkOrders",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_AircraftId",
                table: "Defects",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_WorkOrderId",
                table: "Tasks",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_PilotId",
                table: "TrainingSessions",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PilotCurrencies_PilotId",
                table: "PilotCurrencies",
                column: "PilotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "PilotCurrencies");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropTable(
                name: "Aircraft");
        }
    }
}

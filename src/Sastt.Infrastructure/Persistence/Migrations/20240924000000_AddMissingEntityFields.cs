using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sastt.Infrastructure.Persistence.Migrations
{
    public partial class AddMissingEntityFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base",
                table: "Aircraft",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Aircraft",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "AuditLogs",
                type: "NVARCHAR2(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkOrderId",
                table: "Defects",
                type: "RAW(16)",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "Pilots",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AchievedDate",
                table: "PilotCurrencies",
                type: "TIMESTAMP",
                nullable: false,
                defaultValueSql: "SYSTIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tasks",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "TIMESTAMP",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TrainingSessions",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkOrders",
                type: "NVARCHAR2(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_WorkOrderId",
                table: "Defects",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_WorkOrders_WorkOrderId",
                table: "Defects",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_WorkOrders_WorkOrderId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_WorkOrderId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "Base",
                table: "Aircraft");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Aircraft");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "WorkOrderId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Pilots");

            migrationBuilder.DropColumn(
                name: "AchievedDate",
                table: "PilotCurrencies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkOrders");
        }
    }
}

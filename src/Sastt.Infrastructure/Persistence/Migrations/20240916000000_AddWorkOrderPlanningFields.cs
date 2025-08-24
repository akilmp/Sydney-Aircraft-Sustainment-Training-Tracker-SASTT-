using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sastt.Infrastructure.Persistence.Migrations
{
    public partial class AddWorkOrderPlanningFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "WorkOrders",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedStart",
                table: "WorkOrders",
                type: "TIMESTAMP",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedEnd",
                table: "WorkOrders",
                type: "TIMESTAMP",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStart",
                table: "WorkOrders",
                type: "TIMESTAMP",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualEnd",
                table: "WorkOrders",
                type: "TIMESTAMP",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "PlannedStart",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "PlannedEnd",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "ActualStart",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "ActualEnd",
                table: "WorkOrders");
        }
    }
}


using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Sastt.Domain;

namespace Sastt.Application.Reports;

public class WorkOrderReportService
{
    public byte[] GenerateCsv(IEnumerable<WorkOrder> workOrders)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Title,Aircraft,Tasks,Defects");
        foreach (var order in workOrders)
        {
            var aircraft = order.Aircraft?.TailNumber ?? string.Empty;
            sb.AppendLine($"{order.Id},{order.Title},{aircraft},{order.Tasks.Count},{order.Defects.Count}");
        }
        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public byte[] GeneratePdf(IEnumerable<WorkOrder> workOrders)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.ConstantColumn(60);
                        columns.ConstantColumn(60);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Id");
                        header.Cell().Element(CellStyle).Text("Title");
                        header.Cell().Element(CellStyle).Text("Aircraft");
                        header.Cell().Element(CellStyle).Text("Tasks");
                        header.Cell().Element(CellStyle).Text("Defects");
                    });

                    foreach (var order in workOrders)
                    {
                        table.Cell().Element(CellStyle).Text(order.Id.ToString());
                        table.Cell().Element(CellStyle).Text(order.Title);
                        table.Cell().Element(CellStyle).Text(order.Aircraft?.TailNumber ?? string.Empty);
                        table.Cell().Element(CellStyle).Text(order.Tasks.Count.ToString());
                        table.Cell().Element(CellStyle).Text(order.Defects.Count.ToString());
                    }
                });
            });
        });

        return document.GeneratePdf();
    }

    private static IContainer CellStyle(IContainer container)
        => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(2);
}


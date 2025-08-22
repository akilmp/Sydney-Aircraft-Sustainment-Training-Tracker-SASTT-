using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Sastt.Domain;

namespace Sastt.Application.Reports;

public class TrainingReportService
{
    public byte[] GenerateCsv(IEnumerable<TrainingSession> sessions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Pilot,Date,Hours");
        foreach (var session in sessions)
        {
            var pilot = session.Pilot?.Name ?? string.Empty;
            sb.AppendLine($"{session.Id},{pilot},{session.Date:yyyy-MM-dd},{session.Hours}");
        }
        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public byte[] GeneratePdf(IEnumerable<TrainingSession> sessions)
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
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(50);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Id");
                        header.Cell().Element(CellStyle).Text("Pilot");
                        header.Cell().Element(CellStyle).Text("Date");
                        header.Cell().Element(CellStyle).Text("Hours");
                    });

                    foreach (var session in sessions)
                    {
                        table.Cell().Element(CellStyle).Text(session.Id.ToString());
                        table.Cell().Element(CellStyle).Text(session.Pilot?.Name ?? string.Empty);
                        table.Cell().Element(CellStyle).Text(session.Date.ToString("yyyy-MM-dd"));
                        table.Cell().Element(CellStyle).Text(session.Hours.ToString());
                    }
                });
            });
        });

        return document.GeneratePdf();
    }

    private static IContainer CellStyle(IContainer container)
        => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(2);
}


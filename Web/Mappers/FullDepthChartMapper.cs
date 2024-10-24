using Application.Enums;
using Application.Models;
using Web.DTO;

public class FullDepthChartMapper
{
    public static FullChartDataResponse MapDepthChart(List<Depth> depthChartEntries)
    {
        var response = new FullChartDataResponse
        {
            TeamDepthData = depthChartEntries == null || !depthChartEntries.Any() ?  new Dictionary<string, List<DepthChartEntry>>() : 
            depthChartEntries
                .GroupBy(e => ((PositionAbbre)e.PositionId).ToString())
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => new DepthChartEntry
                    {
                        PlayerNumber = e.PlayerNumber,
                        PositionDepth = e.PositionDepth,
                    }).ToList()
                )
        };

        return response;
    }
}
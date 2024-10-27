namespace Web.DTO{

    public class DepthChartEntry
    {
        public int PlayerNumber { get; set; }
        public int PositionDepth { get; set; }
    }

    public class FullChartDataResponse{
        public Dictionary<string, List<DepthChartEntry>>? TeamDepthData { get; set; }
    }
}
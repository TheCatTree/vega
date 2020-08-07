namespace vega.Controllers.Resources
{
    public class VehicleQueryResource
    {
        public int? MakeId {get; set;}

        public int? ModelId {get; set;}

        public string SortBy {get; set;}

        public bool IsSortAcending {get; set;}

        public int Page {get; set;}
        public int PageSize {get; set;}
    }
}
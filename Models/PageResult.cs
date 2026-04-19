using System.Collections.Generic;

namespace EcoAir.EnergyApi.Models
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages =>
            PageSize == 0 ? 0 : (int)System.Math.Ceiling((double)TotalItems / PageSize);
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}

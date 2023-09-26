using System;
using System.Collections.Generic;

namespace TimeLogger.Domain.Entities
{
    public class PagedResults<T>
    {
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages => TotalItems > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;
    }
}
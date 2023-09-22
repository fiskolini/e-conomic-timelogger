using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Common.Commands
{
    public class OrderByCommand
    {
        public string SortField { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
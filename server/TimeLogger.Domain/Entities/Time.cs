using System.ComponentModel.DataAnnotations.Schema;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Entities
{
    public sealed class Time : BaseEntity
    {
        public int Minutes { get; set; }

        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
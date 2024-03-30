using Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Subcription")]
    public class Subcription
    {
        public int Id { get; set; }
        public virtual SubcriptionPack? SubcriptionPack { get; set; }
        public virtual DateTime? SubcriptionDate { get; set; }
        public int SubcriptionDuration { get; set; }
        public virtual AccountTypes? SubcriptionType { get; set; }
    }
}

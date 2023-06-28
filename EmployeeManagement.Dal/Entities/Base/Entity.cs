using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dal.Entities.Base
{
    public class Entity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public DateTimeOffset LastModAt { get; set; }
        public long LastModById { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public long CreatedById { get; set; }

    }
}

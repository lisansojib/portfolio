using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public EntityState EntityState { get; set; }

        public BaseEntity()
        {
            EntityState = EntityState.Added;
        }
    }
}

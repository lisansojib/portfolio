using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        EntityState EntityState { get; set; }
    }
}

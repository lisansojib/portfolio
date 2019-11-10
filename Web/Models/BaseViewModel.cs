using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
    public abstract class BaseViewModel
    {
        protected BaseViewModel()
        {
            EntityState = EntityState.Added;
        }

        public int Id { get; set; }

        public EntityState EntityState { get; set; }

        public bool IsModified => Id > 0 && EntityState == EntityState.Modified;
    }
}

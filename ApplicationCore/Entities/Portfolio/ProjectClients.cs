namespace ApplicationCore.Entities.Portfolio
{
    public class ProjectClients : BaseEntity
    {
        public int MasterId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Organization { get; set; }
        public string Description { get; set; }

        public virtual Projects Master { get; set; }
    }
}

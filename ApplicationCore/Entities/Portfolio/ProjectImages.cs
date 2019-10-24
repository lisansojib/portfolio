namespace ApplicationCore.Entities.Portfolio
{
    public class ProjectImages : BaseEntity
    {
        public int MasterId { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Projects Master { get; set; }
    }
}

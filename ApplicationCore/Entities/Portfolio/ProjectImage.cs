namespace ApplicationCore.Entities.Portfolio
{
    public class ProjectImage : BaseEntity
    {
        public int MasterId { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Project Master { get; set; }
    }
}

namespace KsefCore.Model
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
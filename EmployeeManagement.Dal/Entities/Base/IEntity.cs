namespace EmployeeManagement.Dal.Entities.Base
{
    public interface IEntity
    {
        long Id { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        public DateTimeOffset LastModAt { get; set; }
        public long LastModById { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public long CreatedById { get; set; }
    }

}

using SQLite;

namespace Challenge2.Data.Entities
{
    [Table("Container")]
    public class Container : BaseEntity
    {
        public Container() : base() { }

        public Container(Container copy) : base(copy)
        {
            Id = copy.Id;
            TransportId = copy.TransportId;
            ContainerName = copy.ContainerName;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int? TransportId { get; set; }

        [Indexed]
        public string ContainerName { get; set; }
    }
}

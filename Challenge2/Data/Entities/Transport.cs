using Challenge2.Data.Controllers;
using SQLite;

namespace Challenge2.Data.Entities
{
    [Table("Transport")]
    public class Transport : BaseEntity
    {
        public const int DEFAULT_CAPACITY = 4;

        public Transport() : base()
        {
            Type = TransportType.Ship;
            Capacity = DEFAULT_CAPACITY;
        }

        public Transport(Transport copy) : base(copy)
        {
            Id = copy.Id;
            TransportName = copy.TransportName;
            Type = copy.Type;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Unique = true)]
        public string TransportName { get; set; }

        public int? Capacity { get; set; }

        [Indexed(Name = "TransportTransportType", Order = 1)]
        public TransportType Type { get; set; }

        [Ignore]
        public int Load
        {
            get
            {
                var cc = new ContainerController();
                return cc.List(this.Id).Count;
            }
        }

        [Ignore]
        public int Left => Capacity.HasValue ? Capacity.Value - Load : 0;
    }

    public enum TransportType
    {
        Ship = 0,
        Truck = 1
    }
}

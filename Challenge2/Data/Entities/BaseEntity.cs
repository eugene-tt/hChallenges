using SQLite;
using System;

namespace Challenge2.Data.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreated = DateTime.UtcNow;
            SyncIdentifier = Guid.NewGuid().ToString();
        }

        public BaseEntity(BaseEntity copy)
        {
            DateCreated = copy.DateCreated;
            DateUpdated = copy.DateUpdated;
            DateLastSynced = copy.DateLastSynced;
            SyncIdentifier = copy.SyncIdentifier;
        }

        [MaxLength(100)]
        public string SyncIdentifier { get; set; }

        [Indexed]
        public DateTime? DateLastSynced { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}

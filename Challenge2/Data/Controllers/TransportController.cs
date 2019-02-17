using Challenge2.Data.Entities;
using Challenge2.Data.Exceptions;
using System;
using System.Collections.Generic;

namespace Challenge2.Data.Controllers
{
    public class TransportController : BaseController
    {
        public int GetCount()
        {
            lock (context.SyncRoot)
            {
                return db.Table<Transport>().Count();
            }
        }

        public List<Transport> List(TransportType? type)
        {
            try
            {
                lock (context.SyncRoot)
                {
                    if (type.HasValue)
                    {
                        return db
                            .Table<Transport>()
                            .Where(t => t.Type == type.Value)
                            .ToList();
                    }
                    else
                    {
                        return db
                            .Table<Transport>()
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to list transports. Error: {ex.Message}", ex);
            }
        }

        public void AddTransport(TransportType type, int capacity, string title)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException(nameof(capacity));
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException(nameof(title));
            }

            try
            {
                lock (context.SyncRoot)
                {
                    var transport = new Transport();
                    transport.DateCreated = DateTime.UtcNow;
                    transport.DateUpdated = transport.DateCreated;
                    transport.Type = type;
                    transport.Capacity = capacity;
                    transport.TransportName = title;

                    db.Insert(transport);
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to add a transport. Error: {ex.Message}", ex);
            }
        }

        public void AddShip(int capacity, string title)
        {
            AddTransport(TransportType.Ship, capacity, title);
        }

        public void AddTruck(int capacity, string title)
        {
            AddTransport(TransportType.Truck, capacity, title);
        }

        public Transport GetById(int id)
        {
            try
            {
                lock (context.SyncRoot)
                {
                    var res = db
                        .Table<Transport>()
                        .Where(t => t.Id == id)
                        .FirstOrDefault();

                    if(res == null)
                    {
                        throw new Exception("Not found");
                    }

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Transport not found. Error: {ex.Message}", ex);
            }
        }

        public void Rename(int id, string title)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException(nameof(title));
            }

            try
            {
                lock (context.SyncRoot)
                {
                    var transport = GetById(id);
                    transport.TransportName = title;

                    db.Update(transport);
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to rename the transport. Error: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            try
            {
                lock (context.SyncRoot)
                {
                    var transport = GetById(id);
                    if(transport.Load > 0)
                    {
                        throw new HprException("The transport is not empty. Unload its cargo first.");
                    }

                    db.Delete(transport);
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to delete the transport. Error: {ex.Message}", ex);
            }
        }
    }
}

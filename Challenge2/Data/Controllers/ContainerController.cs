using Challenge2.Data.Entities;
using Challenge2.Data.Exceptions;
using System;
using System.Collections.Generic;

namespace Challenge2.Data.Controllers
{
    public class ContainerController : BaseController
    {
        public int GetCount()
        {
            lock (context.SyncRoot)
            {
                return db.Table<Container>().Count();
            }
        }

        public List<Container> List(int? transportId)
        {
            try
            {
                lock (context.SyncRoot)
                {
                    if (transportId.HasValue)
                    {
                        return db
                            .Table<Container>()
                            .Where(t => t.TransportId == transportId.Value)
                            .ToList();
                    }
                    else
                    {
                        return db
                            .Table<Container>()
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to list containers. Error: {ex.Message}", ex);
            }
        }

        public void AddContainer(string title)
        {            
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException(nameof(title));
            }

            try
            {
                lock (context.SyncRoot)
                {
                    var transport = new Container();
                    transport.DateCreated = DateTime.UtcNow;
                    transport.DateUpdated = transport.DateCreated;
                    transport.ContainerName = title;

                    db.Insert(transport);
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to add a container. Error: {ex.Message}", ex);
            }
        }

        public Container GetById(int id)
        {
            try
            {
                lock (context.SyncRoot)
                {
                    var res = db
                        .Table<Container>()
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
                throw new HprException($"Container not found. Error: {ex.Message}", ex);
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
                    var container = GetById(id);
                    container.ContainerName = title;

                    db.Update(container);
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to rename the container. Error: {ex.Message}", ex);
            }
        }

        public void Move(int id, int? transportId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            try
            {
                lock (context.SyncRoot)
                {
                    var container = GetById(id);
                    container.TransportId = transportId;

                    // Check the capacities
                    if (transportId.HasValue)
                    {
                        var transport = new TransportController().GetById(transportId.Value);
                        if(transport.Left < 1)
                        {
                            throw new HprException($"Transport #{transportId} is already full, please choose another one");
                        }
                    }

                    db.Update(container);
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to relocate the container. Error: {ex.Message}", ex);
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
                    var container = GetById(id);
                    db.Delete(container);
                }
            }
            catch (Exception ex)
            {
                throw new HprException($"Failed to delete the container. Error: {ex.Message}", ex);
            }
        }
    }
}
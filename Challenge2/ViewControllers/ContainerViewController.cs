using Challenge2.Data.Controllers;
using Challenge2.Data.Entities;
using Challenge2.Data.Exceptions;
using Challenge2.Utils;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2.ViewControllers
{
    public class ContainerViewController : BaseViewController
    {
        private static void PrintContainers(IEnumerable<Container> containers)
        {
            var table = new ConsoleTable("ID", "Name", "Transport ID");

            foreach (var item in containers)
            {
                table.AddRow(
                    item.Id,
                    item.ContainerName,
                    item.TransportId.HasValue ? item.TransportId.Value.ToString() : "Not loaded");
            }

            table.Write(Format.Alternative);
        }

        private static void DisplayContainerList(int? transportId)
        {
            BeginOutput();
            PrintHeader("Container List");

            var tc = new ContainerController();
            var containers = tc.List(transportId);
            PrintContainers(containers);
        }

        public static void ShowContainerList()
        {
            DisplayContainerList(null);
        }


        public static void ShowContainerListByTransport()
        {
            bool canceled = false;
            int transportId = TransportViewController.SelectTransportId(out canceled);
            if (canceled)
            {
                return;
            }
            DisplayContainerList(transportId);
        }

        public static void AddContainer()
        {
            BeginOutput();

            bool canceled = false;
            string containerName = ConsoleUtils.ReadString("Container name", () => { canceled = true; });
            if (canceled)
            {
                return;
            }

            try
            {
                new ContainerController().AddContainer(containerName);
                Console.WriteLine($"Added new container \"{containerName}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int SelectContainerId(out bool canceled)
        {
            canceled = false;
            ShowContainerList();

            bool tempCanceled = false;
            int containerId = ConsoleUtils.ReadPositiveInt("Select a container ID", () => { tempCanceled = true; });
            if (tempCanceled)
            {
                canceled = tempCanceled;
                return int.MinValue;
            }

            return containerId;
        }

        public static void Rename()
        {
            bool canceled = false;
            int containerId = SelectContainerId(out canceled);
            if (canceled)
            {
                return;
            }

            string containerName = ConsoleUtils.ReadString("New name", () => { canceled = true; });
            if (canceled)
            {
                return;
            }

            try
            {
                new ContainerController().Rename(containerId, containerName);
                Console.WriteLine($"Successfully renamed to \"{containerName}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Delete()
        {
            bool canceled = false;
            int containerId = SelectContainerId(out canceled);
            if (canceled)
            {
                return;
            }
            try
            {
                new ContainerController().Delete(containerId);
                Console.WriteLine($"Successfully deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Unload()
        {
            bool canceled = false;
            int containerId = SelectContainerId(out canceled);
            if (canceled)
            {
                return;
            }
            
            try
            {
                var cc = new ContainerController();
                var container = cc.GetById(containerId);
                if(!container.TransportId.HasValue)
                {
                    throw new HprException("Container is already unloaded");
                }
                cc.Move(containerId, null);

                Console.WriteLine($"Successfully unloaded");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Load()
        {
            bool canceled = false;
            int containerId = SelectContainerId(out canceled);
            if (canceled)
            {
                return;
            }

            int transportId = TransportViewController.SelectTransportId(out canceled);
            if (canceled)
            {
                return;
            }

            try
            {
                var cc = new ContainerController();
                var container = cc.GetById(containerId);
                if (container.TransportId.HasValue)
                {
                    throw new HprException("Container must be unloaded first");
                }
                cc.Move(containerId, transportId);

                Console.WriteLine($"Successfully loaded");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

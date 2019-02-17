using Challenge2.Data.Controllers;
using Challenge2.Data.Entities;
using Challenge2.Utils;
using ConsoleTables;
using System;
using System.Collections.Generic;

namespace Challenge2.ViewControllers
{
    public class TransportViewController : BaseViewController
    {
        private static void PrintTransports(IEnumerable<Transport> transports)
        {
            var table = new ConsoleTable("ID", "Type", "Name", "Capacity", "Load");

            foreach (var item in transports)
            {
                table.AddRow(
                    item.Id,
                    Enum.GetName(typeof(TransportType), item.Type),
                    item.TransportName,
                    item.Capacity,
                    item.Load);
            }

            table.Write(Format.Alternative);
        }

        public static void ShowTransportMediaList()
        {
            BeginOutput();
            PrintHeader("Transport List");

            var tc = new TransportController();
            var transports = tc.List(null);
            PrintTransports(transports);
        }

        public static void AddShip()
        {
            BeginOutput();

            bool canceled = false;
            string shipName = ConsoleUtils.ReadString("Ship name", () => { canceled = true; });
            if (canceled)
            {
                return;
            }

            int capacity = ConsoleUtils.ReadPositiveInt("Capacity", () => { canceled = true; });
            if (canceled)
            {
                return;
            }

            try
            {
                new TransportController().AddShip(capacity, shipName);
                Console.WriteLine($"Added new ship \"{shipName}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void AddTruck()
        {
            BeginOutput();

            bool canceled = false;
            string truckName = ConsoleUtils.ReadString("Truck name", () => { canceled = true; });
            if (canceled)
            {
                return;
            }

            int capacity = ConsoleUtils.ReadPositiveInt("Capacity", () => { canceled = true; });
            if (canceled)
            {
                return;
            }

            try
            {
                new TransportController().AddTruck(capacity, truckName);
                Console.WriteLine($"Added new truck \"{truckName}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int SelectTransportId(out bool canceled)
        {
            canceled = false;
            ShowTransportMediaList();

            bool tempCanceled = false;
            int transportId = ConsoleUtils.ReadPositiveInt("Select a transport ID", () => { tempCanceled = true; });
            if (tempCanceled)
            {
                canceled = tempCanceled;
                return int.MinValue;
            }

            return transportId;
        }

        public static void Rename()
        {
            bool canceled = false;
            int transportId = SelectTransportId(out canceled);
            if (canceled)
            {
                return;
            }

            string transportName = ConsoleUtils.ReadString("New name", () => { canceled = true; });
            if (canceled)
            {
                return;
            }

            try
            {
                new TransportController().Rename(transportId, transportName);
                Console.WriteLine($"Successfully renamed to \"{transportName}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Delete()
        {
            bool canceled = false;
            int transportId = SelectTransportId(out canceled);
            if (canceled)
            {
                return;
            }

            try
            {
                new TransportController().Delete(transportId);
                Console.WriteLine($"Successfully deleted the transport");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

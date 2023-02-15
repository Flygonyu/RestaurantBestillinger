using System.Text.Json;
using Newtonsoft.Json;

namespace RestaurantBestillinger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tablesText = File.ReadAllText(args[0]);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var tables = System.Text.Json.JsonSerializer.Deserialize<Table[]>(tablesText, options);

            var reservationsText = File.ReadAllText(args[1]);
            var reservations = System.Text.Json.JsonSerializer.Deserialize<Reservation[]>(reservationsText, options);
            
            if (args.Length == 4)
            {
                GetReservations(args, reservations);
            }

            if (args.Length > 4)
            {
                var tablesByCapacity = tables.OrderBy(t => t.Capacity);
                foreach (var table in tablesByCapacity)
                {
                    if (Convert.ToInt32(args[4]) <= table.Capacity && table.IsBooked == false)
                    {
                        BookTable(args, table);
                        return;
                    }
                }
                Console.WriteLine("Ingen bord tilgjengelig");
                return;               
            }
        }

        static void GetReservations(string[] args, Reservation[] reservations)
        {
            var hours = args[2];
            var minutes = args[3];
            int startTime = Convert.ToInt32(hours + minutes);
            int endTime = Convert.ToInt32(hours + minutes) + 200;

            Console.WriteLine($"Reservasjoner for {hours}:{minutes}:\n");
            foreach (var reservation in reservations)
            {
                if (reservation.StartTime >= startTime && reservation.StartTime <= endTime)
                {
                    Console.WriteLine($"Bord: {reservation.Table}\nNavn: {reservation.CustomerName}\nTlf: {reservation.CustomerPhone}\nAntall: {reservation.Count}\n" +
                        $"Reservert fra {reservation.StartTime} til {reservation.StartTime + 200}");
                }
                Console.WriteLine();
            }
        }

        static void CreateReservationsJson()
        {

        }

        static void BookTable(string[] args, Table table)
        {
            var customerName = "Ukjent";
            if (args.Length > 5) customerName = args[5];
            var customerPhone = "Ukjent";
            if (args.Length > 6) customerPhone = args[6];

            var reservation = new Reservation()
            {
                Count = Convert.ToInt32(args[4]),
                CustomerName = customerName,
                CustomerPhone = customerPhone,
                StartTime = Convert.ToInt32(args[2] + args[3]),
                Table = table.Name
            };
            table.IsBooked = true;
            Console.WriteLine($"{customerName} booket bord {table.Name} for {args[4]} personer");
        }
    }
}
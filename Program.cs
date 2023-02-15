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
            
            Restaurant restaurant = new(reservations, tables);
            
            if (args.Length == 4)
            {
                restaurant.GetReservations(args);
            }

            if (args.Length > 4)
            {
                var tablesByCapacity = tables.OrderBy(t => t.Capacity);
                foreach (var table in tablesByCapacity)
                {
                    if (Convert.ToInt32(args[4]) <= table.Capacity && table.IsBooked == false)
                    {
                        restaurant.BookTable(args, table);
                        return;
                    }
                }
                Console.WriteLine("Ingen bord tilgjengelig");
                return;               
            }
        }

        static void CreateReservationsJson()
        {

        }
    }
}
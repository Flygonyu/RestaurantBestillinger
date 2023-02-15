using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBestillinger
{
    internal class Restaurant
    {
        public Reservation[] Reservations { get; private set; }
        public Table[] Tables { get; private set; }

        public Restaurant(Reservation[] reservations, Table[] tables)
        {
            Reservations = reservations;
            Tables = tables;
        }

        public void GetReservations(string[] args)
        {
            var hours = args[2];
            var minutes = args[3];
            int startTime = Convert.ToInt32(hours + minutes);
            int endTime = Convert.ToInt32(hours + minutes) + 200;

            Console.WriteLine($"Reservasjoner for {hours}:{minutes}:\n");
            foreach (var reservation in Reservations)
            {
                if (reservation.StartTime >= startTime && reservation.StartTime <= endTime)
                {
                    Console.WriteLine($"Bord: {reservation.Table}\nNavn: {reservation.CustomerName}\nTlf: {reservation.CustomerPhone}\nAntall: {reservation.Count}\n" +
                        $"Reservert fra {reservation.StartTime} til {reservation.StartTime + 200}");
                }
                Console.WriteLine();
            }
        }

        public void BookTable(string[] args, Table table)
        {
            var customerName = "Ukjent";
            if (args.Length > 5) customerName = args[5];
            var customerPhone = "Ukjent";
            if (args.Length > 6) customerPhone = args[6];

            new Reservation()
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

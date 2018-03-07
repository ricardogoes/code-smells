using System;
using System.Linq;

namespace CodeSmells.Couplers
{
    public class Customer
    {
        public void AddRental(Movie movie)
        {
            var rental = new Rental(movie);
        }
    }

    public class Rental
    {
        private Movie _movie;
        public DateTime DueDate { get; set; }
        public decimal Price { get; set; }

        public Rental(Movie movie)
        {
            this._movie = movie;
        }

        public decimal GetPrice()
        {
            if (_movie.IsNewRelease)
            {
                if (_movie.IsChildrens)
                {
                    return 4;
                }
                return 5;
            }
            if (_movie.IsChildrens)
            {
                return 2;
            }
            return 3;
        }
    }

    public class Movie
    {
        public bool IsNewRelease { get; set; }
        public bool IsChildrens { get; set; }
        public string Title { get; set; }
    }
}

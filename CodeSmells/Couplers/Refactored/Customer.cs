using System;
using System.Linq;

namespace CodeSmells.Couplers.Refactored
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
    }

public class Movie
{
    public bool IsNewRelease { get; set; }
    public string Title { get; set; }

    public virtual decimal GetPrice()
    {
        if (IsNewRelease)
        {
            return 5;
        }
        return 3;
    }
}

public class ChildrensMovie : Movie
{
    public override decimal GetPrice()
    {
        if (IsNewRelease)
        {
                return 4;
        }
        return 2;
    }
}
}

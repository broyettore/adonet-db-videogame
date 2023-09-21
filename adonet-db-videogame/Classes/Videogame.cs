using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame.Classes
{
    public class Videogame
    {
        public long Id { get; private set; }
        public string Name {  get; private set; }
        public string Overview { get; private set; }
        public DateTime Release_date { get; private set; }
        public long Software_house_id { get; private set; }

        public Videogame(long id, string name, string overview, DateTime release_date)
        {
            if (id < 1)
                throw new ArgumentException("Id has to be bigger than 0");
            Id = id;
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException("Name field can not be empty");
            Name = name;
            if (string.IsNullOrEmpty(overview))
                throw new ArgumentException("Overview field can not be empty");
            Overview = overview;
            Release_date = release_date;
            Software_house_id = GenRandId();
        }   

        public override string ToString()
        {
            string formatDate = this.Release_date.ToString("dd-MM-yyyy");
            return $"Videogame - {this.Id} - {this.Software_house_id}: {this.Name} released {formatDate} \n Overview: {this.Overview}";
        }

        public int GenRandId()
        {
            int rand = Random.Shared.Next(1, 7);

            return rand;
        }
    }
}

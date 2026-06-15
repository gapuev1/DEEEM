using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearchApp.Models
{
    public class MovieDetails : Movie
    {
        public string Overview { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Cast> Credits { get; set; }
        public string TrailerUrl { get; set; }
    }
}

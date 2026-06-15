using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearchApp.Models
{
    public class MovieDetailResponse : MovieDetails
    {
        public CreditsResponse Credits { get; set; }
        public VideosResponse Videos { get; set; }
    }
}

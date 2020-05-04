using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// the movies to display on index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// filtered MPAARatings
        /// </summary>
        [BindProperty(SupportsGet = true)]

        public string[] MPAARatings { get; set; }

        /// <summary>
        /// filtered genre
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genre { get; set; }

        /// <summary>
        /// the minimum IMDB rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// the maximum IMDB rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// the minimum RottenTomatoesMin rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RottenTomatoesMin { get; set; }

        /// <summary>
        /// the maximum RottenTomatoesMax rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RottenTomatoesMax { get; set; }

        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet()
        {
            /*
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genre);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenTomatoesMin, RottenTomatoesMax);
            */
            Movies = MovieDatabase.All;
            if (SearchTerms != null)
            {
                //Movies = MovieDatabase.All.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
                Movies = from movie in Movies
                         where movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase)
                         select movie;
            }
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = from movie in Movies
                         where movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating)
                         select movie;
            }
            if (Genre != null && Genre.Count() != 0)
            {
                Movies = from movie in Movies
                         where movie.MajorGenre != null && Genre.Contains(movie.MajorGenre)
                         select movie;
            }
            if (IMDBMax != null || IMDBMin != null)
            {
                if (IMDBMin == null)
                {
                    Movies = from movie in Movies
                             where movie.IMDBRating <= IMDBMax
                             select movie;
                }

                else if (IMDBMax == null)
                {

                    Movies = from movie in Movies
                             where movie.IMDBRating >= IMDBMin
                             select movie;
                }

                else
                {
                    Movies = from movie in Movies
                             where movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax
                             select movie;
                }
            }



            if (RottenTomatoesMax != null || RottenTomatoesMin != null)
            {
                if (RottenTomatoesMin == null)
                {
                    Movies = from movie in Movies
                             where movie.RottenTomatoesRating <= RottenTomatoesMax
                             select movie;
                }
                else if (RottenTomatoesMax == null)
                {
                    Movies = from movie in Movies
                             where movie.RottenTomatoesRating <= RottenTomatoesMin
                             select movie;
                }
                else
                {
                    Movies = from movie in Movies
                             where movie.RottenTomatoesRating >= RottenTomatoesMin && movie.RottenTomatoesRating <= RottenTomatoesMax
                             select movie;
                }
            }
        }


    }
}


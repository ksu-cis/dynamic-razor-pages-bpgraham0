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
        [BindProperty]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// filtered MPAARatings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// filtered genre
        /// </summary>
        [BindProperty]
        public string[] Genre { get; set; }

        /// <summary>
        /// the minimum IMDB rating
        /// </summary>
        public float IMDBMin { get; set; }

        /// <summary>
        /// the maximum IMDB rating
        /// </summary>
        public float IMDBMax { get; set; }

        /// <summary>
        /// the minimum RottenTomatoesMin rating
        /// </summary>
        public float RottenTomatoesMin { get; set; }

        /// <summary>
        /// the maximum RottenTomatoesMax rating
        /// </summary>
        public float RottenTomatoesMax { get; set; }

        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, double? RottenTomatoesMin, double? RottenTomatoesMax)
        {
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genre = Request.Query["Genre"];

            //null check
            if (IMDBMin == null)
            {
                this.IMDBMin = 0; //if null, sets as default value
            }
            else
            this.IMDBMin = (float)IMDBMin;
            if(IMDBMax == null)
            {
                this.IMDBMax = 10; //if null, sets as default value
            }
            else
            this.IMDBMax = (float)IMDBMax;
            //null check
            if (RottenTomatoesMin == null)
            {
                this.RottenTomatoesMin = 0; //if null, sets as default value
            }
            else
                this.RottenTomatoesMin = (float)RottenTomatoesMin;
            if (RottenTomatoesMax == null)
            {
                this.RottenTomatoesMax = 100; //if null, sets as default value
            }
            else
                this.RottenTomatoesMax = (float)RottenTomatoesMax;

            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genre);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenTomatoesMin, RottenTomatoesMax);



        }
    }
}

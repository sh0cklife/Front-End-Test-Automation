using MovieCatalogExamPrep_3.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogExamPrep_3.Tests
{
    public class MovieCatalogTests : BaseTest
    {
        private string lastMovieTitle;
        private string lastMovieDescription;

        [Test, Order(1)]
        public void AddMovieWithoutTitleTest()
        {
            lastMovieDescription = GenerateRandomDescription();

            addMoviesPage.OpenPage();
            addMoviesPage.AddMovie("", lastMovieDescription);
            addMoviesPage.AssertEmptyTitleMessage();
        }

        [Test, Order(2)]
        public void AddMovieWithoutDescriptionTest()
        {
            lastMovieTitle = GenerateRandomTitle();

            addMoviesPage.OpenPage();
            addMoviesPage.AddMovie(lastMovieTitle, " ");
            addMoviesPage.AssertEmptyDescriptionMessage();
        }

        [Test, Order(3)]
        public void AddMovieWithRandomTitleTest()
        {
            lastMovieTitle = GenerateRandomTitle();
            lastMovieDescription = GenerateRandomDescription();

            addMoviesPage.OpenPage();
            addMoviesPage.AddMovie(lastMovieTitle, lastMovieDescription);

            //Assert from AllMoviesPage.cs
            allMoviesPage.NavigateToLastPage();
            Assert.That(allMoviesPage.LastMovieTitle.Text.Trim(), Is.EqualTo(lastMovieTitle), "Error message: Title is not as expected!");
        }

        [Test, Order(4)]
        public void EditLastAddedMovieTest()
        {
            lastMovieTitle = GenerateRandomTitle();
            lastMovieDescription = GenerateRandomDescription();

            allMoviesPage.OpenPage();
            allMoviesPage.NavigateToLastPage();
            allMoviesPage.LastMovieEditButton.Click();

            editMoviePage.EditMovie($"{lastMovieTitle} EDITED", $"{lastMovieDescription} EDITED");
            editMoviePage.AssertEditMessage();
        }

        [Test, Order(5)]
        public void MarkLastAddedMovieAsWatchedTest()
        {
            lastMovieTitle = $"{lastMovieTitle} EDITED";
            allMoviesPage.OpenPage();
            allMoviesPage.NavigateToLastPage();
            allMoviesPage.LastMovieMarkAsWatchedButton.Click();

            watchedMoviesPage.OpenPage();
            watchedMoviesPage.NavigateToLastPage();

            Assert.That(watchedMoviesPage.LastMovieTitle.Text.Trim(), Is.EqualTo(lastMovieTitle), "The movie was not added to Watched List.");
        }

        [Test, Order(6)]
        public void DeleteLastAddedMovieTest()
        {
            allMoviesPage.OpenPage();
            allMoviesPage.NavigateToLastPage();
            allMoviesPage.LastMovieDeleteButton.Click();

            deletePage.YesButton.Click();
            deletePage.AssertMessage();
        }
    }
}

using IdeaCenterExamPrep.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCenterExamPrep.Tests
{
    public class IdeaCenterTests : BaseTest
    {
        public string lastCreatedIdeaTitle;
        public string lastCreatedIdeaDescription;

        [Test, Order(1)]
        public void CreateIdeaWithInvalidDataTest()
        {
            createIdeaPage.OpenPage();
            createIdeaPage.CreateIdea("", "", "");
            createIdeaPage.AssertErrorMessages();
        }

        [Test, Order(2)]
        public void CreateIdeaWithValidDataTest()
        {
            lastCreatedIdeaTitle = "Idea " + GenerateRandomString(5);
            lastCreatedIdeaDescription = "Description " + GenerateRandomString(5);

            createIdeaPage.OpenPage();
            createIdeaPage.CreateIdea(lastCreatedIdeaTitle, "", lastCreatedIdeaDescription);

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "URL is not correct");
            Assert.That(myIdeasPage.DescriptionLastIdea.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Descriptions does NOT match");
        }

        [Test, Order(3)]
        public void ViewLastCreatedIdeaTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.ViewButtonLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(lastCreatedIdeaTitle), "Titles do NOT match");
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Descriptions do NOT match");
        }

        [Test, Order(4)]
        public void EditLastCreatedIdeaTitleTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.EditButtonLastIdea.Click();

            string editedTitle = "Edited Title: " + lastCreatedIdeaTitle;

            ideasEditPage.TitleInput.Clear();
            ideasEditPage.TitleInput.SendKeys(editedTitle);
            ideasEditPage.EditButton.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Not correctly redirected");

            myIdeasPage.ViewButtonLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(editedTitle));
        }

        [Test, Order(5)]
        public void EditLastCreatedIdeaDescriptionTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.EditButtonLastIdea.Click();

            string editedDescription = "Edited Description: " + lastCreatedIdeaDescription;

            ideasEditPage.DescriptionInput.Clear();
            ideasEditPage.DescriptionInput.SendKeys(editedDescription);
            ideasEditPage.EditButton.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Not correctly redirected");

            myIdeasPage.ViewButtonLastIdea.Click();

            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(editedDescription), "Descriptions did not match.");
        }

        [Test, Order(6)]
        public void DeleteLastIdeaTest()
        {
            myIdeasPage.OpenPage();

            Assert.IsTrue(myIdeasPage.IdeasCards.Count > 0, "No idea cards were found on the page.");

            myIdeasPage.DeleteButtonLastIdea.Click();

            bool isIdeaDeleted = myIdeasPage.IdeasCards.All(card => !card.Text.Contains(lastCreatedIdeaDescription));

            Assert.IsTrue(isIdeaDeleted, "The idea was not deleted successfully or is still visible in the list.");

            //not optimal assertion
            Assert.IsTrue(myIdeasPage.IdeasCards.Count == 0, "Card was not deleted");


        }
    }
}

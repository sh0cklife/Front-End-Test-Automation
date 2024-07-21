﻿using StudentRegistryPOM.Pages;

namespace StudentRegistryPOM.Tests
{
    public class ViewStudentsPage_Tests : BaseTest
    {
        [Test]
        public void Test_ViewStudentsPage_Content()
        {
            ViewStudentsPage viewStudentsPage = new ViewStudentsPage(driver);
            viewStudentsPage.OpenPage();

            Assert.Multiple(() =>
            {
                Assert.That(viewStudentsPage.GetPageTitle(), Is.EqualTo("Students"));
                Assert.That(viewStudentsPage.GetPageHeading(), Is.EqualTo("Registered Students"));
            });

            var students = viewStudentsPage.GetRegisteredStudents();
            foreach (var student in students)
            {
                Assert.That(student.Contains("("), Is.True);
                Assert.That(student.LastIndexOf(")") == student.Length - 1, Is.True);
            }
        }
    }
}

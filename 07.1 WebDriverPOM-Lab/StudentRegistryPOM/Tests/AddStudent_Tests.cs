using StudentRegistryPOM.Pages;

namespace StudentRegistryPOM.Tests
{
    public class AddStudent_Tests : BaseTest
    {
        [Test]
        public void Test_AddStudentPage_Content()
        {
            AddStudentPage addStudentPage = new AddStudentPage(driver);
            addStudentPage.OpenPage();

            Assert.Multiple(() =>
            {
                Assert.That(addStudentPage.GetPageTitle(), Is.EqualTo("Add Student"));
                Assert.That(addStudentPage.GetPageHeading(), Is.EqualTo("Register New Student"));
            });

            Assert.That(addStudentPage.EmailField.Text, Is.EqualTo(""));
            Assert.That(addStudentPage.NameField.Text, Is.EqualTo(""));
            Assert.That(addStudentPage.AddButton.Text, Is.EqualTo("Add"));
        }

        [Test]
        public void Test_TestAddStudentPage_AddValidStudent()
        {
            AddStudentPage addStudentPage = new AddStudentPage(driver);
            addStudentPage.OpenPage();

            string name = RandomName();
            string email = RandomEmail(name);

            addStudentPage.AddStudent(name, email);

            ViewStudentsPage viewStudents = new ViewStudentsPage(driver);
            Assert.That(viewStudents.IsPageOpen(), Is.True);

            var students = viewStudents.GetRegisteredStudents();

            string newStudentFullString = name + " (" + email + ")";
            Assert.True(students.Contains(newStudentFullString));
            
        }

        [Test]
        public void Test_TestAddStudentPage_AddValidStudent_InvalidName()
        {
            AddStudentPage addStudentPage = new AddStudentPage(driver);
            addStudentPage.OpenPage();

            addStudentPage.AddStudent("", "demo_user@test.com");

            Assert.That(addStudentPage.IsPageOpen(), Is.True);
            Assert.That(addStudentPage.GetErrorMessage(), Is.EqualTo("Cannot add student. Name and email fields are required!"));

        }

        private string RandomName()
        {
            var random = new Random();
            string[] names = { "Ivan", "Dragan", "Petkan" };
            return names[random.Next(names.Length)] + random.Next(1, 999).ToString();
        }

        private string RandomEmail(string name)
        {
            var random = new Random();

            return name.ToLower() + random.Next(1, 999).ToString() + "@test.com";
        }
    }
}

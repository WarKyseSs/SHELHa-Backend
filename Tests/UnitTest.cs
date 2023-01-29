using Domain.Utils;
using Infrastructure.Utils;
using NUnit.Framework;

namespace Tests;

public class Tests
{
    [TestFixture]
    public class UserValidatorTests
    {
        [Test]
        public void TestValidatePassword()
        {
            Assert.IsTrue(UserValidator.ValidatePassword("P@ssw0rd"));
            Assert.IsFalse(UserValidator.ValidatePassword("password"));
            Assert.IsFalse(UserValidator.ValidatePassword("PASSWORD"));
            Assert.IsFalse(UserValidator.ValidatePassword("p@ssw0rd"));
            Assert.IsFalse(UserValidator.ValidatePassword("p@ssw0rd1"));
        }

        [Test]
        public void TestValidateMailAddress()
        {
            Assert.IsTrue(UserValidator.ValidateMailAddress("user@example.com"));
            Assert.IsFalse(UserValidator.ValidateMailAddress("user@example"));
            Assert.IsFalse(UserValidator.ValidateMailAddress("user@"));
            Assert.IsFalse(UserValidator.ValidateMailAddress("@example.com"));
        }

        [Test]
        public void TestValidateHelhaMailAddress()
        {
            Assert.IsTrue(UserValidator.ValidateHelhaMailAddress("la123456@student.helha.be"));
            Assert.IsTrue(UserValidator.ValidateHelhaMailAddress("user@helha.be"));
            Assert.IsFalse(UserValidator.ValidateHelhaMailAddress("user@example.com"));
            Assert.IsFalse(UserValidator.ValidateHelhaMailAddress("la12345@student.helha.be"));
            Assert.IsFalse(UserValidator.ValidateHelhaMailAddress("user@helha.be1"));
        }
    }
    
    [TestFixture]
    public class SlugUrlProviderTests
    {
        [Test]
        public void TestToUrlSlug()
        {
            var slugUrlProvider = new SlugUrlProvider();

            string result = slugUrlProvider.ToUrlSlug("This is a test");
            Assert.AreEqual("this-is-a-test", result);

            result = slugUrlProvider.ToUrlSlug("This is a test!");
            Assert.AreEqual("this-is-a-test", result);

            result = slugUrlProvider.ToUrlSlug("This is a test 123");
            Assert.AreEqual("this-is-a-test-123", result);

            result = slugUrlProvider.ToUrlSlug("This is a test  ");
            Assert.AreEqual("this-is-a-test", result);

            result = slugUrlProvider.ToUrlSlug("This is a test ---");
            Assert.AreEqual("this-is-a-test", result);
            
            result = slugUrlProvider.ToUrlSlug("Th'is is a tést ---");
            Assert.AreEqual("this-is-a-test", result);
        }
    }
    
    [TestFixture]
    public class HashingTests
    {
        [Test]
        public void TestHashPassword()
        {
            // Arrange
            string password = "mypassword";

            // Act
            string hashedPassword = Hashing.HashPassword(password);

            // Assert
            Assert.IsTrue(hashedPassword.Length > 0);
        }

        [Test]
        public void TestValidatePassword()
        {
            // Arrange
            string password = "mypassword";
            string correctHash = Hashing.HashPassword(password);

            // Act
            bool isValid = Hashing.ValidatePassword(password, correctHash);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
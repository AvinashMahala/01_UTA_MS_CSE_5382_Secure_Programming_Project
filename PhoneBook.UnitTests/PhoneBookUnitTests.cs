using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.CustomAttributes;
using PhoneBook.Services;

namespace PhoneBook.UnitTests
{
    [TestFixture]
    public class PhoneBookUnitTests
    {
        private DictionaryPhoneBookService _primeService;

        [SetUp]
        public void SetUp()
        {
            _primeService = new DictionaryPhoneBookService();
        }

        [TestCase("Schneier, Bruce")]
        [TestCase("Schneier, Bruce Wayne")]
        [TestCase("O'Malley, John F.")]
        [TestCase("Bruce Schneier")]
        [TestCase("John O'Malley-Smith")]
        [TestCase("Cher")]
        public void NameValidationAttibuteTestWith_GivenValidInputs_True(string name)
        {
            // arrange
            var value = name;
            var attrib = new NameValidationAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.True);
        }


        [TestCase("Ron O''Henry")]
        [TestCase("Ron O'Henry-Smith-Barnes")]
        [TestCase("L33t Hacker")]
        [TestCase("<Script>alert(\"XSS\")</script>")]
        [TestCase("Brad Everett Samuel Smith")]
        [TestCase("select * from users;")]
        public void NameValidationAttibuteTestWith_GivenInValidInputs_False(string name)
        {
            // arrange
            var value = name;
            var attrib = new NameValidationAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.False);
        }



        [TestCase("John O'Malley Smith")]
        [TestCase("John Smith")]
        [TestCase("John")]
        [TestCase("John Malley Smith")]
        [TestCase("Mathias d'Arras")]
        [TestCase("Hector Sausage-Hausen")]
        [TestCase("John Smith")]
        [TestCase("John D'Largy")]
        [TestCase("John Doe-Smith")]
        [TestCase("John Doe Smith")]
        [TestCase("Hector Sausage-Hausen")]
        [TestCase("Mathias d'Arras")]
        [TestCase("Martin Luther King")]
        [TestCase("Ai Wong")]
        [TestCase("Chao Chang")]
        [TestCase("Alzbeta Bara")]
        [TestCase("STEVE SMITH")]
        [TestCase("STeve Smith")]
        [TestCase("Steve SMith")]
        [TestCase("Jo Blow")]
        [TestCase("Hyoung Kyoung Wu")]
        [TestCase("Mike O'Neal")]
        [TestCase("Steve Johnson-Smith")]
        [TestCase("O Henry Smith")]
        [TestCase("Mathais d'Arras")]
        [TestCase("Darren McCarty")]
        [TestCase("George De FunkMaster")]
        [TestCase("Ahmad el Jeffe")]
        [TestCase("Mathias d'Arras")]
        [TestCase("Hector Sausage-Hausen")]
        [TestCase("John Doe")]
        [TestCase("pedro alberto ch")]
        [TestCase("Ar. Gen")]
        [TestCase("Mathias d'Arras")]
        public void NameValidationAttibuteTestWith_MyCustomValidInputs_True(string name)
        {
            // arrange
            var value = name;
            var attrib = new NameValidationAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.True);
        }



        [TestCase("..Mathias")]
        [TestCase("76765675  sgigidgw")]
        [TestCase("O''''Malley, John")]
        [TestCase("!!!Im Hacker")]
        [TestCase("🤣 Maria")]
        [TestCase("١١١١١")]
        [TestCase("123John")]
        public void NameValidationAttibuteTestWith_MyCustomInValidInputs_False(string name)
        {
            // arrange
            var value = name;
            var attrib = new NameValidationAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.False);
        }
    }
}
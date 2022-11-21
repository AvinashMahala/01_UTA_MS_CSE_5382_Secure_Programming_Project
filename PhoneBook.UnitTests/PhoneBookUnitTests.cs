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

        [TestCase("1234 \" AND 1=0 UNION ALL SELECT \"admin\", \"81dc9bdb52d04dc20036dbd8313ed055")]
        [TestCase("\");waitfor delay '0:0:5'--")]
        [TestCase(",(select * from (select(sleep(10)))a)\r\n%2c(select%20*%20from%20(select(sleep(10)))a)\r\n';WAITFOR DELAY '0:0:30'--")]
        [TestCase("-1 UNION SELECT 1 INTO @,@")]
        [TestCase("'ij- -klmnop'")]
        [TestCase("e-'f g'-'-'-h")]
        [TestCase("'-'c -'-'-'-d")]
        [TestCase("a-'- b'-'-'-'")]
        [TestCase("'-'- -'-'-'-'")]
        [TestCase("'''' ''''''''")]
        [TestCase("---- --------")]
        [TestCase("Þór Eldon")]
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




        [TestCase("+1 670 123 4567")]
        [TestCase("1 670 123 4567")]
        [TestCase("123 4567")]
        [TestCase("12345")]
        [TestCase("(703)111-2121")]
        [TestCase("123-1234")]
        [TestCase("+1(703)111-2121")]
        [TestCase("+32 (21) 212-2324")]
        [TestCase("1(703)123-1234")]
        [TestCase("011 701 111 1234")]
        [TestCase("12345.12345")]
        [TestCase("011 1 703 111 1234")]
        [TestCase("123-4567")]
        [TestCase("(670)123-4567")]
        [TestCase("670-123-4567")]
        [TestCase("1-670-123-4567")]
        [TestCase("1(670)123-4567")]
        [TestCase("670 123 4567")]
        [TestCase("670.123.4567")]
        [TestCase("1 670 123 4567")]
        [TestCase("1.670.123.4567")]
        public void PhoneNumberValAttrTestWith_Given_ValidInputs_True(string phoneNumber)
        {
            // arrange
            var value = phoneNumber;
            var attrib = new PhoneMaskAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.True);
        }



        [TestCase("(+45) 35 35 35 35")]
        [TestCase("+45 35 35 35 35")]
        [TestCase("35 35 35 35")]
        [TestCase("35353535")]
        [TestCase("11.11.11.11")]
        [TestCase("11 11 11 11")]
        [TestCase("1111.1111")]
        [TestCase("1111 1111")]
        public void DanishNumbersValAttrTestWith_Given_ValidInputs_True(string phoneNumber)
        {
            // arrange
            var value = phoneNumber;
            var attrib = new PhoneMaskAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.True);
        }

        [TestCase("(45)35353535")]
        [TestCase("4535353535")]
        public void DanishNumbersValAttrTestWith_Given_InValidInputs_False(string phoneNumber)
        {
            // arrange
            var value = phoneNumber;
            var attrib = new PhoneMaskAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.False);
        }


        [TestCase("123")]
        [TestCase("1/703/123/1234")]
        [TestCase("Nr 102-123-1234")]
        [TestCase("<script>alert(“XSS”)</script>")]
        [TestCase("7031111234")]
        [TestCase("+1234 (201) 123-1234")]
        [TestCase("(001) 123-1234")]
        [TestCase("+01 (703) 123-1234")]
        [TestCase("(703) 123-1234 ext 204")]
        public void PhoneNumberValAttrTestWith_Given_InValidInputs_False(string phoneNumber)
        {
            // arrange
            var value = phoneNumber;
            var attrib = new PhoneMaskAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.False);
        }




        [TestCase("123-456-7890")]
        [TestCase("(123) 456-7890")]
        [TestCase("123 456 7890")]
        [TestCase("123.456.7890")]
        [TestCase("+91 (123) 456-7890")]
        public void PhoneNumberValAttrTestWith_My_CustomInputs_ValidInputs_True(string phoneNumber)
        {
            // arrange
            var value = phoneNumber;
            var attrib = new PhoneMaskAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.True);
        }



        [TestCase("&123-456-7890")]
        [TestCase("(12399) 456-7890")]
        [TestCase("123 456 7807790")]
        [TestCase("123.47776.7890")]
        [TestCase("+91 ([[23) 456-7890")]
        [TestCase("cbiwuegicv75475147")]
        [TestCase("e65-dguhg1-2197ydb-")]
        [TestCase("d7612-dd     -12hjhd")]
        public void PhoneNumberValAttrTestWith_My_CustomInputs_InValidInputs_False(string phoneNumber)
        {
            // arrange
            var value = phoneNumber;
            var attrib = new PhoneMaskAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.That(result, Is.False);
        }
    }
}
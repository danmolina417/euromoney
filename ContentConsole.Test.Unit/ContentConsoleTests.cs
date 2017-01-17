using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ContentConsole.Managers;
using ContentConsole.Models;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    class ContentConsoleTests
    {
        //Story 1
        //As a user
        //I want see the number of negative words in a text input
        //So that we can track the amount of negative content

        //Acceptance criteria
        //Total number of negative words output to screen
        //Console output the total number of negative words and the phrase analysed
        //Example output:
        //Scanned the text:
        //The weather in Manchester in winter is bad.It rains all the time - it must be horrible for people visiting.
        //Total Number of negative words: 2
        //Press ANY key to exit.

       [Test]
        public void ShouldCountBadWords()
        {
            // Establish System Under Test
            var sut = new ContentManager();

            var content = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            List<string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            var result = sut.GetWordCount(content, bannedWords);

            Assert.That(result, Is.EqualTo(2));
        }

        //Story 2
        //As an administrator
        //I want to be able to change the set of negative words counted
        //So that I don't need to change the code when we select new negative words or phrases

        //Acceptance criteria
        //Negative words retrieved from data store.
        //Number of negative words found respects words available from the data store.

        [Test]
        public void ShouldChangeSetOfBadWords()
        {
            // Establish System Under Test
            var sut = new ContentManager();

            var rm = new RoleManager();
            var userType = SecurityType.AdminUser;
            List<string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            sut.AddWordToList(ref bannedWords, "newWord", userType);
            var result = bannedWords.Count();

            Assert.That(result, Is.EqualTo(5));
        }

        //Story 2 tested with non-Admin (should fail)

        [Test]
        public void ShouldFailChangeSetOfBadWords()
        {
            // Establish System Under Test
            var sut = new ContentManager();

            var userType = SecurityType.Reader;
            List<string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            sut.AddWordToList(ref bannedWords, "newWord", userType);
            var result = bannedWords.Count();

            Assert.That(result, !Is.EqualTo(5));
        }


        //Story 3
        //As a reader
        //I want negative words filtered out of the text
        //So that our clients are not upset by negative language

        //Acceptance criteria
        //Any negative word in the text should have its middle replaced with hashes.
        //"Horrible"
        //should be outputted
        //"H######e"

        [Test]
        public void ShouldFilterOutBadWords()
        {
            // Establish System Under Test
            var sut = new ContentManager();

            var originalContent = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            var expectedContent = "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.";
            List<string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            var result = sut.FilterBannedWordList(originalContent, bannedWords);

            

            Assert.That(result, Is.EqualTo(expectedContent));
        }

        //Story 4
        //As an content curator
        //I want disable negative word filtering on the command line
        //So that I can see the original content.

        //Acceptance criteria
        //Count of negative words output
        //Original text output

        [Test]
        public void ShouldDisableFilterForCurator()
        {
            // Establish System Under Test
            var sut = new ContentManager();

            var originalContent = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            var userType = SecurityType.Curator;
            List<string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            
            //Set The filter off
            sut.SetFilter(false, userType);

            //Process the Content
            var result = sut.GetProcessedContent(originalContent, bannedWords, userType);
            
            Assert.That(result, Is.EqualTo(originalContent));

        }

        [Test]
        public void ShouldEnableFilterForCurator()
        {
            // Establish System Under Test
            var sut = new ContentManager();

            var originalContent = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            var expectedContent = "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.";
            var userType = SecurityType.Curator;
            List < string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            
            //Set The filter off
            sut.SetFilter(true, userType);

            //Process the Content
            var result = sut.GetProcessedContent(originalContent, bannedWords, userType);

            Assert.That(result, Is.EqualTo(expectedContent));

        }

        //This is to test that a reader cannot set turn filter off, 
        //and so GetProcessedContent() will return filter content
        [Test]
        public void ShouldNotDisableFilterForReader()
        {
            // Establish System Under Test
            var sut = new ContentManager();

            var originalContent = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            var expectedContent = "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.";
            List<string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            var userType = SecurityType.Reader;

            //Set The filter off
            sut.SetFilter(false, userType);

            //Process the Content
            var result = sut.GetProcessedContent(originalContent, bannedWords, userType);

            Assert.That(result, Is.EqualTo(expectedContent));

        }

    }
}

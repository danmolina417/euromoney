using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ContentConsole.Models;

namespace ContentConsole.Managers
{
    public class ContentManager
    {

        private bool FilteredViewOn = true;

        ///<summary>
        ///Counts the number of matches of items in list within the content
        ///Optional field caseMatch determines whether to use case sensitivity
        ///</summary>
        ///<returns>int value</returns>
        public int GetWordCount(string content, List<string> list, bool caseMatch = false)
        {
            //Convert string to queryable list
            string[] contentArray = content.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (caseMatch)
            {
                return contentArray.Where(x => list.Contains(x)).Count();
            }
            else
            {
                return contentArray.Where(x => list.Contains(x.ToLower())).Count();
            }
        }


        //As an administrator
        //I want to be able to change the set of negative words counted
        ///<summary>
        ///Allow addition of entry to a negative words list
        ///</summary>
        ///<returns>int value</returns>
        public void AddWordToList(ref List<string> list, string word, SecurityType userType)
        {
            if (userType == SecurityType.AdminUser)
            {
               list.Add(word);
            }
        }

        //As a reader
        //I want negative words filtered out of the text
        //So that our clients are not upset by negative language

        ///<summary>
        ///Filters content of a string with placeholder values for words that match entries in supplied list
        ///</summary>
        ///<returns>filter version of the content string</returns>
        public string FilterBannedWordList(string content, List<string> bannedWordsList)
        {
            // Find bad words and replace with filtered value
            foreach(var badWord in bannedWordsList)
            {
                var firstChar = badWord.Substring(0, 1);
                var lastChar = badWord.Substring(badWord.Length - 1, 1);
                var maskedCharacters = new String('#', badWord.Length - firstChar.Length - lastChar.Length);
                var filteredBadWord = string.Concat(firstChar, maskedCharacters, lastChar);
                content = content.Replace(badWord, filteredBadWord);
            }

            return content;
        }


        //Story 4

        //As an content curator
        //I want disable negative word filtering on the command line
        //So that I can see the original content.

        //Acceptance criteria

        //Count of negative words output
        //Original text output

        ///<summary>
        ///Processes the content for a view based on role and filter settings
        ///</summary>
        ///<returns>Process version of the content string</returns>
        public string GetProcessedContent(string content, List<string> bannedWordsList, SecurityType userType)
        {
            if ((userType == SecurityType.AdminUser || userType == SecurityType.Curator)
                && !FilteredViewOn)
            {
                return content;
            }
            return FilterBannedWordList(content, bannedWordsList);
        }

        public void SetFilter(bool b, SecurityType userType)
        {
            if (userType == SecurityType.AdminUser || userType == SecurityType.Curator)
            {
                FilteredViewOn = b;
            }
        }

        public bool GetFilterViewOn()
        {
            return FilteredViewOn;
        }

    }
}

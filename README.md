## Intro

One of the things we do at Euromoney is publish and manage content.
This assignment is to analyse text, detecting and filtering negative words.

- This assignment takes between 30 minutes and an hour.
- [NUnit](http://www.nunit.org) and [Moq](http://code.google.com/p/moq), references have been added using [NuGet](http://nuget.codeplex.com/) Packages.


---

## Notes from Dan Molina on code supplied



Daniel Molian on Code for Euromoney Code Challenge

Notes/Thoughts of code provided:

- The content list is loaded by a static string assigned in Project.cs.  This is essentially just to test the features of the ContentManager.

- Content would normally be pulled in through a ContentController that would either interface with data access layer or an ORM (Entity Framework), or pull from other sources (feeds, files, etc.).  For sake of the application challenge, I only loaded in memory, and allowed for the modification of a the content string value.

- The RoleManager was set up with hardcoded simulation of security permissions, based on some hardcoded usernames.  This obviously would be removed and an implementation of a call to Authenticate and access security levels would be added.   Implementation would be based on setup/needs.

- The console app has a lot of hardcoded console messages, and was done quickly to implement a user interface that satisifed the noted user stories.

- ContentManager could have contained the bannedword list, but wanted to have manager less tied to a specific list.  

- BannedWordList could have been modeled (implementing IEnumerable), but as this is just a List of strings, did not see the need to over architected.

Anticipated implementations if time permitted:

- Implementation of WebApi for services to do the following:
  * User login, authenticate and determine roler/permissions
  * Services to pull content from somewhere
  * Implement a web UI and user AngularJS to consume said servies, creating a single load experience
  
- Complete removal of word method.

- Ability to manage different filter list

 
---

## Task requirements

- All stories to be completed with an appropriate level of testing.
- No actual storage implementation or databases are required, feel free to stub or mock any data access you need.
- Reformat, refactor and rework the provided code in any way you see fit.
- Code must be supported by tests to be "done-done".

## Task Stories

Please complete each story in order.

---

### Story 1

As a **user**  
I want **see the number of negative words in a text input**  
So that **we can track the amount of negative content**

#### Acceptance criteria

- Total number of negative words output to screen
- Console output the total number of negative words and the phrase analysed
- Example output:
<pre>Scanned the text:
The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.
Total Number of negative words: 2
Press ANY key to exit.</pre>

---

### Story 2

As an **administrator**  
I want **to be able to change the set of negative words counted**  
So that **I don't need to change the code when we select new negative words or phrases**

#### Acceptance criteria

- Negative words retrieved from data store.
- Number of negative words found respects words available from the data store.

---

### Story 3

As a **reader**  
I want **negative words filtered out of the text**  
So that **our clients are not upset by negative language**

#### Acceptance criteria

- Any negative word in the text should have its middle replaced with hashes. <pre>"Horrible"</pre> should be outputted <pre>"H######e"</pre>.

---

### Story 4

As an **content curator**  
I want **disable negative word filtering on the command line**  
So that **I can see the original content**.

#### Acceptance criteria

- Count of negative words output
- Original text output

---

Thanks for your time, we look forward to hearing from you!
# euromoney

# STAT312WordAnalyzer

## Project Description
We want to analyze the complexity of words based on their source, proportion of vowels, \(etc?\). We will use the length of the word as a basic measure of the word�s complexity and this will be our response variable for all questions.
* What effect does word source have on Complexity? \(Wikipedia, Scholarly Journal, social media \(twitter\), reddit, etc.\)
* What effect does the proportion of vowels in a word have on Complexity?
* More? Words than end in �ing� or other suffix.

## Members
* Cameron Taylor
* David Pieper
* Khoa Nguyen
* Matt Seno
* Brad Harris (dropped)

## Schedule
|Completed	|Date	|Item to Complete	|
|:----------:|-------|-----------------------|
|![yes](http://img1.wikia.nocookie.net/__cb20100621101726/asheron/images/1/1d/Green_Check_Mark_Icon.png)|Oct 14	|Project Proposal	|
|![yes](http://img1.wikia.nocookie.net/__cb20100621101726/asheron/images/1/1d/Green_Check_Mark_Icon.png)|Oct 19	|Complexity Analysis Completed|
|![yes](http://img1.wikia.nocookie.net/__cb20100621101726/asheron/images/1/1d/Green_Check_Mark_Icon.png)|Nov 2	|Data Collection Program Completed|
|![yes](http://img1.wikia.nocookie.net/__cb20100621101726/asheron/images/1/1d/Green_Check_Mark_Icon.png)|Nov 2 |Met with Professor Smith to talk about Regression and Covariates|
|![yes](http://img1.wikia.nocookie.net/__cb20100621101726/asheron/images/1/1d/Green_Check_Mark_Icon.png)|Nov 9	|Project Update (Pilot Study and Power Analysis)|
||Nov 16	|Data for Final Project Collected in a Minitab File|
||Nov 30	|Presentation Completed and Practiced at least Once|
||Dec 7	|Final Presentation	|

## Download
You can download the most up to date version here: [Word Analyzer Files](http://www.mediafire.com/download/cf880cw5gisod1a/GUIDist.zip)

## Updates

#### 14 November 2015

Hey Guys,

##### Some clarification on what we are going to do for the rest of the project
Our project makes it hard to look at interaction factors accross the board because of the variety of sources that we are looking at. Date and Intended audience don't necessarily have the same prevalence in each source. As a result, we should split the project into the 3 sections clarified in the update bellow.

Our initial pilot study required that we gathered a sample of 2199 words per treatment.

- Source (with minimum sample size, this is a multiple of 2199 depending on the amount of sub-treatments, each subtreatment will need a minimum of 2199 words)
	- Wikipedia (4398 words)
	- Scholarly Articles (4398 words)
	- NYT articles (6597 words)
	- Social Media (2199 words)
	- Novels (6597 words)
- Date (only for novels and NYT articles)
	- After 1980\*
	- between 1900\* and 1980\*
	- before 1900\*
- Topic (only for Wikipedia and Scholarly Journals)
	- Hard Sciences (ex: Biology, Physics, Chemisty, Mathematics, Computer Science, etc.)
	- Social Sciences (ex: Current Events, History, Sports, etc.)

I will be updating the software to be able to take topic into account, so hold off on taking that data for now. (This update has been made, feel free to take the data)

I think for now, it would be best if we had Khoa take data from Wikipedia and Scholarly Journals, Matt take data for Novels and NYT articles, and David take data from Social Media. I can then put it all together into one file and process it in minitab (it might be good to do the processing together so we all know what the results were). Let me know if you have any questions and/or if you need help gathering your data. Also, be sure to document how you gathered your data, just write out a simple step-by-step so that someone else can see how we got the data.

It would be great if we got started with data collection this weekend!

Thanks!<br/>
\- Cameron

\* _These times can change, I just thought they would be interesting. Matt, please let me know whether or not the novels can be accessed for all of those dates._

#### 8 November 2015

Hey Guys,

I'm writing this after finishing up the Project Update, and I am super pleased with how the pilot study and power analysis turned out! However, it has become clear that factors other than Source are going to be difficult to deal with, and I think we need to give that some thought. Whether or not a word starts wtih a vowel works, but it does not really have a whole lot of meaning.

A couple suggestions that have come up have been the time that the text was written, and the intended audience of the text. Both of these seem like they would be very interesting questions to answer. I think that the best way to go about it is to split our study into 3 main parts. One focusing on Source, one on Date, and one on Intended Audience.

* <strong>Source</strong> will include analysis between all sources that we gather data from, and there will be no focus on interation.
* <strong>Date</strong> will only look at Novels and New York Times articles (I found an archive of articles that date back to 1851). This will include interaction analysis between date written and Novels vs. NYT article.
* <strong>Intended Audience</strong> will only look at Wikipedia and Scholarly Journals. This will include interaction analysis between intended audience and wikipedia vs scholarly journals.

There will have to be some minor changes to the software in order to take all this into account, but that will be easy to do. We will also want to start our data collection for this as soon as possible since we will need a lot of it.

Thanks everyone!<br/>
\- Cameron

#### 1 November 2015

[Word Analyzer Files](http://www.mediafire.com/download/zl6f3b9wfp6took/WordAnalyzerDistribution.zip)

Hi guys, I have gotten the software to a point where it can be used to start to collect Data. Download and Extract the files above \(You may also need to download the [.NET framework](https://www.microsoft.com/en-us/download/confirmation.aspx?id=42643) if you haven't already\). The software only works on Windows. Make sure to provide a date for your source in the software, and uncheck the 'Use Date' box if you cannot find a date. It would be good if everyone could download that and start testing the software. If you have any questions, or find a problem with the software PLEASE LET ME KNOW!

Last Wednesday, we also decided that each of us would gather data from an individual source, and it will be up to you individually to determine how to collect your data, just make sure that you are getting a valid random sample, and document your procedure for collecting data. I think a good target to shoot for would be about 1000 words from your source.

|Name	|Source to Collect from	|
|-------|-----------------------|
|Cameron|Wikipedia		|
|Khoa	|Scholarly Journals	|
|Matt	|Books/Novels		|
|David	|Reddit			|
|Brad	|Social Media (pick one)|

Khoa has set up a meeting with Professor Smith on Friday 6 Nov 2015 from 1:30-2:30 in her office to talk about Covariates and Regression. If you can make it to that, please do! I will also be meeting with her to talk about some details on collecting data. Once that is done, we can start the first data collection for the pilot study.

Thanks everyone!<br/>
\- Cameron Taylor

<hr/>

#### 14 October 2015
Hi guys, sorry about any confusion that may have happened with the 5 people, but Professor Smith was pretty awesome and is letting us work in a team of 5. She had also already let us pursue this project despite being an observational study. Because of this, I really want to make sure that everyone does their best and we do produce a project that is worth of the work of 5 people.

Since there is a heavy emphasis on coding for the project, it is great that we have 3 people in a CS discipline. This Github page will be where I am keeping the code for the project. It will allow anyone to view/edit the code as they please. I also think that the README file would be a good place to post larger updates (like this one).

Khoa has agreed to reserve a fishbowl in the library from 2-4 every wednesday for us. We can use that as time to work on the project, work on stats homework, or study for midterms. This will start on the 21st. This is not the only time that we can meet, and other times can be arranged if needed, but with 5 people it is difficult to find a time that works for everyone.

I also posted a schedule with a basic outline of tasks we should complete. Hopefuly we can keep to it and be done a week before the final presentation. By next Monday, I think it would be awesome to have our complexity algorithm figured out, so let me know if people have suggestions on what parameters make a word less or more complex.

It would also be great if people could come up with some ideas for how to get random text from Twitter, Scholarly Journals, Wikipedia, Reddit, and any other sources you can think of (the more the better). The links at the bottom of this post have our current ideas.

Some other links:
* [Word Complexity Algorithm](https://github.com/axis7818/STAT312WordAnalyzer/blob/master/WordComplexity.md)
* [Word Sources](https://github.com/axis7818/STAT312WordAnalyzer/blob/master/DataRetrieval.md)
* [RexEgg](http://www.rexegg.com/) Regular Expressions are something that will be of great use in this project. I suggest that Khoa and Brad have a look here to get a basic understanding of what they are. \(I think they are awesome...\)


Thanks everyone!<br/>
\- Cameron Taylor

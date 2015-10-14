# Data Retrieval

We need ideas for how to gather chunks of text from our sources. If you have any ideas, please let me know\! Current ideas include...

#### Wikipedia
Two stage cluster sampling<br/>
1. Use Wikipedia's "Random Page" button to get a random cluster<br/>
2. Randomly sample _n_ words from the page.

#### Reddit
Multi stage cluster sampling
1. Use Reddit's "Random Page" button to get a random cluster<br/>
2. Randomly generate a number \(from 1 to _infinity_\). This number will be the number of the post that we look at. If the number is larger than the number of posts, then we will "wrap" the numbers. So if the number is 17, and there are 10 posts, then we will look at post 7.<br/>
3. randomly select _n_ words from the comment secion.

#### We still need an idea for how to collect from:
* Twitter
* Scholarly Articles and Journals
* Other potential sources

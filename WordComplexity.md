# Word Complexity

We need ideas for what factors affect the complexity of a word. If you have any ideas, please let me know\! Current ideas include...
* length
* number of unique characters
* repetition
  * repeating characters (WHEEEEEEE!)
  * repeating sequences (Banana)
* number of syllables 
* different letters having different weights \(z worth more than e\)

Once we have gathered all of these parameters, we can pretty simply come up with an algorithm to compute a measure of "Word Complexity".

## Current Algorithm

C = complexity measure<br/>
u = number of unique characters in the word<br/>
l = length of the word<br/>
S = the word's Scrabble score<br/>
r = number of repeated single characters<br/>
R = number of repeated sequences of characters

C = [u(S + l - r)] / [l(R + 1)]


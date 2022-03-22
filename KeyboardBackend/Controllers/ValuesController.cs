using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Cors;

namespace KeyboardBackend.Controllers
{
    
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public Dictionary dictionary = new Dictionary();

        [Route("/")]
        [EnableCors]
        [HttpGet("{searchString}")]

        // inputs a search string, finds all words that begin with that string, counts the frequency of the next letter, and returns the top 7 letters and 9 results
        public string[] Get(string searchString)
        {
            searchString = searchString.ToLower();
            var charactersAndResults = new string[] { "A", "C", "M", "P", "S", "T", "U", "and", "for", "have", "not", "that", "the", "this", "with", "you" };
            var charactersToIgnore = "0123456789.!?,-@()#%/";
            foreach (var character in charactersToIgnore)
            {
                searchString = searchString.Replace(character.ToString(), "");
            }
            if (searchString != null && searchString.Length > 0)
            {
                var shortList = dictionary.wordList.Where(word => word.StartsWith(searchString.ToLower())).ToList();
                shortList.Remove(searchString.ToLower());
                //var shortArray = shortList.Take(10).ToArray();
                charactersAndResults[7] = searchString.ToLower();
                charactersAndResults[8] = (shortList.Count >= 1) ? shortList[0] : "&nbsp";
                charactersAndResults[9] = (shortList.Count >= 2) ? shortList[1] : "&nbsp";
                charactersAndResults[10] = (shortList.Count >= 3) ? shortList[2] : "&nbsp";
                charactersAndResults[11] = (shortList.Count >= 4) ? shortList[3] : "&nbsp";
                charactersAndResults[12] = (shortList.Count >= 5) ? shortList[4] : "&nbsp";
                charactersAndResults[13] = (shortList.Count >= 6) ? shortList[5] : "&nbsp";
                charactersAndResults[14] = (shortList.Count >= 7) ? shortList[6] : "&nbsp";
                charactersAndResults[15] = (shortList.Count >= 8) ? shortList[7] : "&nbsp";

                var nextLetterList = new List<char>();
                foreach (var word in shortList)
                {
                    nextLetterList.Add(word[searchString.Length]);
                };
                var letterList = "abcdefghijklmnopqrstuvwxyz";
                var letterDistribution = new Dictionary<string, int>();
                var letterFrequency = 0;
                for (int i = 0; i < letterList.Length; i++)
                {
                    letterFrequency = nextLetterList.Where(nextLetter => nextLetter.Equals(letterList[i])).Count();

                    if (letterFrequency > 0)
                    {
                        letterDistribution.Add(letterList[i].ToString(), letterFrequency);
                    }
                }
                //letterDistribution.OrderByDescending(item => item.Value);
                var sortedLetters = (from entry in letterDistribution orderby entry.Value descending select entry.Key).Take(7).ToList();
                //var top7Letters = new List<string>();
                var top7Letters = new List<string>();
                top7Letters = sortedLetters.OrderBy(letter => letter).ToList();
                charactersAndResults[0] = (top7Letters.Count() >= 1) ? top7Letters[0].ToUpper() : "&nbsp";
                charactersAndResults[1] = (top7Letters.Count() >= 2) ? top7Letters[1].ToUpper() : "&nbsp";
                charactersAndResults[2] = (top7Letters.Count() >= 3) ? top7Letters[2].ToUpper() : "&nbsp";
                charactersAndResults[3] = (top7Letters.Count() >= 4) ? top7Letters[3].ToUpper() : "&nbsp";
                charactersAndResults[4] = (top7Letters.Count() >= 5) ? top7Letters[4].ToUpper() : "&nbsp";
                charactersAndResults[5] = (top7Letters.Count() >= 6) ? top7Letters[5].ToUpper() : "&nbsp";
                charactersAndResults[6] = (top7Letters.Count() >= 7) ? top7Letters[6].ToUpper() : "&nbsp";
            }
            return charactersAndResults;
        }
    }
}

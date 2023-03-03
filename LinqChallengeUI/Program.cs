using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace LinqChallengeUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");


        //https://stackoverflow.com/questions/33718144/do-arabic-characters-have-different-unicode-code-points-based-on-position-in-str

        // Python has an Arabic reshaper; https://pypi.org/project/arabic-reshaper/

        // use IronPython to call the Python script;

        // but six letters (و ,ز ,ر ,ذ ,د ,ا) can only be linked to their preceding letter. For example, أرارات (Ararat) has only

        // to do: figure out if all these letters are in the isolated form; are there any medial/final/initial forms
        // that get displayed;

        // https://stackoverflow.com/questions/58152091/the-structure-of-arabic-letters-in-unicode
        // When the Arabic text in that file is typeset/displayed, it undergoes a process called shaping.

        //            Some important concepts are at play here because Unicode text files are in the business of storing… well, text(Unicode), and typesetting and display systems are in the business of using fonts and glyphs(OpenType):

        //the text file saved the Arabic characters in a left-to - right order but Arabic is read / displayed from right-to - left: text files store text in so - called logical order;
        //            the text file contains individual characters that look very different to the actual display presented on the screen: the text file contains the Arabic characters in their isolated, non-joined, form.

        //https://stackoverflow.com/questions/58152091/the-structure-of-arabic-letters-in-unicode

        //// https://en.wikipedia.org/wiki/Kashida#:~:text=The%20Unicode%20standard%20assigns%20code%20point%20U%2B0640%20as%20Arabic%20Tatweel.

        // Goal: Create a LINQ query that takes in a string and returns a list of all letters in order, regardless of case;

        // I always find myself in need of a text editor in which one can enable or disable the rendering of diacritics together with base letters, so that all characters can also be seen isolated. I got the recommendation recently of using BabelPad:

        https://www.babelstone.co.uk/Software/BabelPad.html

           // BabelPad includes the option to “render all Unicode characters as individual spacing glyphs(i.e.with no shaping or joining of complex text)”. The only problem of BabelPad is that it is only supported by Windows, but I managed to make it work in Linux using wine.So if anyone also needs this functionality, this editor is perfect.

            // Bonus: Modify the LINQ query to return a list of anonymous objects, each with a Letter property and a Count property.
            // popuate the count with the number of times a letter is used. order the list by letter count (max first) then by character

            //string myString = "These are the times that try men's souls. Fourscore and eight years ago.";
            string myString = "وقالت زاخاروفا إن البيانات الصادرة عن العديد من المختصين في مجلس حقوق الإنسان التابع للأمم المتحدة بشأن";

            // https://www.sublimetext.com/3 >> sublime does not support Arabic;


            string fileName = "c:\\temp\\myArabic.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(myString);
            }



            var letterCount = myString.Where(x => char.IsLetter(x)).OrderBy(x => char.ToLower(x)).GroupBy(x => char.ToLower(x))
                              .Select(g => new
                              {
                                  Count = g.Count(),
                                  Letter = g.Key,
                              }).OrderByDescending(g => g.Count);

            foreach (var item in letterCount)
            {
                Console.WriteLine($"{ item.Letter } occurs { item.Count} times");
            }

            //here is a

            Console.ReadLine();
            //foreach (var item in letters)
            //{
            //    Console.WriteLine(item);
            //}


            

        }
    }
}

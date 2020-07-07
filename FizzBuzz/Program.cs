using System;
using System.Collections.Generic;

namespace FizzBuzz
{

    class FizzBuzzer
    {
        private List<string> words = new List<string>();

        private List<Action<int>> rules = new List<Action<int>>();

        private void newRule(int x, int d, string ruleName, bool removeRest, bool addEnd, char beforeLetter,
            bool reverseRule)
        {
            if (x % d == 0)
            {
                if (removeRest)
                {
                    words.Clear();
                }

                if (addEnd)
                {
                    words.Add(ruleName);
                }

                if (beforeLetter != '0')
                {
                    words.Add(ruleName);
                    for (int j = 0; j < words.Count-1; j++)
                    {
                        if (words[j][0] == beforeLetter)
                        {
                            for (int k = words.Count - 1; k > j; k--)
                            {
                                words[k] = words[k - 1];
                            }
                            words[j] = ruleName;
                            break;
                        }
                    }
                }

                if (reverseRule)
                {
                    words.Reverse();
                }
            }
        }

        public void addRule(int d, string ruleName, bool removeRest, bool addEnd, char beforeLetter,
            bool reverseRule, List<int> validNumbers)
        {
            if (validNumbers.Contains(d))
            {
                rules.Add(x => newRule(x, d, ruleName, removeRest, addEnd, beforeLetter, reverseRule));
            }
        }

        public void applyRules(int x)
        {
            foreach (Action<int> rule in rules)
            {
                rule(x);
            }
        }

        public void printResult(int i)
        {
            if (words.Count > 0)
            {
                string finalWord = "";
                    
                for (int j = 0; j <= words.Count-1; j++)
                {
                    finalWord += words[j];
                }
                Console.WriteLine(finalWord);
            }
            else
            {
                Console.WriteLine(i);
            }
            // Reset words list for next iteration
            words.Clear();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!");
            FizzBuzzer fb = new FizzBuzzer();
            
            Console.WriteLine("Which number rules would you like to be valid?");
            string[] str = Console.ReadLine().Split(' ');
            List<int> validNumbers = new List<int>();
            for (int i = 0; i <= str.Length - 1; i++)
            {
                validNumbers.Add(Convert.ToInt32(str[i]));
            }

            void askRule()
            {
                Console.WriteLine("Would you like to add a new rule? (Y/N)");
                string ruleName;
                int d;
                bool removeRest, addEnd, reverseRule;
                char beforeLetter;

                if (Convert.ToChar(Console.ReadLine().ToUpper()) == 'Y')
                {
                    Console.WriteLine("Great! What's the rule name?");
                    ruleName = Console.ReadLine();

                    Console.WriteLine("And what's the factor your number should be divisible by?");
                    d = Convert.ToInt32(Console.ReadLine());

                    // Clear words rule
                    Console.WriteLine("Should this rule clear the other words applied up until now? (Y/N)");
                    if (Convert.ToChar(Console.ReadLine().ToUpper()) == 'Y')
                    {
                        removeRest = true;
                    }
                    else
                    {
                        removeRest = false;
                    }

                    // Append word rule
                    Console.WriteLine("Should this rule append the word? (Y/N)");
                    if (Convert.ToChar(Console.ReadLine().ToUpper()) == 'Y')
                    {
                        addEnd = true;
                    }
                    else
                    {
                        addEnd = false;
                    }

                    // Add word before first word starting with a letter rule
                    Console.WriteLine(
                        "Should this rule add the word before the first word starting with a certain letter? (Y/N)");
                    if (Convert.ToChar(Console.ReadLine().ToUpper()) == 'Y')
                    {
                        Console.WriteLine("Which letter should that be?");
                        beforeLetter = Convert.ToChar(Console.ReadLine().ToUpper());
                    }
                    else
                    {
                        beforeLetter = '0';
                    }

                    // Reverse words rule
                    Console.WriteLine("Should this rule reverse the order of all the words? (Y/N)");
                    if (Convert.ToChar(Console.ReadLine().ToUpper()) == 'Y')
                    {
                        reverseRule = true;
                    }
                    else
                    {
                        reverseRule = false;
                    }

                    fb.addRule(d, ruleName, removeRest, addEnd, beforeLetter, reverseRule, validNumbers);
                    Console.WriteLine("Alright! Added the rule!");
                    askRule();
                }
            }
            askRule();

            fb.addRule(3, "Fizz", false, true, '0', false, validNumbers);
            fb.addRule(5, "Buzz", false, true, '0', false, validNumbers);
            fb.addRule(7, "Bang", false, true, '0', false, validNumbers);
            fb.addRule(11, "Bong", true, true, '0', false, validNumbers);
            fb.addRule(13, "Fezz", false, false, 'B', false, validNumbers);
            fb.addRule(17, "", false, false, '0', true, validNumbers);

            Console.WriteLine("How many numbers should we count?");
            int max = Convert.ToInt32(Console.ReadLine());
            
            for (int i = 1; i <= max; i++)
            {
                fb.applyRules(i);
                fb.printResult(i);
            }
        }
    }
}
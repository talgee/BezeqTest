using System.Text;

namespace BezeqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Sapien cubilia  tortor Eleifend praesent  cubilia  cubilia tortor Pretium eleifend";
            wordCount(text);

            string text2 = "ab./b,A";
            checkPalindrom(text2);

            int[][] jaggedArray = {
                new int[] {1, 3},
                new int[] {5, 7, 9},
                new int[] {3, 1},
                new int[] {3, 6, 7},
                new int[] {9, 7, 5},
                new int[] {3, 67}
                };

            findDuplicateJagged(jaggedArray);
        }

        private static void findDuplicateJagged(int[][] testData)
        {
            var testSets = testData.Select(s => new HashSet<int>(s));

            var groupedSets = testSets.GroupBy(s => s, HashSet<int>.CreateSetComparer());

            foreach (var g in groupedSets)
            {
                var setString = String.Join(", ", g.Key);
                Console.WriteLine($" {g.Count()} | {setString}");
            }

            groupedSets.ToArray();

            int[][] testDataNew = new int[groupedSets.Count()][];
            int count = 0;
            foreach (var gs in groupedSets) 
            {
                if (gs.Count() > 0) 
                {
                    foreach (var g in gs)
                    {
                        testDataNew[count] = g.ToArray();
                        break;
                    }
                }

                count++;
                continue;
            }
        }

        private static bool checkPalindrom(string myString)
        {
            myString = myString.ToLower();
            myString = removePunctioation(myString);

            string first = myString.Substring(0, myString.Length / 2);
            char[] arr = myString.ToCharArray();

            Array.Reverse(arr);

            string temp = new string(arr);
            string second = temp.Substring(0, temp.Length / 2);
            
            var res = first.Equals(second);
            return res;
        }

        private static string removePunctioation(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (!char.IsPunctuation(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private static string[] removeEmptyPlaces(string[] splitedList) 
        {
            return splitedList.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        }

        private static Dictionary<string, int> removeDuplicates(Dictionary<string, int> dict)
        {
            var uniqueValues = dict.GroupBy(pair => pair.Key.ToLower())
                         .Select(group => group.First())
                         .ToDictionary(pair => pair.Key, pair => pair.Value)
                         .OrderByDescending(pair => pair.Value);

            Dictionary<string, int> newDict = new Dictionary<string, int>();
            foreach (var uniqueValue in uniqueValues) 
            {
                newDict.Add(uniqueValue.Key, uniqueValue.Value);
            }

            return newDict;
        }

        private static void wordCount(string text) 
        {
            var source = text.Split(" ");
            source = removeEmptyPlaces(source);

            int wordCount;
            Dictionary<string, int> _dict = new Dictionary<string, int>();

            foreach (var s in source) 
            {
                wordCount = 0;
                var matchQuery = from word in source
                                 where word.Equals(s, StringComparison.InvariantCultureIgnoreCase)
                                 select word;

                wordCount = matchQuery.Count();

                if (!_dict.ContainsKey(s))
                {
                    _dict.Add(s, wordCount);
                }
                else 
                {
                    continue;
                }
            }

            var finalDict = removeDuplicates(_dict);

            foreach (var fd in finalDict) 
            {
                Console.WriteLine($"{fd.Key} occurrences(s) of the search term \"{fd.Value}\" were found.");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

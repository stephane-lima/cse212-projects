using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        var set = new HashSet<string>(words);
        var used = new HashSet<string>();

        // Store matching pairs
        var pairs = new List<string>();

        string reversed;

        // Loop through each word in the set
        foreach (string word in set)
        {
            char firstChar = word[0];
            char secondChar = word[1];
            
            // Ignore words like "aa"
            if (firstChar == secondChar)
            {
                continue;
            }

            // Reverse the current word
            reversed = $"{secondChar}{firstChar}";
            // reversed = new string(new[] {secondChar, firstChar});

            // Check if reverse exists in the set and also check if we have not used this pair yet
            if (set.Contains(reversed) && !used.Contains(word) && !used.Contains(reversed))
            {
                pairs.Add($"{reversed} & {word}");

                // Mark both as used so they are never processed again
                used.Add(word);
                used.Add(reversed);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            
            // TODO Problem 2 - ADD YOUR CODE HERE
            var degreeInfo = fields[3];

            if (degrees.ContainsKey(degreeInfo))
            {
                degrees[degreeInfo] += 1;
            }
            else
            {
                degrees[degreeInfo] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        // Remove all spaces and convert both words to lowercase
        // so that spaces and letter case do not affect comparison
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // If the words do not have the same number of letters, they cannot be anagrams
        if (word1.Length != word2.Length)
        {
            return false;
        }

        // Dictionary to store each letter and the number of times it appears
        var letterCounts = new Dictionary<char, int>();

        // Go through each letter in the first word
        // and count how many times each letter appears
        foreach (char letter in word1)
        {
            if (letterCounts.ContainsKey(letter))
            {
                letterCounts[letter] += 1;
            }
            else
            {
                letterCounts[letter] = 1;
            }
        }

        // Go through each letter in the second word
        // and decrease the count for matching letters
        foreach (char letter in word2)
        {
            // If the letter does not exist in the dictionary, the words cannot be anagrams
            if (!letterCounts.ContainsKey(letter))
            {
                return false;
            }

            letterCounts[letter] -= 1;

            // If the count goes below 0, the second word has too many of this letter
            if (letterCounts[letter] < 0)
            {
                return false;
            }
        }

        // if all letters matched correctly, the words are anagrams
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        var summaries = new List<string>();

        string place;
        decimal magnitude;

        foreach (var feature in featureCollection.Features)
        {
            // Get earthquake Location
            place = feature.Properties.Place;

            // Get earthquake Magnitude
            magnitude = feature.Properties.Mag;

            summaries.Add($"{place} - Mag {magnitude}");
        }

        return summaries.ToArray();
    }
}
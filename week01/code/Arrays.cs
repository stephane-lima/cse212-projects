public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // DETAILED PLAN:
        // 1. Create a new array of type double with size equal to "length".
        //    This array will store all the multiples we generate.
        //
        // 2. We will use a loop that starts at index 0 and goes up to length - 1.
        //    Each index represents the position in the result array.
        //
        // 3. For each index i:
        //      - We calculate the multiplier as (i + 1)
        //        because multiples start at 1 × number, not 0 × number.
        //      - Multiply "number" by (i + 1)
        //      - Store the result in array at position i
        //
        // 4. After the loop finishes, all positions in the array will be filled.
        //
        // 5. Return the completed array.

        var multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // DETAILED PLAN:
        //
        // 1. Understand the goal:
        //    We want to move the last "amount" elements of the list
        //    to the front, while keeping their original order.
        //
        // 2. Determine where to split the list:
        //    We calculate the starting index of those elements:
        //    splitIndex = total number of elements - amount
        //    Use splitIndex = data.Count - amount
        //
        //    Everything from splitIndex to the end of the list
        //    will be moved to the front.
        //
        // 3. Step A - Copy the last "amount" elements:
        //    Use GetRange(splitIndex, amount)
        //    This creates a new list containing those elements.
        //
        // 4. Step B - Remove those same elements from the original list:
        //    Use RemoveRange(splitIndex, amount)
        //    This shortens the original list.
        //
        // 5. Step C - Insert the saved elements at the beginning:
        //    Use InsertRange(0, savedElements)
        //
        // 6. Result:
        //    The list is now rotated to the right by "amount".

        int splitIndex = data.Count - amount;

        var savedElements = data.GetRange(splitIndex, amount);

        data.RemoveRange(splitIndex, amount);

        data.InsertRange(0, savedElements);
    }
}

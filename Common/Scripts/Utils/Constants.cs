
using System.Collections.Generic;
using UnityEngine;


public static class Constants
{
    public static int bookId;

    public static int mathGameId;

    public static int mathStarCount;

    public static int[] GetRandomIndex(int count, int first = 0, int last = 10)
    {
        List<int> indexs = new List<int>();
        int random = Random.Range(first, last);

        indexs.Add(random);

        while (indexs.Count < count)
        {
            random = Random.Range(first, last);
            if (!indexs.Contains(random))
            {
                indexs.Add(random);
            }
        }

        return indexs.ToArray();
    }


}

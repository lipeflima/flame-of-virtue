using UnityEngine;

public static class MathOperations
{
    public static int GetNthPrime(int n)
    {
        int count = 0;
        int number = 2;

        while (true)
        {
            if (IsPrime(number))
            {
                count++;
                if (count == n)
                    return number;
            }
            number++;
        }
    }

    public static bool IsPrime(int num)
    {
        if (num < 2) return false;
        if (num == 2) return true;
        if (num % 2 == 0) return false;

        int sqrt = Mathf.CeilToInt(Mathf.Sqrt(num));
        for (int i = 3; i <= sqrt; i += 2)
        {
            if (num % i == 0)
                return false;
        }
        return true;
    }
}

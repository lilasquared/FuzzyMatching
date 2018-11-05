using System;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class Levenshtein
    {
        public Int32 GetDistance(String s, String t)
        {
            var n = s.Length;
            var m = t.Length;
            var d = new Int32[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (var i = 0; i <= n; i++)
            {
                d[i, 0] = i;
            }

            for (var j = 0; j <= m; j++)
            {
                d[0, j] = j;
            }

            for (var j = 1; j <= m; j++)
            {
                for (var i = 1; i <= n; i++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        d[i, j] = d[i - 1, j - 1];
                    }
                    else
                    {
                        d[i, j] =
                            Math.Min(
                                Math.Min(
                                    d[i - 1, j] + 1,
                                    d[i, j - 1] + 1
                                ),
                                d[i - 1, j - 1] + 1
                            );
                    }
                }
            }

            return d[n, m];
        }

        public Double GetRatio(String s, String t)
        {
            var sum = s.Length + t.Length * 1.0;
            var distance = GetDistance(s, t);
            return (sum - distance) / sum;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;

namespace FuzzyMatch.Core
{
    public static class LiteFileStreamExtensions
    {
        public static IEnumerable<String> ReadLines(this LiteFileStream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}

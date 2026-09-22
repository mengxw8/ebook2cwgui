using System.Collections.Generic;
using System.Linq;

namespace CW
{
    internal static class MorseCodeSet
    {
        public static Dictionary<char, string> Create(int encodingType, IReadOnlyDictionary<char, string>? custom = null)
        {
            if (encodingType == 3 && custom is { Count: > 0 })
                return new Dictionary<char, string>(custom);

            return encodingType switch
            {
                1 => Merge(Constant.header, Constant.shortNumber5, Constant.alphabet, Constant.symbol),
                2 => Merge(Constant.header, Constant.shortNumber10, Constant.alphabet, Constant.symbol),
                _ => Merge(Constant.header, Constant.allCharCode),
            };
        }

        private static Dictionary<char, string> Merge(params Dictionary<char, string>[] parts)
        {
            return parts.SelectMany(part => part)
                .GroupBy(pair => pair.Key)
                .ToDictionary(group => group.Key, group => group.Last().Value);
        }
    }
}

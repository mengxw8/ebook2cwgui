namespace CW
{
    /// <summary>
    /// 按组生成练习报文。字符或不同组不够时直接失败，避免重试死循环。
    /// </summary>
    internal static class GroupTextGenerator
    {
        public static bool TryGenerate(IReadOnlyList<char> charset, int perGroup, int groupCount, bool uniqueInGroup, bool uniqueGroups, Random random, out string text, out string? error)
        {
            if (charset.Count == 0)
            {
                text = "";
                error = "请选择字母、数字、符号，或填写自定义字符。";
                return false;
            }

            if (perGroup < 1 || groupCount < 1)
            {
                text = "";
                error = "每组字符数和组数都要大于 0。";
                return false;
            }

            if (uniqueInGroup && charset.Count < perGroup)
            {
                text = "";
                error = $"组内不重复需要至少 {perGroup} 个不同字符，当前只有 {charset.Count} 个。";
                return false;
            }

            if (uniqueGroups && !HasEnoughDistinctGroups(charset.Count, perGroup, groupCount, uniqueInGroup, out long possible))
            {
                text = "";
                error = $"组不重复时，当前字符范围最多只能组成 {possible} 种不同的组，无法生成 {groupCount} 组。";
                return false;
            }

            var pool = new char[charset.Count];
            for (int i = 0; i < charset.Count; i++)
                pool[i] = charset[i];

            var builder = new System.Text.StringBuilder(groupCount * (perGroup + 1));
            var buffer = new char[perGroup];
            var seen = uniqueGroups ? new HashSet<string>() : null;
            int rejected = 0;
            int rejectLimit = Math.Max(groupCount * 64, 256);
            for (int group = 0; group < groupCount; group++)
            {
                string token;
                while (true)
                {
                    FillGroup(pool, buffer, uniqueInGroup, random);
                    token = new string(buffer);
                    if (seen == null || seen.Add(token))
                        break;
                    if (++rejected > rejectLimit)
                    {
                        text = "";
                        error = "无法生成足够多的不同组，请扩大字符范围或减少组数。";
                        return false;
                    }
                }

                if (group > 0)
                    builder.Append(' ');
                builder.Append(token);
            }

            text = builder.ToString();
            error = null;
            return true;
        }

        private static bool HasEnoughDistinctGroups(int charsetCount, int perGroup, int groupCount, bool uniqueInGroup, out long possible)
        {
            possible = 1;
            for (int i = 0; i < perGroup; i++)
            {
                int factor = uniqueInGroup ? charsetCount - i : charsetCount;
                if (factor <= 0)
                {
                    possible = 0;
                    return false;
                }

                if (possible > long.MaxValue / factor)
                {
                    possible = long.MaxValue;
                    return true;
                }

                possible *= factor;
                if (possible >= groupCount)
                    return true;
            }

            return possible >= groupCount;
        }

        private static void FillGroup(char[] pool, char[] buffer, bool uniqueInGroup, Random random)
        {
            if (uniqueInGroup)
            {
                Shuffle(pool, pool.Length, random);
                for (int i = 0; i < buffer.Length; i++)
                    buffer[i] = pool[i];
                return;
            }

            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = pool[random.Next(pool.Length)];
        }

        private static void Shuffle(char[] pool, int count, Random random)
        {
            for (int i = 0; i < count; i++)
            {
                int j = random.Next(i, count);
                (pool[i], pool[j]) = (pool[j], pool[i]);
            }
        }
    }
}

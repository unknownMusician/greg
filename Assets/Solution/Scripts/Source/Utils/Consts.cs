using System;
using System.Collections.Generic;
using Greg.Data;

namespace Greg.Utils
{
    public static class Consts
    {
        public static readonly IReadOnlyList<CrowdSfxCharacterType> CrowdSfxCharacterTypeValues =
            (CrowdSfxCharacterType[])Enum.GetValues(typeof(CrowdSfxCharacterType));
    }
}
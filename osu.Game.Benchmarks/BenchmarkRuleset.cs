// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Online.API;

namespace osu.Game.Benchmarks
{
    public class BenchmarkRuleset : BenchmarkTest
    {
        private APIMod apiModDoubleTime = null!;
        private APIMod apiModDifficultyAdjust = null!;

        public override void SetUp()
        {
            base.SetUp();
            apiModDoubleTime = new APIMod { Acronym = "DT" };
            apiModDifficultyAdjust = new APIMod { Acronym = "DA" };
        }
    }
}

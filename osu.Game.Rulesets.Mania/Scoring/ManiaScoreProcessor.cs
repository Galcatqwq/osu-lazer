// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

//using System;

using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Mania.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Scoring;

// ReSharper disable MissingBlankLines
// ReSharper disable MultipleSpaces
// ReSharper disable PartialTypeWithSinglePart

namespace osu.Game.Rulesets.Mania.Scoring
{
    public partial class ManiaScoreProcessor : ScoreProcessor
    {
        private const double accuracy_cutoff_x = 1;
        private const double accuracy_cutoff_s = 0.95;
        private const double accuracy_cutoff_a = 0.9;
        private const double accuracy_cutoff_b = 0.8;
        private const double accuracy_cutoff_c = 0.7;
        private const double accuracy_cutoff_d = 0;

        public ManiaScoreProcessor()
            : base(new ManiaRuleset())
        {
        }

        protected override IEnumerable<HitObject> EnumerateHitObjects(IBeatmap beatmap)
            => base.EnumerateHitObjects(beatmap).Order(JudgementOrderComparer.DEFAULT);

        public override int GetBaseScoreForResult(HitResult result)
        {
            switch (result)
            {
                case HitResult.Perfect: return 320; // MAX
                case HitResult.Great:   return 300; // 300
                case HitResult.Good:    return 200; // 200
                case HitResult.Ok:      return 100; // 100
                case HitResult.Meh:     return 50;  // 50
                default:                return 0;   // MISS
            }
        }

        protected override double GetV1ScoreChange(JudgementResult result)
        {
            return GetBaseScoreForResult(result.Type);
        }

        protected override double ComputeTotalScore(double comboProgress, double accuracyProgress, double bonusPortion, double currentV1BasePortion)
        {
            return currentV1BasePortion;
        }

        public override ScoreRank RankFromScore(double accuracy, IReadOnlyDictionary<HitResult, int> results)
        {
            if (accuracy == accuracy_cutoff_x)
                return ScoreRank.X;
            if (accuracy >= accuracy_cutoff_s)
                return ScoreRank.S;
            if (accuracy >= accuracy_cutoff_a)
                return ScoreRank.A;
            if (accuracy >= accuracy_cutoff_b)
                return ScoreRank.B;
            if (accuracy >= accuracy_cutoff_c)
                return ScoreRank.C;

            return ScoreRank.D;
        }

        private class JudgementOrderComparer : IComparer<HitObject>
        {
            public static readonly JudgementOrderComparer DEFAULT = new JudgementOrderComparer();

            public int Compare(HitObject? x, HitObject? y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(x, null)) return -1;
                if (ReferenceEquals(y, null)) return 1;

                int result = x.GetEndTime().CompareTo(y.GetEndTime());
                if (result != 0)
                    return result;

                // due to the way input is handled in mania, notes take precedence over ticks in judging order.
                if (x is Note && y is not Note) return -1;
                if (x is not Note && y is Note) return 1;

                return x is ManiaHitObject maniaX && y is ManiaHitObject maniaY
                    ? maniaX.Column.CompareTo(maniaY.Column)
                    : 0;
            }
        }
    }
}

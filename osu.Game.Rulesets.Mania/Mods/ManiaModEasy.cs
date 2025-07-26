// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Mania.Mods
{
    public class ManiaModEasy : ModEasyWithExtraLives
    {
        public override double ScoreMultiplier => 0.5;
        public override string Name => "简单";
        public override LocalisableString Description => @"降低总体难度,HP掉的更慢,更低的准度要求";
    }
}

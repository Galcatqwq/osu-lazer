// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModEasy : ModEasyWithExtraLives
    {
        public override string Name => "Easy你妈";

        public override LocalisableString Description => @"更大的圆圈(喘气),更少的扣血(吐血),更低的准确率要求(黑视),并且拥有额外生命!(全部木大!)";
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModSingleTap : InputBlockingMod
    {
        public override string Name => @"恒定";
        public override string Acronym => @"SG";
        public override LocalisableString Description => @"你只能使用第一次点击时的按键!";
        public override IconUsage? Icon => FontAwesome.Solid.Keyboard;
        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(OsuModAlternate) }).ToArray();

        protected override bool CheckValidNewAction(OsuAction action) => LastAcceptedAction == null || LastAcceptedAction == action;
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace osu.Game.Localisation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public enum Language
    {
        [Description(@"简体中文")]
        zh,

        [Description(@"English")]
        en,

        [Description(@"日本語")]
        ja,

        // Traditional Chinese (Hong Kong) is listed in web sources but has no associated localisations,
        // and was wrongly falling back to Simplified Chinese.
        // Can be revisited if localisations ever arrive.
        // [Description(@"繁體中文（香港）")]
        // zh_hk,
        [Description(@"繁體中文（台灣）")]
        zh_hant,

        [Description(@"Русский")]
        ru,
    }
}

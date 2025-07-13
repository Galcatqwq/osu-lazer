// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Localisation;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Localisation;
using osu.Game.Utils;
using osuTK;

namespace osu.Game.Overlays.Mods
{
    /// <summary>
    /// On the mod select overlay, this provides a local updating view of the aggregate score multiplier coming from mods.
    /// </summary>
    public partial class RankingInformationDisplay : ModFooterInformationDisplay
    {
        public const float HEIGHT = 42;

        public Bindable<double> ModMultiplier = new BindableDouble(1);

        private readonly BindableWithCurrent<double> current = new BindableWithCurrent<double>();

        private const float transition_duration = 200;

        private RollingCounter<double> counter = null!;

        private Box flashLayer = null!;

        [Resolved]
        private OsuColour colours { get; set; } = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            // You would think that we could add this to `Content`, but borders don't mix well
            // with additive blending children elements.
            AddInternal(new Container
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                RelativeSizeAxes = Axes.Both,
                Shear = new Vector2(OsuGame.SHEAR, 0),
                CornerRadius = ShearedButton.CORNER_RADIUS,
                Masking = true,
                Children = new Drawable[]
                {
                    flashLayer = new Box
                    {
                        Alpha = 0,
                        Blending = BlendingParameters.Additive,
                        RelativeSizeAxes = Axes.Both,
                    }
                }
            });

            LeftContent.AddRange(new Drawable[]
            {
                new Container
                {
                    Width = 50,
                    RelativeSizeAxes = Axes.Y,
                    Margin = new MarginPadding(10),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });

            RightContent.Add(new Container
            {
                Width = 40,
                RelativeSizeAxes = Axes.Y,
                Margin = new MarginPadding(10),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Child = counter = new EffectCounter
                {
                    Shear = new Vector2(-OsuGame.SHEAR, 0),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Current = { BindTarget = ModMultiplier }
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ModMultiplier.BindValueChanged(e =>
            {
                if (e.NewValue > ModMultiplier.Default)
                {
                    // 倍率上升 -> 绿色
                    counter.FadeColour(colours.Lime1, transition_duration, Easing.OutQuint);
                }
                else if (e.NewValue < ModMultiplier.Default)
                {
                    // 倍率下降 -> 红色
                    counter.FadeColour(colours.Red1, transition_duration, Easing.OutQuint);
                }
                else
                {
                    counter.FadeColour(Colour4.White, transition_duration, Easing.OutQuint);
                }

                flash();

                const float move_amount = 4;
                if (e.NewValue > e.OldValue)
                    counter.MoveToY(Math.Max(-move_amount * 2, counter.Y - move_amount)).Then().MoveToY(0, transition_duration * 2, Easing.OutQuint);
                else
                    counter.MoveToY(Math.Min(move_amount * 2, counter.Y + move_amount)).Then().MoveToY(0, transition_duration * 2, Easing.OutQuint);
            }, true);

            // required to prevent the counter initially rolling up from 0 to 1
            // due to `Current.Value` having a nonstandard default value of 1.
            counter.SetCountWithoutRolling(ModMultiplier.Value);
        }

        private void flash()
        {
            flashLayer
                .FadeOutFromOne()
                .FadeTo(0.15f, 60, Easing.OutQuint)
                .Then().FadeOut(500, Easing.OutQuint);
        }

        private partial class TextWithTooltip : OsuSpriteText, IHasTooltip
        {
            public LocalisableString TooltipText { get; set; }
        }

        private partial class EffectCounter : RollingCounter<double>, IHasTooltip
        {
            protected override double RollingDuration => 250;

            protected override LocalisableString FormatCount(double count) => ModUtils.FormatScoreMultiplier(count);

            protected override OsuSpriteText CreateSpriteText() => new OsuSpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Font = OsuFont.Default.With(size: 17, weight: FontWeight.SemiBold)
            };

            public LocalisableString TooltipText => ModSelectOverlayStrings.ScoreMultiplier;
        }
    }
}

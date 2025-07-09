// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.StateChanges;
using osu.Framework.Localisation;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Replays;
using osu.Game.Rulesets.Osu.UI;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModAutopilot : Mod, IUpdatableByPlayfield, IApplicableToDrawableRuleset<OsuHitObject>
    {
        public override string Name => "自动移动";
        public override string Acronym => "AP";
        public override IconUsage? Icon => OsuIcon.ModAutopilot;
        public override ModType Type => ModType.Automation;
        public override LocalisableString Description => @"跟着节奏点就好，光标会自动移动.";
        public override double ScoreMultiplier => 0;

        public override Type[] IncompatibleMods => new[]
        {
            typeof(OsuModSpunOut),
            typeof(ModAutoplay),
            typeof(OsuModMagnetised),
            typeof(ModTouchDevice)
        };

        private OsuInputManager inputManager = null!;

        private List<OsuReplayFrame> replayFrames = new List<OsuReplayFrame>(1000);

        public int CurrentFrame = -1;

        public void Update(Playfield playfield)
        {
            if (replayFrames.Count == 0) return;

            double time = playfield.Clock.CurrentTime;

            // 找到当前时间对应的帧区间（binary search 优化性能）
            int targetFrame = findFrameIndexForTime(time);
            if (targetFrame < 0) return;

            // 插值计算当前位置（如果介于两帧之间）
            Vector2 position;

            if (targetFrame < replayFrames.Count - 1 && time > replayFrames[targetFrame].Time)
            {
                double t = (time - replayFrames[targetFrame].Time) / (replayFrames[targetFrame + 1].Time - replayFrames[targetFrame].Time);
                position = Vector2.Lerp(replayFrames[targetFrame].Position, replayFrames[targetFrame + 1].Position, (float)t);
            }
            else
            {
                position = replayFrames[targetFrame].Position;
            }

            // 应用光标位置
            new MousePositionAbsoluteInput { Position = playfield.ToScreenSpace(position) }.Apply(inputManager.CurrentState, inputManager);
        }

        private int findFrameIndexForTime(double time)
        {
            // 使用二分查找优化性能（replayFrames 已按时间排序）
            int low = 0, high = replayFrames.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (replayFrames[mid].Time < time)
                    low = mid + 1;
                else if (replayFrames[mid].Time > time)
                    high = mid - 1;
                else
                    return mid;
            }

            return high; // 返回最后一个小于 time 的帧
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            // Grab the input manager to disable the user's cursor, and for future use
            inputManager = ((DrawableOsuRuleset)drawableRuleset).KeyBindingInputManager;

            inputManager.AllowUserCursorMovement = false;

            // Generate the replay frames the cursor should follow
            replayFrames = new OsuAutoGenerator(drawableRuleset.Beatmap, drawableRuleset.Mods).Generate().Frames.Cast<OsuReplayFrame>().ToList();
        }
    }
}

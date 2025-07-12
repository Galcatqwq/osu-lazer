// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace osu.Game.Benchmarks
{
    public class BenchmarkHitObject : BenchmarkTest
    {
        [Params(1, 100, 1000)]
        public int Count { get; set; }

        [Params(false, true)]
        public bool WithBindableAccess { get; set; }
    }
}

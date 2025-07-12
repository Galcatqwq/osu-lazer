// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace osu.Game.Benchmarks
{
    public class BenchmarkMod : BenchmarkTest
    {
        [Params(1, 10, 100)]
        public int Times { get; set; }

        [Benchmark]
        public int ModHashCode()
        {
            var hashCode = new HashCode();

            return hashCode.ToHashCode();
        }
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using BenchmarkDotNet.Attributes;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.Formats;
using osu.Game.IO;

namespace osu.Game.Benchmarks
{
    public class BenchmarkBeatmapParsing : BenchmarkTest
    {
        private readonly MemoryStream beatmapStream = new MemoryStream();

        [Benchmark]
        public Beatmap BenchmarkBundledBeatmap()
        {
            beatmapStream.Seek(0, SeekOrigin.Begin);
            var reader = new LineBufferedReader(beatmapStream); // no disposal

            var decoder = Decoder.GetDecoder<Beatmap>(reader);
            return decoder.Decode(reader);
        }
    }
}

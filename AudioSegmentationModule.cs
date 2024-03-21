using System;

namespace AudioTranscriptionAnalysis
{
    public class AudioSegmentationModule
    {
        private const int SegmentDurationMs = 30000; // 30 seconds

        public byte[][] SegmentAudio(byte[] audioData, int sampleRate, int bytesPerSample)
        {
            long segmentSizeLong = (long)SegmentDurationMs * sampleRate * bytesPerSample / 1000;

            if (segmentSizeLong > int.MaxValue)
            {
                throw new ArgumentException("Segment size calculation exceeds maximum allowed size.", nameof(segmentSizeLong));
            }

            int segmentSize = (int)segmentSizeLong;

            int startIdx = Math.Max(audioData.Length - segmentSize, 0);

            int lastSegmentSize = audioData.Length - startIdx;

            byte[] lastSegment = new byte[lastSegmentSize];
            Array.Copy(audioData, startIdx, lastSegment, 0, lastSegmentSize);

            LoggingModule.LogInfo("Last 30 seconds (or less) of audio segmented.");

            return new byte[][] { lastSegment };
        }
    }
}

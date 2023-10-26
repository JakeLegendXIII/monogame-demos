using System.Collections.Generic;

namespace Engine2DPipelineExtensions
{
    public class AnimationData
    {
        public int AnimationSpeed { get; set; }
        public bool IsLooping { get; set; }
        public List<AnimationFrameData> Frames { get; set; }
    }

    public class AnimationFrameData
    {
        public int X;
        public int Y;
        public int CellWidth;
        public int CellHeight;
    }
}

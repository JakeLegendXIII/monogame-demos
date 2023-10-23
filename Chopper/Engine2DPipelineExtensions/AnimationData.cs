using System.Collections.Generic;

namespace Engine2DPipelineExtensions
{
    public class AnimationData
    {
        public int AnimationSpeed { get; set; }
        public bool IsLooping { get; set; }
        public List<AnimationDataFrame> Frames { get; set; }
    }

    public class AnimationDataFrame
    {
        public int X;
        public int Y;
        public int CellWidth;
        public int CellHeight;
    }
}

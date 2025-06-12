using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Graphics;

public class Animation
{
    /// <summary>
    /// The texture regions that make up the frames of this animation.  The order of the regions within the collection
    /// are the order that the frames should be displayed in.
    /// </summary>
    public List<TextureRegion> Frames { get; set; }

    /// <summary>
    /// The amount of time to delay between each frame before moving to the next frame for this animation.
    /// </summary>
    public TimeSpan Delay { get; set; }

}

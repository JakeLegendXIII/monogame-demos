using Chopper.Engine.States;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Chopper.Levels
{
    public class LevelReader
    {
        private int _viewportWidth;
        private ContentManager _content;

        public LevelReader(ContentManager content, int viewportWidth)
        {
            _viewportWidth = viewportWidth;
            _content = content;
        }

        public ChopperPipelineExtensions.Level LoadLevel(int nb)
        {
            var levelAssetName = $"Levels/Level{nb}";
            var level = _content.Load<ChopperPipelineExtensions.Level>(levelAssetName);
            level.ComputePositions(_viewportWidth);

            return level;
        }
    }
}

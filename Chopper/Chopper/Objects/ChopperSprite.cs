using Chopper.Engine.Objects;
using Chopper.Engine.States;
using Chopper.States.GamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Chopper.Objects
{
    public class ChopperSprite : BaseGameObject
    {
        private const float Speed = 4.0f;
        private const float BladeSpeed = 0.2f;

        // which chopper do we want from the texture
        private const int ChopperStartX = 0;
        private const int ChopperStartY = 0;
        private const int ChopperWidth = 44;
        private const int ChopperHeight = 98;

        // where are the blades on the texture
        private const int BladesStartX = 133;
        private const int BladesStartY = 98;
        private const int BladesWidth = 94;
        private const int BladesHeight = 94;

        // rotation center of the blades
        private const float BladesCenterX = 47.5f;
        private const float BladesCenterY = 47.5f;

        // positioning of the blades on the chopper
        private const int ChopperBladePosX = ChopperWidth / 2;
        private const int ChopperBladePosY = 34;

        // initial direction and speed of chopper
        private float _angle = 0.0f;
        private Vector2 _direction = new Vector2(0, 0);

        // track life total and age of chopper
        private int _age = 0;
        private int _life = 40;

        // chopper will flash red when hit
        private int _hitAt = 0;

        // bounding box. Note that since this chopper is rotated 180 degrees around its 0,0 origin, 
        // this causes the bounding box to be further to the left and higher than the original texture coordinates
        private int BBPosX = -16;
        private int BBPosY = -63;
        private int BBWidth = 34;
        private int BBHeight = 98;

        private List<(int, Vector2)> _path;

        public ChopperSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public override void OnNotify(BaseGameStateEvent gameEvent)
        {
            switch (gameEvent)
            {
                case GameplayEvents.ChopperHitBy m:
                    JustHit(m.HitBy);
                    SendEvent(new GameplayEvents.EnemyLostLife(_life));
                    break;
            }
        }


        private void JustHit(IGameObjectWithDamage o)
        {
            _hitAt = 0;
            _life -= o.Damage;
        }
    }
}

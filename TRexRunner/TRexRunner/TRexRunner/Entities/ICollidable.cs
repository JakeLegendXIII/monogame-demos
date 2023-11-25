using Microsoft.Xna.Framework;

namespace TRexRunner.Entities
{
	public interface ICollidable
	{
		Rectangle CollisionBox { get; }
	}
}

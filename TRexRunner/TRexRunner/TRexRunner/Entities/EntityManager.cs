using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TRexRunner.Entities
{
	public class EntityManager
	{
		private readonly List<IGameEntity> _entities = new List<IGameEntity>();

		private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>();
		private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>();

		public void Update(GameTime gameTime)
		{
			foreach (var entity in _entities)
			{
				entity.Update(gameTime);
			}

			foreach (var entity in _entitiesToAdd)
			{
				_entities.Add(entity);
			}

			foreach(var entity in _entitiesToRemove)
			{
				_entities.Remove(entity);
			}

			_entitiesToAdd.Clear();
			_entitiesToRemove.Clear();
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach (var entity in _entities.OrderBy(e => e.DrawOrder))
			{
				entity.Draw(spriteBatch, gameTime);
			}
		}

		public void AddEntity(IGameEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity), "Null cannot be added to list of entities");
			}

			_entitiesToAdd.Add(entity);
		}

		public void RemoveEntity(IGameEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity), "Null cannot be removed from list of entities");
			}

			_entitiesToRemove.Add(entity);
		}

		public void Clear()
		{
			_entities.Clear();
			_entitiesToAdd.Clear();
			_entitiesToRemove.Clear();
		}
	}
}

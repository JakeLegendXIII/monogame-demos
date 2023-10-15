using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Chopper.Engine.Objects
{
    public class GameObjectPool<T> where T : BaseGameObject
    {
        private LinkedList<T> _activePool = new LinkedList<T>();
        private LinkedList<T> _inactivePool = new LinkedList<T>();

        public List<T> ActiveObjects
        {
            get
            {
                var list = new List<T>();
                foreach (var item in _activePool)
                {
                    list.Add(item);
                }

                return list;
            }
        }

        public GameObjectPool()
        {

        }

        public T GetOrCreate(Func<T> createNumberObjectFn)
        {
            T activatedObject;

            if (_inactivePool.Count > 0)
            {
                var gameObject = _inactivePool.First.Value;
                gameObject.Initialize();
                gameObject.Activate();
                activatedObject = gameObject;

                _activePool.AddLast(gameObject);
                _inactivePool.RemoveFirst();
            }
            else
            {
                var gameObject = createNumberObjectFn();
                gameObject.Activate();
                activatedObject = gameObject;

                _activePool.AddLast(gameObject);
            }

            return activatedObject;
        }
    }
}

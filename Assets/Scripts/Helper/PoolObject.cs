using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Ig.Model;
using UnityEngine;

namespace Ig.Helper
{
    public class PoolObject<T> where T : BaseModel
    {
        private readonly ConcurrentBag<T> _bag;
        private readonly Func<T> _func;

        public PoolObject(Func<T> func)
        {
            if (func == null)
            {
                return;
            }
            _bag = new ConcurrentBag<T>();
            _func = func;
        }

        public T GetObject()
        {
            return _bag.TryTake(out var item) ? item : _func();
        }
        
        public void PutObject(T item)
        {
            item.Position = Main.Instance.BasePoint.Position;
            _bag.Add(item);
        }

        public void PutObjects(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.Position = Main.Instance.BasePoint.Position;
                _bag.Add(item);
            }
        }
    }
}
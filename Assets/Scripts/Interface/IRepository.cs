using System;
using System.Collections.Generic;
using Ig.Model;

namespace Ig.Interface
{
    public interface IRepository<T> : IDisposable
    {
        int Create(T item);
        T Retrieve(int id);
        bool CheckExist();
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
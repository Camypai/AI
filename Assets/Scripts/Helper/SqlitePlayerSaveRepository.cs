using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ig.Helpers.Db;
using Ig.Interface;
using Ig.Model;

namespace Ig.Helpers
{
    public class SqlitePlayerSaveRepository : IRepository<PlayerSave>
    {
        private SaveContext _db = new SaveContext();
        private bool disposed = false;
        
        public int Create(PlayerSave item)
        {
            var result = _db.PlayerSave.Add(item);
            return result.Id;
        }

        public PlayerSave Retrieve(int id)
        {
            var item = _db.PlayerSave.Find(id);
            return item;
        }

        public bool CheckExist()
        {
            return _db.PlayerSave.Any();
        }

        public void Update(PlayerSave item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = _db.PlayerSave.Find(id);
            if (item != null)
            {
                _db.PlayerSave.Remove(item);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        
        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
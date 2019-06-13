using System;
using System.IO;
using Ig.Helpers.Db;
using Ig.Interface;
using Newtonsoft.Json;
using UnityEngine;

namespace Ig.Helpers
{
    public class JsonPlayerSaveRepository : IRepository<PlayerSave>
    {
        private string _path;

        public JsonPlayerSaveRepository(string path)
        {
            _path = path;
        }

        public int Create(PlayerSave item)
        {
            var data = JsonConvert.SerializeObject(item);
            Debug.Log($"Data: {data}");
            Debug.Log($"Item: {item.Position.X}");
            Debug.Log($"Item: {item.Rotate.Y}");
            File.WriteAllText(_path, data);
            return item.Id;
        }

        public PlayerSave Retrieve(int id)
        {
            string resultFromFile;
            using (var sr = new StreamReader(_path))
            {
                resultFromFile = sr.ReadToEnd();
            }
            var data = JsonConvert.DeserializeObject<PlayerSave>(resultFromFile);
            return data.Id == id ? data : null;
        }

        public bool CheckExist()
        {
            if (!File.Exists(_path)) return false;
            
            string resultFromFile;
            using (var sr = new StreamReader(_path))
            {
                resultFromFile = sr.ReadToEnd();
            }
            var data = JsonConvert.DeserializeObject<PlayerSave>(resultFromFile);
            
            Debug.Log(data);
            return data != null;
        }

        public void Update(PlayerSave item)
        {
            string resultFromFile;
            using (var sr = new StreamReader(_path))
            {
                resultFromFile = sr.ReadToEnd();
            }
            var data = JsonConvert.DeserializeObject<PlayerSave>(resultFromFile);

            if (data == null) return;

            data = item;
            var wData = JsonConvert.SerializeObject(data);
            File.WriteAllText(_path, wData);
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
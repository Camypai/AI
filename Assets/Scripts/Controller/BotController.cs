using System.Collections.Generic;
using Ig.Enum;
using Ig.Interface;
using Ig.Model;
using UnityEngine;

namespace Ig.Controller
{
    public class BotController : BaseController, IUpdate
    {
        private readonly List<BotModel> _botModels = new List<BotModel>();

        public override void On()
        {
            base.On();
            var count = Random.Range(2, 6);
            for (var i = 0; i < count; i++)
            {
                _botModels.Add(Main.Instance.Bots.GetObject());
            }
        }

        public void Update()
        {
            if (!Enabled) return;
            foreach (var botModel in _botModels)
            {
                botModel.Position = new Vector3(botModel.Position.x, 0, botModel.Position.z);
                botModel.Do();
            }
        }
    }
}
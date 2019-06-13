using System.Collections.Generic;
using Ig.Controller;
using Ig.Helpers;
using Ig.Interface;
using Ig.Model;
using UnityEngine;

namespace Ig
{
    public class Main : MonoBehaviour
    {
        [SerializeField] public Transform Spawn;
        [SerializeField] public BotModel BotModel;
        public PoolObject<BotModel> Bots { get; private set; }
//        public SqlitePlayerSaveRepository Db;

        public static Main Instance { get; private set; }

        private readonly List<IUpdate> _updates = new List<IUpdate>();
        private PlayerController _aiController;
        private BotController _botController;
        private CameraController _cameraController;

        private void Awake()
        {
            Instance = this;
            
//            Db = new SqlitePlayerSaveRepository();

            _aiController = new PlayerController();
            _botController = new BotController();
            _cameraController = new CameraController();
            Bots = new PoolObject<BotModel>(Spawn.position,
                () => Instantiate(BotModel, Helper.GenericPoint(Spawn), Quaternion.identity));

            _updates.AddRange(new IUpdate[] {_aiController, _botController, _cameraController});
        }

        private void Start()
        {
            _aiController.On();
            _botController.On();
            _cameraController.On();
        }

        private void Update()
        {
            foreach (var update in _updates)
            {
                update.Update();
            }
        }
    }
}
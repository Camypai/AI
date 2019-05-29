using System;
using System.Collections.Generic;
using Ig.Controller;
using Ig.Helper;
using Ig.Interface;
using Ig.Model;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Ig
{
    public class Main : MonoBehaviour
    {
        [SerializeField] public PointModel BasePoint;

        public PointController PointController { get; private set; }
        public PoolObject<PointModel> PoolObject;
        
        public static Main Instance { get; private set; }

        private readonly List<IUpdate> _updates = new List<IUpdate>();
        private AiController _aiController { get; set; }
        
        private void Awake()
        {
            Instance = this;
            var character = FindObjectOfType(typeof(AICharacterControl)) as AICharacterControl;
            
            _aiController = new AiController(character);
            PointController = new PointController();
            PoolObject = new PoolObject<PointModel>(() => Instantiate(BasePoint, BasePoint.Position, BasePoint.Rotation));
            
            _updates.AddRange(new IUpdate[]{PointController, _aiController});
        }

        private void Start()
        {
            PointController.On();
            _aiController.On();
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
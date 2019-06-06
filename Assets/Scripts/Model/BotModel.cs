using System;
using System.Collections.Generic;
using System.Linq;
using Ig.Enum;
using UnityEngine.AI;
using Ig.Helpers;
using Ig.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ig.Model
{
    public class BotModel : BaseModel, ISetDamage
    {
        public BotState _state = BotState.Empty;
        private float _hp = 100;
        private float _cooldown = 4;
        private List<PlayerModel> _targets;
        private PlayerModel _activeTarget;

        private WeaponModel _weaponModel;

//        private readonly Vector3 _deathScale = new Vector3(0.1f, 0.1f, 0.1f);
        private float step = 1f;

        private NavMeshAgent _navMeshAgent;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _weaponModel = GetComponentInChildren<WeaponModel>();
            _targets = new List<PlayerModel>(FindObjectsOfType<PlayerModel>());
        }

        public void Do()
        {
            if (_state == BotState.Death) return;


            var point = Helper.GenericPoint(transform);
            switch (_state)
            {
                case BotState.Empty:
                case BotState.Patrol:

                    if (!_navMeshAgent.hasPath)
                    {
                        _navMeshAgent.SetDestination(point);
                        _navMeshAgent.stoppingDistance = 1;
                    }
                    else
                    {
                        if (Vector3.Distance(point, transform.position) <= 1)
                        {
                            _state = BotState.Observation;
                            Invoke(nameof(ReadyPatrol), _cooldown);
                        }
                    }

                    break;
                case BotState.Observation:
                    // TODO: Сделать поворот головы
                    break;
                case BotState.Aggression:
                    _navMeshAgent.SetDestination(_activeTarget.Position);
                    _navMeshAgent.stoppingDistance = 6;
                    _weaponModel.Fire();
                    break;
                case BotState.Lose:
                    _navMeshAgent.SetDestination(point);
                    _navMeshAgent.stoppingDistance = 1;
                    _state = BotState.Patrol;
                    break;
                case BotState.Death:
                    CancelInvoke();
                    return;
            }

            if (_targets.Any(target => Helper.SeeTarget(transform, target.transform)))
            {
                _activeTarget = _targets.First(target => Helper.SeeTarget(transform, target.transform));
                _state = BotState.Aggression;
                CancelInvoke();
            }
            else if (_state == BotState.Aggression)
            {
                _state = BotState.Lose;
            }
        }

        public void SetDamage(float damage)
        {
            if (_hp > 0)
            {
                _hp -= damage;
            }

            if (_hp <= 0)
            {
                _state = BotState.Death;
            }
        }

        private void ReadyPatrol()
        {
            _state = BotState.Patrol;
        }
    }
}
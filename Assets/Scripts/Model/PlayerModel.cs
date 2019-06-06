using Ig.Helpers;
using UnityEngine;
using UnityEngine.AI;

namespace Ig.Model
{
    public class PlayerModel : BaseModel
    {
        private NavMeshAgent _navMeshAgent;

        private WeaponModel _weaponModel;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _weaponModel = GetComponentInChildren<WeaponModel>();
        }

        public void Move(Vector3 point)
        {
            _navMeshAgent.SetDestination(point);
            _navMeshAgent.stoppingDistance = 0;
        }

        public void Attack(Vector3 target)
        {
            var rp = target - Position;
            Rotation = Quaternion.LookRotation(rp);
            _weaponModel.Rotation = Rotation;
            _weaponModel.Fire();
            _navMeshAgent.SetDestination(target);
            _navMeshAgent.stoppingDistance = 6;
        }
    }
}
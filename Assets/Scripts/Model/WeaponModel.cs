using System.Collections.Generic;
using Ig.Model;
using UnityEngine;

namespace Ig.Model
{
    public abstract class WeaponModel : BaseModel
    {
        public AmmunitionModel Ammunition;

        protected bool CanFire = true;

        [SerializeField] protected Transform Gun;
        [SerializeField] protected float Force = 500;
        [SerializeField] protected float RechargeTime = 0.2f;

        public abstract void Fire();

        protected void ReadyForShoot()
        {
            CanFire = true;
        }
    }
}
using Ig.Interface;
using UnityEngine;

namespace Ig.Model
{
    public class BulletModel : AmmunitionModel
    {
        private void OnCollisionEnter(Collision other)
        {
            var enemy = other.gameObject.GetComponent<ISetDamage>();
            if (enemy == null) return;

            SetDamage(enemy);
        }

        private void SetDamage(ISetDamage damage)
        {
            damage?.SetDamage(CurrentDamage);
        }
    }
}
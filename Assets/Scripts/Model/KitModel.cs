using System.Linq;
using Ig.Interface;
using UnityEngine;

namespace Ig.Model
{
    public class KitModel : BaseModel
    {
        public float HealthCount;
        
        private void OnCollisionEnter(Collision other)
        {
            var character = other.gameObject.GetComponent<ISetHealth>();
            if (character == null) return;

            SetHealth(character);
        }

        private void SetHealth(ISetHealth character)
        {
            character?.SetHealth(HealthCount);
        }
    }
}
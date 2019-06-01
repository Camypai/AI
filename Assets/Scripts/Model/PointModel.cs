using System.Linq;
using UnityEngine;

namespace Ig.Model
{
    public class PointModel : BaseModel
    {
        private float Radius = 2f;

        [SerializeField] private LayerMask layer;

        public bool CheckCollide()
        {
            var hits = Physics.OverlapSphere(_position, Radius, layer);

            return hits.Any();
        }
    }
}
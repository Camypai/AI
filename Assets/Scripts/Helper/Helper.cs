using Ig.Enum;
using UnityEngine;
using UnityEngine.AI;

namespace Ig.Helpers
{
    public static class Helper
    {
        private const float ActiveDis = 10;
        private const float ActiveAng = 35;

        public static Vector3 GenericPoint(Transform agent)
        {
            var dis = Random.Range(5, 50);
            var randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(agent.position + randomPoint, out var hit, dis, NavMesh.AllAreas);
            var result = hit.position;

            return result;
        }

        public static bool SeeTarget(Transform self, Transform target)
        {
            return Dist(self, target) && Angle(self, target) && !CheckBlocked(self, target);
        }

        private static bool CheckBlocked(Transform self, Transform target)
        {
            return !Physics.Linecast(self.position, target.position, out var hit) || hit.collider.CompareTag("Green");
        }

        private static bool Angle(Transform self, Transform target)
        {
            var angle = Vector3.Angle(self.forward, target.position - self.position);
            return angle <= ActiveAng;
        }

        private static bool Dist(Transform self, Transform target)
        {
            var dist = Vector3.Distance(self.position, target.position);
            return dist <= ActiveDis;
        }
    }
}
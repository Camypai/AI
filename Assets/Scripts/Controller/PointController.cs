using System.Collections.Generic;
using System.Linq;
using Ig.Enum;
using Ig.Interface;
using Ig.Model;
using UnityEngine;

namespace Ig.Controller
{
    public class PointController : BaseController, IUpdate
    {
        private const int MouseButton = (int) Enum.MouseButton.RightButton;

        public PointModel ActivePoint;
        private readonly Queue<PointModel> _pointQueue = new Queue<PointModel>();

        public void Update()
        {
            if (!Enabled) return;

            if (Input.GetMouseButtonUp(MouseButton))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var point = Main.Instance.PoolObject.GetObject();
                    point.Position = hit.point;
                    _pointQueue.Enqueue(point);
                }
            }

            if (_pointQueue.Any() && ActivePoint == null)
            {
                ActivePoint = _pointQueue.Dequeue();
            }

            if (ActivePoint == null || !ActivePoint.CheckCollide()) return;
            
            Main.Instance.PoolObject.PutObject(ActivePoint);
            ActivePoint = null;
        }
    }
}
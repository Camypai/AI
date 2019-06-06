using Ig.Interface;
using Ig.Model;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ig.Controller
{
    public class CameraController : BaseController, IUpdate
    {
        private CameraModel _camera;

        public override void On()
        {
            base.On();
            _camera = Object.FindObjectOfType<CameraModel>();
        }

        public void Update()
        {
            var position = _camera.Target.position;
            var rp = position - _camera.transform.position;
            _camera.Rotation = Quaternion.LookRotation(rp);
        }
    }
}
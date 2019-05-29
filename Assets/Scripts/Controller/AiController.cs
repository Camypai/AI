using Ig.Interface;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Ig.Controller
{
    public class AiController : BaseController, IUpdate
    {
        private readonly AICharacterControl _characterControl;
        
        public AiController(AICharacterControl characterControl)
        {
            _characterControl = characterControl;
        }
        
        public void Update()
        {
            if (!Enabled) return;

            if (Main.Instance.PointController.ActivePoint == null)
            {
                _characterControl.target = null;
                return;
            }

            _characterControl.target = Main.Instance.PointController.ActivePoint.transform;
        }
    }
}
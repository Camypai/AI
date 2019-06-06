using System;
using Ig.Enum;
using Ig.Interface;
using Ig.Model;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Object = UnityEngine.Object;

namespace Ig.Controller
{
    public class PlayerController : BaseController, IUpdate
    {
        private readonly PlayerModel _character;
        private const int MouseButton = (int) Enum.MouseButton.RightButton;
        private Vector3 _targetPosition;
        private GameObject _target;
        private PlayerState _state = PlayerState.Idle;

        public PlayerController()
        {
            _character = Object.FindObjectOfType<PlayerModel>();
        }

        public void Update()
        {
            if (!Enabled) return;

            if (Input.GetMouseButtonUp(MouseButton))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.CompareTag("Red"))
                    {
                        Debug.Log("Red");
                        _targetPosition = hit.rigidbody.position;
                        _target = hit.collider.gameObject;
                        _state = PlayerState.Attack;
                    }
                    else
                    {
                        _targetPosition = hit.point;
                        _state = PlayerState.Move;
                    }
                }
            }

            switch (_state)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Move:
                    _character.Move(_targetPosition);
                    break;
                case PlayerState.Attack:
                    _character.Attack(_target.transform.position);
                    break;
            }
        }
    }
}
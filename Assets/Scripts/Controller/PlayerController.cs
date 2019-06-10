using System;
using System.Data.SQLite;
using Ig.Enum;
using Ig.Helpers;
using Ig.Helpers.Db;
using Ig.Interface;
using Ig.Model;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Object = UnityEngine.Object;
using Vector3 = UnityEngine.Vector3;

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

            if (Input.GetKeyUp(KeyCode.F5))
            {
                if (CheckSaveExist(out var model))
                {
                    using (var db = new SqlitePlayerSaveRepository())
                    {
                        
                    db.Update(_character);
                    }
                }
                else
                {
                    using (var db = new SqlitePlayerSaveRepository())
                    {
                        
                        db.Create(_character);
                    }
                }
            }
            
            if (Input.GetKeyUp(KeyCode.F6))
            {
                if (CheckSaveExist(out var model))
                {
                    _character.Position = model.Position;
                    _character.Rotation = model.Rotate;
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

        private bool CheckSaveExist(out PlayerSave model)
        {
            using (var db = new SqlitePlayerSaveRepository())
            {
                if (db.CheckExist())
                {
                    model = db.Retrieve(_character.Id);
                    return true;
                }
            }
            model = null;
            return false;
        }
    }
}
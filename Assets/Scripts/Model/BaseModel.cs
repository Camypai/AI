using UnityEngine;

namespace Ig.Model
{
    public class BaseModel : MonoBehaviour
    {
        protected int _layer;
        protected Color _color;
        protected Material _material;
        protected Transform _transform;
        protected Vector3 _position;
        protected Quaternion _rotation;
        protected Vector3 _scale;
        protected GameObject _instanceObject;
        protected Rigidbody _rigidbody;
        protected string _name;
        protected bool _isVisible;
        
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;

                if (_instanceObject != null)
                {
                    _instanceObject.layer = _layer;
                }

                if (_instanceObject != null)
                {
                    AskLayer(GetTransform, value);
                }
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                if (_material != null)
                {
                    _material.color = _color;
                }

                AskColor(GetTransform, _color);
            }
        }

        public Material Material => _material;

        public Vector3 Position
        {
            get
            {
                if (InstanceObject != null)
                {
                    _position = GetTransform.position;
                }

                return _position;
            }
            set
            {
                _position = value;
                if (InstanceObject != null)
                {
                    GetTransform.position = _position;
                }
            }
        }

        public Quaternion Rotation
        {
            get
            {
                if (InstanceObject != null)
                {
                    _rotation = GetTransform.rotation;
                }

                return _rotation;
            }
            set
            {
                _rotation = value;
                if (InstanceObject != null)
                {
                    GetTransform.rotation = _rotation;
                }
            }
        }

        public Vector3 Scale
        {
            get
            {
                if (InstanceObject != null)
                {
                    _scale = GetTransform.localScale;
                }

                return _scale;
            }
            set
            {
                _scale = value;
                if (InstanceObject != null)
                {
                    GetTransform.localScale = _scale;
                }
            }
        }

        public GameObject InstanceObject => _instanceObject;

        public Rigidbody Rigidbody => _rigidbody;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                InstanceObject.name = _name;
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                var renderer = GetComponent<Renderer>();
                if (renderer)
                {
                    renderer.enabled = _isVisible;
                }

                if (_transform.childCount <= 0) return;
                foreach (Transform child in _transform)
                {
                    renderer = child.gameObject.GetComponent<Renderer>();
                    if (renderer)
                    {
                        renderer.enabled = _isVisible;
                    }

                    if (child.childCount <= 0) return;
                    foreach (Transform intoChild in child)
                    {
                        renderer = intoChild.gameObject.GetComponent<Renderer>();
                        if (renderer)
                        {
                            renderer.enabled = _isVisible;
                        }
                    }
                }
            }
        }

        public Transform GetTransform => _transform;

        /// <summary>
        /// Применяет слой каждому дочернему объекту и самому себе
        /// </summary>
        /// <param name="obj">Базовый объект</param>
        /// <param name="layer">Слой</param>
        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;

            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }

        /// <summary>
        /// Применяет цвет каждому дочернему объекту и самому себе
        /// </summary>
        /// <param name="obj">Базовый объект</param>
        /// <param name="color">Цвет</param>
        private void AskColor(Transform obj, Color color)
        {
            obj.gameObject.GetComponent<Renderer>().material.color = color;

            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskColor(child, color);
            }
        }

        protected virtual void Awake()
        {
            _instanceObject = gameObject;
            _name = InstanceObject.name;
            if (GetComponent<Renderer>())
            {
                _material = GetComponent<Renderer>().material;
            }

            _rigidbody = InstanceObject.GetComponent<Rigidbody>();
            _transform = InstanceObject.transform;
        }
    }
}
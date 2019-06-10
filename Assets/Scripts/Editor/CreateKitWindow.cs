using Ig.Model;
using UnityEditor;
using UnityEngine;

namespace Ig.Editor
{
    public class CreateKitWindow : EditorWindow
    {
        public static KitModel ObjectInstantiate;
        public string _nameObject = "Kit";
        public bool _groupEnabled;
        public int _countObject = 1;
        public float _radius = 10;
        
        private void OnGUI()
        {
            GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
            ObjectInstantiate =
                EditorGUILayout.ObjectField("Объект который хотим вставить",
                        ObjectInstantiate, typeof(KitModel), true)
                    as KitModel;
            _nameObject = EditorGUILayout.TextField("Имя объекта", _nameObject);
            _groupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки",
                _groupEnabled);
            _countObject = EditorGUILayout.IntSlider("Количество объектов",
                _countObject, 1, 100);
            _radius = EditorGUILayout.Slider("Радиус окружности", _radius, 10, 600);
            EditorGUILayout.EndToggleGroup();
            if (GUILayout.Button("Создать объекты"))
            {
                if (ObjectInstantiate)
                {
                    GameObject root = new GameObject("Kits");
                    for (int i = 0; i < _countObject; i++)
                    {
                        float angle = i * Mathf.PI * 2 / _countObject;
                        Vector3 pos = new Vector3(Mathf.Cos(angle), 0,
                                          Mathf.Sin(angle)) * _radius;
                        KitModel temp = Instantiate(ObjectInstantiate, pos,
                            Quaternion.identity);
                        temp.name = _nameObject + "(" + i + ")";
                        temp.transform.parent = root.transform;
                        temp.transform.position = Random.insideUnitSphere * _radius;
                    }
                }
            }
        }
    }
}
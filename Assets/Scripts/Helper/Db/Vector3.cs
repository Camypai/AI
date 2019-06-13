using UnityEngine;

namespace Ig.Helpers.Db
{
    public class Vector3
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static implicit operator Vector3(UnityEngine.Vector3 position)
        {
            return new Vector3
            {
                X = position.x,
                Y = position.y,
                Z = position.z
            };
        }
        
        public static implicit operator UnityEngine.Vector3(Vector3 position)
        {
            return new UnityEngine.Vector3(position.X, position.Y, position.Z);
        }
        
        public static implicit operator Vector3(Quaternion rotate)
        {
            return new Vector3
            {
                X = rotate.x,
                Y = rotate.y,
                Z = rotate.z
            };
        }
        
        public static implicit operator Quaternion(Vector3 rotate)
        {
            return new Quaternion(rotate.X, rotate.Y, rotate.Z, float.Epsilon);
        }
    }
}
using UnityEngine;
using System.Runtime.Serialization;

namespace LOK1game.Tools
{
    /// <summary>
    /// KFEditor surrogate
    /// </summary>
    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var vector3 = (Vector3)obj;

            info.AddValue("x", vector3.x);
            info.AddValue("y", vector3.y);
            info.AddValue("z", vector3.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var vector3 = (Vector3)obj;

            vector3.x = (float)info.GetValue("x", typeof(float));
            vector3.y = (float)info.GetValue("y", typeof(float));
            vector3.z = (float)info.GetValue("z", typeof(float));

            obj = vector3;

            return obj;
        }
    }
}
using UnityEngine;
using System.Runtime.Serialization;

namespace LOK1game.Tools
{
    /// <summary>
    /// KFEditor surrogate
    /// </summary>
    public class LightSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var light = (Light)obj;

            info.AddValue("intensity", light.intensity);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var light = (Light)obj;

            light.intensity = (float)info.GetValue("intensity", typeof(float));

            obj = light;

            return obj;
        }
    }
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
namespace Utility
{
    public class ListOfVector3JsonCovert : JsonConverter<List<Vector3>>
    {
        public override void WriteJson(JsonWriter writer, List<Vector3> value, JsonSerializer serializer)
        {
            var array = new JArray();
            foreach (var v in value)
            {
                var obj = new JObject();
                obj.Add("x",v.x);
                obj.Add("Y",v.y);
                obj.Add("z",v.z);
                array.Add(obj);
            }
            array.WriteTo(writer);
        }

        public override List<Vector3> ReadJson(JsonReader reader, Type objectType, List<Vector3> existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var list = new List<Vector3>();
            var array = JArray.Load(reader);
            foreach (var obj in array)
            {
                float x = (float)obj["x"];
                float y = (float)obj["y"];
                float z = (float)obj["z"];
                Vector3 v = new Vector3(x, y, z);
                list.Add(v);
            }

            return list;
        }
    }
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Utility
{
    public class Tst : MonoBehaviour
    {
        private void Start()
        {
            var list = new List<Vector3>();
            list.Add(new Vector3(1,1,1));
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ListOfVector3JsonCovert());
            print(JsonConvert.SerializeObject(list, settings));
        }
    }
}
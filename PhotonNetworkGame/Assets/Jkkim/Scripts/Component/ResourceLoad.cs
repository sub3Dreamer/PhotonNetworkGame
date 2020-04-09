using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class ResourceLoad 
    {
        public static T Load<T>(string fileName) where T : Object
        {
            return Resources.Load<T>(fileName);
        }
    }
}
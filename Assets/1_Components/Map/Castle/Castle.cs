using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    public class Castle : MonoBehaviour, IPressObject
    {
        public void OnPress()
        {
            Debug.Log("Castle Pressed");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    public class Castle : MonoBehaviour, IPressObject
    {
        public void OnPress()
        {
            // bilgi paneli, loading
            // zgame.data.get<castle>((data) => { bilgipaneli.setview(castledata) });
            Debug.Log("Castle Pressed");
        }
    }
}

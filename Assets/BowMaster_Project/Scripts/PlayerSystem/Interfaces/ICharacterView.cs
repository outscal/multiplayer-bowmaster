using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public interface ICharacterView
    {
        int GetCharacterID();
        string GetLocalPlayerID();
        Vector2 GetForwardDirection();
    }
}
using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace PlayerSystem
{
    public interface IPlayerView
    {
        int GetCharacterID();
        Vector2 GetForwardDirection();
    }
}
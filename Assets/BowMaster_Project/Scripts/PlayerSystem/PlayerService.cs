using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerService:IPlayerService
    {
        public PlayerService()
        {

        }

        public int GetLocalPlayerID()
        {            
            return 0;
        }
    }
}
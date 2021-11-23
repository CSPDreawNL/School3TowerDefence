using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    public class BehaviorContext : MonoBehaviour
    {
        public Vector3 position;
        public Vector3 velocity;
        public Steeringsettings settings;

        public BehaviorContext(Vector3 pos, Vector3 vel, Steeringsettings sett)
        {
            position = pos;
            velocity = vel;
            settings = sett;
        }
    }
}



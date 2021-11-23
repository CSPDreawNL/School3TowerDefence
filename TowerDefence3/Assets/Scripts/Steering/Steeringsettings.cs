using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [CreateAssetMenu(fileName = "Steering Settings", menuName = "Steering settings", order = 1)]

    public class Steeringsettings : ScriptableObject
    {
        public enum FPM { Forwards, Backwards, PingPong, Random }

        [Header("Steering Settings")]
        public float m_mass = 70.0f; // mass in kg
        public float m_maxDesiredVelocity = 3.0f; // max desired velocity in m/s
        public float m_maxSteeringForce = 3.0f; // max steering "force" in m/s
        public float m_maxSpeed = 3.0f; // max vehicle speed

        [Header("FollowPath")]
        public bool LoopPath = true; // if true loop the selected path
        public FPM m_followPathMode = FPM.Forwards; // options
        public bool loopPath = true; // looping on or off
        public string m_followPathTag = ""; // waypoints tah... waypoints
        public float m_followPathRadius = 2.5f; // circle radius in m

        [Header("Arrive")]
        public float m_arriveDistance = 1.0f; // disctance to object when we reach zero velocity in m
    }
}



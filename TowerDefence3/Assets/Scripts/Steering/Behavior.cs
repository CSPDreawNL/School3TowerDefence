using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.UniversalAssets;

namespace Steering
{
    public abstract class Behavior : IBehavior
    {
        [Header("Behavior Runtime")]
        public Vector3 m_positionTarget = Vector3.zero; // current target position
        public Vector3 m_velocityDesired = Vector3.zero; // desired velocity

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        public virtual void Start(BehaviorContext context)
        {
            m_positionTarget = context.position;
        }

        public abstract Vector3 CalculateSteeringForce(float dt, BehaviorContext context);

        public virtual void OnDrawGizmos(BehaviorContext context)
        {
#if UNITY_EDITOR
            Support.DrawRay(m_positionTarget, m_velocityDesired, Color.yellow);
#endif
        }
    }
}

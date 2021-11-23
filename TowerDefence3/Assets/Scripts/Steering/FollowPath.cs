using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    public class FollowPath : Behavior
    {
        [Header("Private")]
        private List<GameObject> pathPoints;
        private int currentPathPoint = 0;

        public FollowPath(List<GameObject> _pathPoints)
        {
            pathPoints = _pathPoints;
        }

        override public Vector3 CalculateSteeringForce(float dt, BehaviorContext context)
        {
            // check is currentPathPoint
            if (Vector3.Distance(pathPoints[currentPathPoint].transform.position, context.position) < context.settings.m_arriveDistance)
            {
                // check if this is the last pathpoint
                if (currentPathPoint < pathPoints.Count - 1)
                {
                    currentPathPoint++;
                }
                else
                {
                    if (context.settings.loopPath)
                    {
                        currentPathPoint = 0;
                    }
                    else
                    {
                        m_velocityDesired = Vector3.zero;
                        return -context.velocity;
                    }
                }
            }

            m_positionTarget = pathPoints[currentPathPoint].transform.position;
            m_positionTarget.y = context.position.y;

            m_velocityDesired = (m_positionTarget - context.position).normalized * context.settings.m_maxDesiredVelocity;
            return m_velocityDesired - context.velocity;
        }
    }
}

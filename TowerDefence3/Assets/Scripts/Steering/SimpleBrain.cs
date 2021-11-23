using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [RequireComponent(typeof(Steering))]

    public class SimpleBrain : MonoBehaviour
    {
        public enum BehaviorEnum { FollowPath, NotSet }

        [Header("Manual")]
        public BehaviorEnum m_behavior; // the requested behavior

        [Header("Private")]
        private Steering m_steering;

        public List<GameObject> followPathPoints; // the list with pathPoints

        //-----------------------------------------------------------------------------
        //-----------------------------------------------------------------------------

        public SimpleBrain()
        {
            m_behavior = BehaviorEnum.NotSet;
        }

        // Start is called before the first frame update
        void Start()
        {
            // get steering
            m_steering = GetComponent<Steering>();

            List<IBehavior> behaviors = new List<IBehavior>();
            switch (m_behavior)
            {
                case BehaviorEnum.FollowPath:
                    behaviors.Add(new FollowPath(followPathPoints));
                    m_steering.SetBehaviors(behaviors, "FollowPath");
                    break;

                default:
                    Debug.LogError($"Behavior of type {m_behavior} not implemented yet");
                    break;
            }

        }
    }
}


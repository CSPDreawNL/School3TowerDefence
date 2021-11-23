using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    using BehaviorList = List<IBehavior>;

    public class Steering : MonoBehaviour
    {
        [Header("Steering Settings")]
        public string m_label; // label show when running
        public Steeringsettings m_settings; // the steering settings fot all behaviors

        [Header("Steering Runtime")]
        public Vector3 m_position = Vector3.zero; // current position
        public Vector3 m_velocity = Vector3.zero; // current velocty
        public Vector3 m_steering = Vector3.zero; // steering force
        public BehaviorList m_behaviors = new BehaviorList(); // all behaviors for this steering object

        private void Start()
        {
            m_position = transform.position;
        }
        private void FixedUpdate()
        {

            m_steering = Vector3.zero;
            foreach (IBehavior behavior in m_behaviors)
            {
                m_steering += behavior.CalculateSteeringForce(Time.fixedDeltaTime, new BehaviorContext(m_position, m_velocity, m_settings));
            }

            m_steering = Vector3.ClampMagnitude(m_steering, m_settings.m_maxSteeringForce);
            m_steering /= m_settings.m_mass;

            m_velocity = Vector3.ClampMagnitude(m_velocity + m_steering, m_settings.m_maxSpeed);
            m_position += m_velocity * Time.fixedDeltaTime;

            transform.position = m_position;
            transform.LookAt(m_position + Time.fixedDeltaTime * m_velocity);
        }

        public void SetBehaviors(BehaviorList behaviors, string label = "")
        {
            // remember the new settings
            m_label = label;
            m_behaviors = behaviors;

            //sort all behaviors
            foreach (IBehavior behavior in m_behaviors)
            {
                behavior.Start(new BehaviorContext(m_position, m_velocity, m_settings));
            }
        }
    }
}



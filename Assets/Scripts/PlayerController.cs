using UnityEngine;
using UnityEngine.AI;

namespace Techno
{
    public class PlayerController : MonoBehaviour
    {
        #region Inspector
        [SerializeField]
        private Camera m_MainCamera;

        [SerializeField]
        private NavMeshAgent m_Agent;

        [SerializeField]
        private Animator m_Animator;

        [SerializeField]
        private ObservableVariableVector3 m_LastNavigationRequest;
        #endregion

        #region State
        #endregion

        #region Unity Lifecycle Methods
        private void Awake()
        {
            m_Agent.updatePosition = false;
            m_Agent.updateRotation = true;
            m_Animator.applyRootMotion = false;
            m_LastNavigationRequest.RegisterListener(OnNavigationRequest);
        }

        private void OnDestroy()
        {
            m_LastNavigationRequest.UnregisterListener(OnNavigationRequest);
        }

        private void Update()
        {
            // Input
            // if (Input.GetMouseButtonDown(0))
            // {
            //     Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            //     RaycastHit hit;
            //     if (Physics.Raycast(ray, out hit))
            //     {
            //         m_Agent.SetDestination(hit.point);
            //     }
            // }

            // Update Animation
            if (!Mathf.Approximately(0, m_Agent.velocity.magnitude))
            {
                // Calculate normalized speed
                Vector3 velocity = m_Agent.velocity;
                float normalizedMagnitude = velocity.magnitude / m_Agent.speed;

                // // Calculate world delta position
                // Vector3 projectedTarget = Vector3.ProjectOnPlane(
                //     (m_Agent.steeringTarget - transform.position).normalized,
                //     Vector3.up
                // );
                // Vector3 projectedCurrent = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
                // float currentAngle = Vector3.SignedAngle(
                //     Vector3.forward,
                //     projectedCurrent,
                //     Vector3.up
                // );
                // float targetAngle = Vector3.SignedAngle(
                //     Vector3.forward,
                //     projectedTarget,
                //     Vector3.up
                // );
                // float angleDiff = currentAngle - targetAngle;
                // float normalizedAngleDiff = angleDiff / m_Agent.angularSpeed;

                // Update animator
                m_Animator.SetFloat("Forward", normalizedMagnitude);
                // m_Animator.SetFloat("Turn", normalizedAngleDiff);
            }
        }

        private void OnAnimatorMove()
        {
            // Update position to agent position
            transform.position = m_Agent.nextPosition;
        }
        #endregion

        #region Helpers
        private void OnNavigationRequest()
        {
            m_Agent.SetDestination(m_LastNavigationRequest.Value);
        }
        #endregion
    }
}

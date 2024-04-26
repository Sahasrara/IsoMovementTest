using System;
using Cysharp.Threading.Tasks;
using TMPro;
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
        private float m_TurnSpeed = 0.1f;

        [SerializeField]
        private ObservableVariableNavigationRequest m_LastNavigationRequest;
        #endregion

        #region State
        private NavAgentState m_NavState = NavAgentState.Idle;
        private NavigationRequest m_LastDestination;
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

            // Update Agent State
            switch (m_NavState)
            {
                case NavAgentState.Calculating:
                    if (!m_Agent.pathPending)
                    {
                        if (m_Agent.pathStatus == NavMeshPathStatus.PathComplete)
                        {
                            m_NavState = NavAgentState.Running;
                            goto case NavAgentState.Running;
                        }
                        else
                        {
                            // Failed
                            m_NavState = NavAgentState.Idle;
                        }
                    }
                    break;
                case NavAgentState.Running:
                    if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                    {
                        m_Agent.ResetPath();
                        TurnOnArrive().Forget();
                    }
                    break;
            }

            // Update Animation
            if (!Mathf.Approximately(0, m_Agent.velocity.magnitude))
            {
                // Calculate normalized speed
                Vector3 velocity = m_Agent.velocity;
                float normalizedMagnitude = velocity.magnitude / m_Agent.speed;

                // // Calculate world delta position
                // Vector3 projectedTarget = Vector3.ProjectOnPlane(
                //     (m_Agent.steeringTarget - trasform.position).normalized,
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
            m_LastDestination = m_LastNavigationRequest.Value;
            if (m_LastDestination.Position != transform.position)
            {
                m_Agent.SetDestination(m_LastDestination.Position);
                m_NavState = NavAgentState.Calculating;
            }
            else if (
                m_LastDestination.WithRotation
                && m_LastDestination.Rotation != transform.rotation
            )
            {
                TurnOnArrive().Forget();
            }
        }

        private async UniTaskVoid TurnOnArrive()
        {
            if (!m_LastDestination.WithRotation)
                goto BecomeIdle;
            m_NavState = NavAgentState.Turning;
            float timeCount = 0.0f;
            float normalizedProgress;
            Quaternion startRotation = transform.rotation;
            do
            {
                normalizedProgress = timeCount * m_TurnSpeed;
                transform.rotation = Quaternion.Slerp(
                    startRotation,
                    m_LastDestination.Rotation,
                    normalizedProgress
                );
                await UniTask.Yield(PlayerLoopTiming.Update);
                if (m_NavState != NavAgentState.Turning)
                    return;
                timeCount += Time.deltaTime;
            } while (normalizedProgress < 1);

            BecomeIdle:
            m_NavState = NavAgentState.Idle;
            m_LastDestination.SuccessListener?.OnNavigationSuccess();
        }
        #endregion

        private enum NavAgentState
        {
            Idle,
            Calculating,
            Running,
            Turning,
        }
    }
}

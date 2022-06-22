using UnityEngine;
using UnityEngine.AI;

namespace EcsTask.Unity
{
    public class PlayerView : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void SetDestination(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }
    }
}
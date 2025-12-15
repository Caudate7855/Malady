using UnityEngine.AI;

public static class NavMeshAgentExtensions
{
    public static void RebindToNavMesh(this NavMeshAgent agent)
    {
        agent.enabled = false;

        if (NavMesh.SamplePosition(
                agent.transform.position,
                out var hit,
                2f,
                NavMesh.AllAreas))
        {
            agent.transform.position = hit.position;
        }

        agent.enabled = true;
    }
}
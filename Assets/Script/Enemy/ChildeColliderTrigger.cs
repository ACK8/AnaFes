using UnityEngine;

public class ChildeColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    public void HitRaycast(RaycastHit hit)
    {
        enemy.HitRaycast(hit);
    }

    public void HitCutlery(Collision hit)
    {
        enemy.HitCutlery(hit);
    }
}

using UnityEngine;

public class ChildeColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    public void HitRaycast(RaycastHit hit, int damageMagnification)
    {
        enemy.HitRaycast(hit, damageMagnification);
    }

    public void HitCutlery(Collision hit, int damageMagnification)
    {
        enemy.HitCutlery(hit, damageMagnification);
    }
}

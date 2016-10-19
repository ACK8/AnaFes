using UnityEngine;

public class ChildeColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private Zombie zombie;

    public void HitRaycast(RaycastHit hit)
    {
        zombie.HitRaycast(hit);
    }
}

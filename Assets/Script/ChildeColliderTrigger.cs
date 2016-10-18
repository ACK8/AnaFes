using UnityEngine;

public class ChildeColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private Zombie zombie;    

    void OnCollisionEnter(Collision collider)
    {
        zombie.RelayOnTriggerEnter(collider);
    }
}

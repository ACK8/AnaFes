using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private Animator anim;

	void Start () 
	{
        anim = GetComponent<Animator>();
        Destroy(gameObject, 0.2f);
	}
}

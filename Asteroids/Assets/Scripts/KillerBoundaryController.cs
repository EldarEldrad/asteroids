using UnityEngine;

public class KillerBoundaryController : MonoBehaviour
{
	void OnTriggerExit (Collider collider)
    {
        Destroy(collider.gameObject);
	}
}

using UnityEngine;

public class DestroyOnCollision : MonoBehaviour, OnCollisionStrategy
{
    // Start is called before the first frame update
    public void OnCollision(Collider collider){
        Destroy(collider.gameObject);
    }
}

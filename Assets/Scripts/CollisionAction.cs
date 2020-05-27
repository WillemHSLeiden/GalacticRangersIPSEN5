using UnityEngine;
using UnityEngine.Events;

public class CollisionAction : MonoBehaviour{
    [System.Serializable]
    public class hitEvents : UnityEvent { }

    [SerializeField]
    public hitEvents hitEvent;

    public OnCollisionStrategy onCollision;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "laserPrefab") {
            hitEvent.Invoke();
            onCollision.OnCollision(collider);
        }
    }
}

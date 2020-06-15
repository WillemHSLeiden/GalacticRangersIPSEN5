using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SerpentiamBodypart : MonoBehaviour
{
    public SerpentiamBody serpentiamBody = null;

    [System.Serializable]
    public class hitEvents : UnityEvent { }

    [SerializeField]
    public hitEvents neutralEvent, steelEvent, vulnerableEvent;

    public enum BodypartState { NEUTRAL, STEEL, VULNERABLE }

    public BodypartState state = BodypartState.NEUTRAL;

    public Renderer rend;

    [SerializeField]
    private Material flashMat;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Laser") {

            Debug.Log("Hit");

            switch(state) {
                case BodypartState.NEUTRAL:
                    neutralEvent.Invoke();
                    state = BodypartState.STEEL;
                    break;

                case BodypartState.STEEL:
                    steelEvent.Invoke();
                    break;

                case BodypartState.VULNERABLE:
                    vulnerableEvent.Invoke();
                    break;
            }

            Destroy(other.gameObject);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            neutralEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            steelEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            vulnerableEvent.Invoke();
        }
    }

    public void addSerpentiamHit() {
        if (serpentiamBody != null)
            if (state != BodypartState.VULNERABLE)
                serpentiamBody.AddBodyHit();
            else {
                Debug.Log("Damaged");
                serpentiamBody.damage();
            }
        else
            Debug.LogError("Serpentiam not found!");
    }

    public void steelize() {
        state = BodypartState.STEEL;
    }

    public void flashHit(Material mat) {
        rend.material = flashMat;
        StartCoroutine(flashHitReset(mat));
    }

    IEnumerator flashHitReset(Material mat) {
        yield return new WaitForSeconds(0.05f);
        rend.material = mat;
    }

}

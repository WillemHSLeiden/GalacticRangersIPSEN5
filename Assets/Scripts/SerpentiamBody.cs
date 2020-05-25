using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFollower : MonoBehaviour {
    public List<Transform> bodyParts = new List<Transform>();

    [SerializeField]
    private float minDist = 0.25f, speed = 1, rotationSpeed = 50;

    public GameObject bodyPrefab;

    private float dist;
    private Transform currBodyPart;
    private Transform prevBodyPart;

    public void Move() {
        float currSpeed = speed;

        bodyParts[0].Translate(bodyParts[0].forward * currSpeed * Time.deltaTime, Space.World);

        for (int i = 1; i < bodyParts.Count; i++) {
            currBodyPart = bodyParts[i];
            prevBodyPart = bodyParts[i - 1];

            dist = Vector3.Distance(prevBodyPart.position, currBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            float T = Time.deltaTime * dist / minDist * currSpeed;

            if (T > 0.5f)
                T = 0.5f;

            currBodyPart.position = Vector3.Slerp(currBodyPart.position, newPos, T);
            currBodyPart.rotation = Quaternion.Slerp(currBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart() {
        Transform newPart = Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation).transform;

        newPart.SetParent(transform);

        bodyParts.Add(newPart);
    }

}
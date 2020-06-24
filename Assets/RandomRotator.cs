using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    Vector3 randomRotateVector;

    // Start is called before the first frame update
    void Start()
    {
        randomRotateVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randomRotateVector);
    }
}

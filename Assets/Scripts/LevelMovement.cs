using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition = new Vector3(0.0f, 0.0f, 381.3f);
    [SerializeField] private Vector3 endPosition = new Vector3(0.0f, 0.0f, -276f);
    private LevelSpeed levelSpeed;

    private void Start()
    {
        levelSpeed = this.transform.parent.GetComponent<LevelSpeed>();
    }

    void Update()
    {
        PointAToB();
    }

    private void PointAToB()
    {
        RestartStartingPosition();
        ForwardMovement();
    }

    private void ForwardMovement()
    {
        float zSpeed = levelSpeed.getLevelSpeed() * Time.deltaTime;
        Vector3 movement = -Vector3.forward * zSpeed;
        transform.Translate(movement);
    }

    private void RestartStartingPosition()
    {
        if (transform.position.z < endPosition.z)
        {
            transform.position = startPosition;
        }
    }
}

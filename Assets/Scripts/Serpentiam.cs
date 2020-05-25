using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Serpentiam : MonoBehaviour
{

    public enum BossState { STARTUP, EVADING, ATTACKING, DYING }

    private BossState state = BossState.EVADING;

    private FollowPath[] pathFollower = new FollowPath[10];
    private GameObject[] bodyParts = new GameObject[10];

    public PathCreator[] paths;

    public GameObject headObject;
    public GameObject bodyPartObject;

    private Enemy head = new Enemy();

    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        head.speed = 5;
        bodyParts[0] = Instantiate(headObject, transform.position, transform.rotation);
        pathFollower[0] = new FollowPath(bodyParts[0], head, paths[0]);

        float distance = 0;

        for (int i = 1; i < 10; i++) {
            bodyParts[i] = Instantiate(bodyPartObject, transform.position, transform.rotation);

            float size = 0.5f + (float)i / 18.0f;

            bodyParts[i].transform.GetChild(0).localScale = new Vector3(size, size, size);
            pathFollower[i] = new FollowPath(bodyParts[i], head, paths[0], distance - 7);

            distance += size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case BossState.STARTUP:
                break;

            case BossState.EVADING:
                pathFollower[0].followLookAt(cameraTransform);
                for (int i = 1; i < 10; i++) {
                    pathFollower[i].follow();
                    bodyParts[i].transform.GetChild(0).transform.Rotate(transform.up * 200 * Time.deltaTime);
                }

                Debug.Log((int)pathFollower[0].distanceTravelled);

                break;

            case BossState.ATTACKING:

                break;

            case BossState.DYING:
                break;
        }
    }
}

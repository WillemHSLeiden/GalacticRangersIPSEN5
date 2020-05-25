using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Boss : MonoBehaviour
{

    public enum BossState { STARTUP, EVADING, ATTACKING, DYING }

    private BossState state = BossState.EVADING;

    private FollowPath pathFollower;

    public PathCreator[] paths;

    [SerializeField]
    private GameObject bossObject;
    private GameObject _bossObject;

    private Enemy boss = new Enemy();

    public Transform cameraTransform;

    private bool invoked = false;

    // Start is called before the first frame update
    void Start()
    {
        boss.speed = 3;
        _bossObject = Instantiate(bossObject);
        pathFollower = new FollowPath(_bossObject, boss, paths[0]);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case BossState.STARTUP:
                break;

            case BossState.EVADING:
                if (!invoked) {
                    pathFollower = new FollowPath(_bossObject, boss, paths[0]);
                    StartCoroutine(switchState(BossState.ATTACKING, 13f));
                    invoked = true;
                }

                pathFollower.followLookAt(cameraTransform);

                break;

            case BossState.ATTACKING:
                if (!invoked) {
                    pathFollower = new FollowPath(_bossObject, boss, paths[1]);
                    Invoke("fireAsteroid", 5);
                    Invoke("fireAsteroid", 10.1f);

                    StartCoroutine(switchState(BossState.EVADING, 13f));
                    invoked = true;
                }

                pathFollower.followLookAt(cameraTransform);

                break;

            case BossState.DYING:
                break;
        }
    }

    IEnumerator switchState(BossState switchedState, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        state = switchedState;
        invoked = false;
    }
}

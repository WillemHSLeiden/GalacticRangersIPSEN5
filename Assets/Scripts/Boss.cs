using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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

    [System.Serializable]
    public class bossEvent : UnityEvent { }

    [SerializeField]
    public bossEvent evadingEvents, attackingEvents;

    [SerializeField]
    private float evasionPhaseLength, attackPhaseLength;

    private Enemy boss = new Enemy();

    public Transform cameraTransform;

    private bool invoked = false;

    // Start is called before the first frame update
    void Start()
    {
        boss.speed = 5;
        //_bossObject = Instantiate(bossObject);
        pathFollower = new FollowPath(bossObject, boss, paths[0]);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case BossState.STARTUP:
                break;

            case BossState.EVADING:
                if (!invoked) {
                    pathFollower = new FollowPath(bossObject, boss, paths[0]);
                    evadingEvents.Invoke();
                    StartCoroutine(switchState(BossState.ATTACKING, attackPhaseLength));
                    invoked = true;
                }

                pathFollower.followLookAt(cameraTransform);

                break;

            case BossState.ATTACKING:
                if (!invoked) {
                    pathFollower = new FollowPath(bossObject, boss, paths[1]);
                    attackingEvents.Invoke();
                    StartCoroutine(switchState(BossState.EVADING, evasionPhaseLength));
                    invoked = true;
                }

                pathFollower.follow();

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

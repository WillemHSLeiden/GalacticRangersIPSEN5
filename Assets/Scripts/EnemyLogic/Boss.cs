using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using PathCreation;

public class Boss : MonoBehaviour, BehaviourStrategy
{

    public enum BossState { STARTUP, EVADING, ATTACKING, DYING, VULNERABLE }

    public BossState state = BossState.STARTUP;

    private FollowPath pathFollower = new FollowPath();

    public PathCreator[] paths;

    [SerializeField]
    private GameObject bossObject = null;

    [System.Serializable]
    public class bossEvent : UnityEvent { }

    [SerializeField]
    public bossEvent evadingEvents, attackingEvents;

    [SerializeField]
    private float evasionPhaseLength = 10, attackPhaseLength = 10, startUpPhaseLength = 5, vulnerablePhaseLength = 7;

    public Enemy boss = new Enemy();

    public Transform cameraTransform;

    private bool invoked = false;

    // Start is called before the first frame update
    void Start()
    {
        pathFollower.addEnemy(bossObject, boss, paths[0]);
        boss.speed = 5;
        boss.health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case BossState.STARTUP:
                if (!invoked) {
                    pathFollower.addEnemy(bossObject, boss, paths[0]);
                    StartCoroutine(switchState(BossState.EVADING, startUpPhaseLength));
                    invoked = true;
                }

                pathFollower.follow(false, null, EndOfPathInstruction.Loop);
                break;

            case BossState.EVADING:
                if (!invoked) {
                    pathFollower.addEnemy(bossObject, boss, paths[1], 24.5f);
                    evadingEvents.Invoke();
                    StartCoroutine(switchState(BossState.ATTACKING, evasionPhaseLength));
                    boss.speed = 5;
                    invoked = true;
                }

                pathFollower.follow(true, cameraTransform, EndOfPathInstruction.Loop);

                break;

            case BossState.ATTACKING:
                if (!invoked) {
                    pathFollower.addEnemy(bossObject, boss, paths[2]);
                    attackingEvents.Invoke();
                    StartCoroutine(switchState(BossState.EVADING, attackPhaseLength));
                    invoked = true;
                }

                pathFollower.follow(false, null, EndOfPathInstruction.Loop);

                break;

            case BossState.VULNERABLE:
                if (!invoked) {
                    pathFollower.addEnemy(bossObject, boss, paths[3]);
                    StartCoroutine(switchState(BossState.EVADING, vulnerablePhaseLength));
                    boss.speed = 8;
                    invoked = true;
                }

                pathFollower.follow(false, null, EndOfPathInstruction.Loop);

                break;

            case BossState.DYING:
                transform.GetChild(0).Translate(Vector3.forward * 5 * Time.deltaTime);
                break;
        }
    }

    public void switchStateWrapper(BossState switchedState) {
        StopAllCoroutines();
        StartCoroutine(switchState(switchedState, 0));
    }

    IEnumerator switchState(BossState switchedState, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        state = switchedState;
        invoked = false;
    }

    public float getVulnerabilityLength() {
        return this.vulnerablePhaseLength;
    }
}

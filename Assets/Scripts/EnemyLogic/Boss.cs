using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PathCreation;

public class Boss : MonoBehaviour, BehaviourStrategy {
    private float health, damage, speed;
    public enum BossState { STARTUP, EVADING, ATTACKING, DYING, VULNERABLE }

    public BossState state = BossState.STARTUP;

    private FollowPath pathFollower = new FollowPath();

    public PathCreator[] paths;

    [System.Serializable]
    public class bossEvent : UnityEvent { }

    [SerializeField]
    public bossEvent evadingEvents, attackingEvents;

    [SerializeField]
    private float evasionPhaseLength = 10, attackPhaseLength = 10, startUpPhaseLength = 5, vulnerablePhaseLength = 7;

    public Transform cameraTransform;

    private Transform player;

    private bool invoked = false;

    // Start is called before the first frame update
    void Start() {
        //pathFollower.addEnemy(bossObject, null, paths[0]);
        this.setSpeed(5);
        this.setHealth(20);
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case BossState.STARTUP:
                if (!invoked) {
                    pathFollower.addEnemy(gameObject, null, paths[0]);
                    StartCoroutine(switchState(BossState.EVADING, startUpPhaseLength));
                    invoked = true;
                }

                pathFollower.follow(false, null, EndOfPathInstruction.Loop);
                break;

            case BossState.EVADING:
                if (!invoked) {
                    pathFollower.addEnemy(gameObject, null, paths[1], 24.5f);
                    evadingEvents.Invoke();
                    StartCoroutine(switchState(BossState.ATTACKING, evasionPhaseLength));
                    this.setSpeed(5);
                    invoked = true;
                }

                pathFollower.follow(true, cameraTransform, EndOfPathInstruction.Loop);

                break;

            case BossState.ATTACKING:
                if (!invoked) {
                    pathFollower.addEnemy(gameObject, null, paths[2]);
                    attackingEvents.Invoke();
                    StartCoroutine(switchState(BossState.EVADING, attackPhaseLength));
                    invoked = true;
                }

                pathFollower.follow(false, null, EndOfPathInstruction.Loop);

                break;

            case BossState.VULNERABLE:
                if (!invoked) {
                    pathFollower.addEnemy(gameObject, null, paths[3]);
                    StartCoroutine(switchState(BossState.EVADING, vulnerablePhaseLength));
                    this.setSpeed(8);
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

    public void setInActive() { }
    public void setHealth(float health) {
        this.health = health;
    }
    public void setDamage(float damage) {
        this.damage = damage;
    }
    public void setSpeed(float speed) {
        this.speed = speed;
    }
    public void setPlayerObject(Transform player) {
        this.player = player;
    }
    public float getHealth() {
        return this.health;
    }
    public float getDamage() {
        return this.damage;
    }
    public float getSpeed() {
        return this.speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowPath {
    private PathCreator pathCreator;
    public float distanceTravelled;

    private Enemy enemy;

    private GameObject gameobj;
    public FollowPath(GameObject gameobj, Enemy enemy, PathCreator path, float startPos = 0) {
        this.enemy = enemy;
        this.pathCreator = path;
        this.gameobj = gameobj;
        this.distanceTravelled = startPos;
    }
    public void follow() {


        distanceTravelled += this.enemy.speed * Time.deltaTime;
        this.gameobj.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        this.gameobj.transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }

    public void followLookAt(Transform lookAtTarget) {

        distanceTravelled += this.enemy.speed * Time.deltaTime;
        this.gameobj.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        this.gameobj.transform.LookAt(lookAtTarget);
    }
}
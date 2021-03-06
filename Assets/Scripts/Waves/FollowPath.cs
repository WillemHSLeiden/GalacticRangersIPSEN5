﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowPath
{
    private List<PathCreator> pathCreator = new List<PathCreator>();
    private List<float> distanceTravelled = new List<float>();
    public EndOfPathInstruction end = EndOfPathInstruction.Stop;

    private List<Enemy> enemy = new List<Enemy>();

    private List<GameObject> gameobj = new List<GameObject>();

    private List<Vector3> prevPos = new List<Vector3>();
    public FollowPath(){
    }

    public void addEnemy(GameObject gameobj, Enemy enemy, PathCreator path, float startPos = 0){
        this.enemy.Add(enemy);
        this.pathCreator.Add(path);
        this.gameobj.Add(gameobj);
        this.distanceTravelled.Add(startPos);

        prevPos.Add(gameobj.transform.position);

    }
    public void follow(bool lookAt = false, Transform lookAtTarget = null, EndOfPathInstruction instruction = EndOfPathInstruction.Stop){
        for (int i = 0; i < gameobj.Count; i++){
            if(this.gameobj[i] != null){
                this.distanceTravelled[i] += this.gameobj[i].GetComponent<BehaviourStrategy>().getSpeed() * Time.deltaTime;
                this.gameobj[i].transform.position = pathCreator[i].path.GetPointAtDistance(distanceTravelled[i], instruction);
                if (!lookAt)
                    this.gameobj[i].transform.rotation = Quaternion.Lerp(this.gameobj[i].transform.rotation, pathCreator[i].path.GetRotationAtDistance(distanceTravelled[i], instruction), 2f * Time.deltaTime);
                else
                    this.gameobj[i].transform.LookAt(lookAtTarget);

                if (this.gameobj[i].transform.position != this.prevPos[i]) {
                    this.prevPos[i] = this.gameobj[i].transform.position;
                } else {
                    this.gameobj[i] = null;
                }
            }
        }
    }

    // Use in unending loops only
    public void followLerp() {

        for (int i = 0; i < gameobj.Count; i++) {
            if (this.gameobj[i] != null) {
                this.distanceTravelled[i] += this.gameobj[i].GetComponent<BehaviourStrategy>().getSpeed() * Time.deltaTime;
                this.gameobj[i].transform.position = Vector3.Lerp(this.gameobj[i].transform.position, pathCreator[i].path.GetPointAtDistance(distanceTravelled[i]), 0.5f);
                this.gameobj[i].transform.rotation = Quaternion.Lerp(this.gameobj[i].transform.rotation, pathCreator[i].path.GetRotationAtDistance(distanceTravelled[i]), 0.5f);
            }
        }
    }

    public bool pathFinished(int index){

        if(this.gameobj[index] == null){
            return true;
        }else{
            return false;
        }

    }

    public void cleanArrays(){
        distanceTravelled = new List<float>();
        enemy = new List<Enemy>();
        gameobj = new List<GameObject>();
        prevPos = new List<Vector3>();
    }
}

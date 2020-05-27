
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowPath
{
    private PathCreator pathCreator;
    private List<float> distanceTravelled = new List<float>();
    public EndOfPathInstruction end = EndOfPathInstruction.Stop;

    private List<Enemy> enemy = new List<Enemy>();

    private List<GameObject> gameobj = new List<GameObject>();

    private List<Vector3> prevPos = new List<Vector3>();
    public FollowPath(){
    }

    public void addEnemy(GameObject gameobj, Enemy enemy, PathCreator path, float startPos = 0){
        this.enemy.Add(enemy);
        this.pathCreator = path;
        this.gameobj.Add(gameobj);
        this.distanceTravelled.Add(startPos);

        prevPos.Add(gameobj.transform.position);

    }
    public void follow(bool lookAt = false, Transform lookAtTarget = null, EndOfPathInstruction instruction = EndOfPathInstruction.Stop){
        for (int i = 0; i < gameobj.Count; i++){
            if(this.enemy[i] != null){
                this.distanceTravelled[i] += this.enemy[i].speed * Time.deltaTime;
                this.gameobj[i].transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled[i], instruction);
                if (!lookAt)
                    this.gameobj[i].transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled[i], instruction);
                else
                    this.gameobj[i].transform.LookAt(lookAtTarget);

                if (this.gameobj[i].transform.position != this.prevPos[i]) {
                    this.prevPos[i] = this.gameobj[i].transform.position;
                } else {
                    this.enemy[i] = null;
                }
            }
        }
    }

    // Use in unending loops only
    public void followLerp() {

        for (int i = 0; i < gameobj.Count; i++) {
            if (this.enemy[i] != null) {
                this.distanceTravelled[i] += this.enemy[i].speed * Time.deltaTime;
                this.gameobj[i].transform.position = Vector3.Lerp(this.gameobj[i].transform.position, pathCreator.path.GetPointAtDistance(distanceTravelled[i]), 0.5f);
                this.gameobj[i].transform.rotation = Quaternion.Lerp(this.gameobj[i].transform.rotation, pathCreator.path.GetRotationAtDistance(distanceTravelled[i]), 0.5f);
            }
        }
    }

    public bool pathFinished(int index){

        if(this.enemy[index] == null){
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

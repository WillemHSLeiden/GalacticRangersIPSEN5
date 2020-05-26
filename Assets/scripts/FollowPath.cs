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

    public void addEnemy(GameObject gameobj, Enemy enemy, PathCreator path){
        this.enemy.Add(enemy);
        this.pathCreator = path;
        this.gameobj.Add(gameobj);
        this.distanceTravelled.Add(0);

        prevPos.Add(gameobj.transform.position);

    }
    public void follow(){
        for (int i = 0; i < gameobj.Count; i++){
            this.distanceTravelled[i] += this.enemy[i].speed * Time.deltaTime;
            this.gameobj[i].transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled[i], end);
            this.gameobj[i].transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled[i], end);   
            
            if(this.gameobj[i].transform.position == this.prevPos[i]){
                Debug.Log(enemy[i].name+" Heeft niet bewogen");
            }else{
                this.prevPos[i] = this.gameobj[i].transform.position;
            }
        }
    }

    public bool pathFinished(){
        for (int i = 0; i < this.gameobj.Count; i++){
            if(this.pathCreator.path.GetPoint(1) == this.gameobj[i].transform.position){
                return true;
            }else{
                return false;
            }
        }
         return false;
    }
}

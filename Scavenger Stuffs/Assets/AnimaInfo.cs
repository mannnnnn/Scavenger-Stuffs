using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaInfo : MonoBehaviour {

    public string animaName;
    public GameObject animaHolder;
    private GameObject loadedAnima;
    private GameObject readyAnima;
    private int[] stats;
    //0 stamina, 1 strength, 2 agility, 3 luck
    public bool adventuring = false;
    private string[] environments;
    public string currEnvironment = "nowhere"; 
    public int currEnvNum = 0;
	
    public Image envPic;
    public Sprite homePic;
    public Sprite cavePic;
    public Sprite meadowPic;
    public Sprite orbPic;
    public Sprite icePic;

    public Text advText;
    public int adventureLength = 3;
    public List<int> visitedNodes = new List<int>();
    public Text timeText;

	void Start () {
        environments = new string[]{"home", "cave", "meadow", "orb", "ice"};
        currEnvironment = environments[currEnvNum];
        animaName = GetComponent<UserInfo>().currAnima;
        //find the prefab for the starting anima and load it in
        loadedAnima = (GameObject)Resources.Load("Prefabs/" + animaName, typeof(GameObject));
        readyAnima = Instantiate(loadedAnima, new Vector3(0, 1.4f, -1), Quaternion.identity);
        readyAnima.transform.parent = animaHolder.transform;
        if (loadedAnima == null)
        {
            Debug.Log("Prefabs/" + animaName + " was not found!");
        }
        if (adventuring)
        {
            //animaBase.GetComponent<SpriteMeshInstance>().Color = new Color(animaBase.GetComponent<SprinteMeshInstance>().Color.r, animaBase.GetComponent<SprinteMeshInstance>().Color.g, animaBase.GetComponent<SprinteMeshInstance>().Color.b, 0.4f);
        }
	}

    public void adventureEvent(int hour, int min){
        if(!adventuring){
            return;
        }

        string eventText = "An event has occurred in the " + currEnvironment + "! The time is " + hour + ":" + min;
        int rand = Random.Range(0, 3);
        int limit = 100;
        while(visitedNodes.Contains(rand) && limit > 0){
            rand = Random.Range(0, 3);
            limit--;
        }

        if (limit == 0 || adventureLength == visitedNodes.Count){
            advText.text = animaName + " left for an adventure!";
            GetComponent<btnScript>().endAdventure();
            adventuring = false;
            visitedNodes.Clear();
            return;
        } else {
            visitedNodes.Add(rand);
        }

        switch(currEnvNum){
            case(0): eventText = getHomeEvent(rand); break;
            case(1): eventText = getCaveEvent(rand); break;
            case(2):
            case(3):
            case(4): break;
            default: break;
        }
        timeText.text = hour + ":" + min;
        advText.text = eventText;
    }

	// Update is called once per frame
	void Update () {
        currEnvironment = environments[currEnvNum];
        updateEnvironmentPic();
        if (adventuring)
        {
            //animaBase.GetComponent<SpriteMeshInstance>().Color = new Color(animaBase.GetComponent<SprinteMeshInstance>().Color.r, animaBase.GetComponent<SprinteMeshInstance>().Color.g, animaBase.GetComponent<SprinteMeshInstance>().Color.b, 0.4f);
        } else
        {
            //animaBase.GetComponent<SpriteMeshInstance>().Color = new Color(animaBase.GetComponent<SprinteMeshInstance>().Color.r, animaBase.GetComponent<SprinteMeshInstance>().Color.g, animaBase.GetComponent<SprinteMeshInstance>().Color.b, 1f);
        }
    }
    public void moveEnv(){
        if(adventuring){
            Debug.Log("Cannot move environments in the middle of an adventure");
            return;
        }
        currEnvNum++;
        if(currEnvNum > environments.Length-1){
            currEnvNum = 0;
        }
    }

    public void updateEnvironmentPic(){
        switch(currEnvNum){
             case(0): envPic.sprite = homePic; break;
             case(1): envPic.sprite = cavePic; break;
             case(2): envPic.sprite = meadowPic; break;
             case(3): envPic.sprite = orbPic; break;
             case(4): envPic.sprite = icePic; break;
            default: break;
        }
    }

    public string getHomeEvent(int rand){
        switch(rand){
            case(0): return animaName + " relaxes on the porch of the house";
            case(1): return "A sudden sound startles " + animaName + " but it was just a distant storm.";
            case(2): return  animaName + " attempts to knock down the pie on the windowsil.";
            default: return "Just a regular day at home.";
        }
    }

     public string getCaveEvent(int rand){
        switch(rand){
            case(0): return animaName + " listens to the water dripping off the stalagtites";
            case(1): return "The distant sound of bats startles " + animaName + ".";
            case(2): return  animaName + " plays with a rock, rolling it around the ground.";
            default: return "Just a regular day at teh cave.";
        }
    }

}

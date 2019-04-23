using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour {

//Delapouite - Gift Icon
//lorc - Tree Icon

	public List<string> unlockedAnima;
	public string currAnima;
	public AnimaInfo animaInfo;
	public GameSparksPieces gs; 

	// Use this for initialization
	void Start () {
		unlockedAnima.Add("Peach");
		currAnima = unlockedAnima[0];
		unlockedAnima = new List<string>();
		animaInfo = GetComponent<AnimaInfo>();
		animaInfo.reloadAnima();
		
		
	}
	

	// Update is called once per frame
	void Update () {

		if(gs == null && GameObject.FindGameObjectsWithTag("gs").Length>0){
			gs = GameObject.FindGameObjectsWithTag("gs")[0].GetComponent<GameSparksPieces>();
		}
		if(!unlockedAnima.Contains("Peach")){
			unlockedAnima.Add("Peach");
		}
	}

	public void switchAnima(){
		int curr = unlockedAnima.IndexOf(currAnima);
		curr++;
		if(curr >= unlockedAnima.Count){
			curr = 0;
		}
		currAnima = unlockedAnima[curr];
		animaInfo.reloadAnima();
	}

	public void switchToNewAnima(){
		int curr = unlockedAnima.Count-1;
		currAnima = unlockedAnima[curr];
		animaInfo.reloadAnima();
	}

	public void lookForAnima(){
		Debug.Log("Looking!");
		if(gs!=null){
			gs.getAnimas(gs.storedUser);
		} else{
			Debug.Log("Gs is null");
		}
		switchToNewAnima();
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour {

//Delapouite - Gift Icon
//lorc - Tree Icon

	public List<string> unlockedAnima;
	public string currAnima;
	public AnimaInfo animaInfo;

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

}

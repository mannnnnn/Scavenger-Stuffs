using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine.SceneManagement;

public class GameSparksPieces : MonoBehaviour {
	public GameObject controller;
	public string storedUser = "";
	bool registering = true;
	public string showuser;
	public string showpass;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		//
		if(controller == null){
			Debug.Log("This scene does not contain a ram controller");
		}
	}

	public void login(){
		registering = false;
		finalizeLoginOrRegister();
	}
	
	void Update(){
		
		if(controller!=null){
			getAnimas(storedUser);
		}
		
		if(controller == null && GameObject.FindGameObjectsWithTag("GameController").Length>0){
			controller = GameObject.FindGameObjectsWithTag("GameController")[0];
			Debug.Log("Found the ram controller!");
		}
	}

	public void finalizeLoginOrRegister(){
		//move to main
		string usernameHere = GameObject.Find("usernameText").GetComponent<Text>().text;
		string passwordHere = GameObject.Find("passwordText").GetComponent<Text>().text;
		showuser = usernameHere;
		showpass = passwordHere;
		
		if(usernameHere != null && passwordHere != null){
			if(!registering){
				Debug.Log("Logging-in");
				new AuthenticationRequest()
				.SetPassword(passwordHere)
				.SetUserName(usernameHere)
				.Send((response) => {
					Debug.Log("response was sent");
					if (!response.HasErrors) {
					string authToken = response.AuthToken;
					string displayName = response.DisplayName;
					bool? newPlayer = response.NewPlayer;
					GSData scriptData = response.ScriptData;
					AuthenticationResponse._Player switchSummary = response.SwitchSummary;
					string userId = response.UserId;
					Debug.Log(userId + " was logged in");
					storedUser = usernameHere;
					SceneManager.LoadScene(0);
					} else {
						Debug.Log("Something went wrong.");
						//GameObject.Find("errorPanel").GetComponent<CanvasGroup>().alpha = 1f;
						Debug.Log(response.Errors);
					}
				});
			} else{
				Debug.Log("Registering");
				new RegistrationRequest()
				.SetDisplayName(usernameHere)
				.SetPassword(passwordHere)
				.SetUserName(usernameHere)
				.Send((response) => {
					Debug.Log("response was sent");
				if (!response.HasErrors) {
					string authToken = response.AuthToken;
					string displayName = response.DisplayName;
					bool? newPlayer = response.NewPlayer;
					GSData scriptData = response.ScriptData;
					RegistrationResponse._Player switchSummary = response.SwitchSummary;
					string userId = response.UserId;
					storedUser = userId;
					Debug.Log(userId + " was registered");
					storedUser = usernameHere;
					SceneManager.LoadScene(0);
				} else {
					Debug.Log(response.Errors);
					Debug.Log("Something went wrong.");
				}
				});
			}
	}else {
		Debug.Log("Username or password not filled out!");
		return;
		}
	}

	public void addAnima(string animaname){
			//anima added to server under user's name
			new GameSparks.Api.Requests.LogEventRequest().SetEventKey("addAnima")
			.SetEventAttribute("username", storedUser)
			.SetEventAttribute("animaname", animaname)
			.Send((response) => {
					if (!response.HasErrors) {
						Debug.Log("Anima added!");
					} else {
						Debug.Log(response.Errors);
					}
			});	
	}
	
	public void getAnimas(string username){
			//a list if animas the user owns, or a check if one exists
			new GameSparks.Api.Requests.LogEventRequest().SetEventKey("getAnimas")
			.SetEventAttribute("username", username)
			.Send((response) => {
					if (!response.HasErrors) {
						var allAnimas = response.ScriptData.GetGSDataList("collectedAnimas");
						Debug.Log(allAnimas.Count + " Animas found for this user.");
						/*for(int i = 0; i < allAnimas.Count; i++)
							{

								Anima newAni = JsonUtility.FromJson<Anima>(allAnimas[i].JSON);
								Debug.Log(newAni.animaname);
								controller.GetComponent<UserInfo>().unlockedAnima[i] = newAni.animaname;
								
							}*/
					} else {
						Debug.Log("Could not get animas!");
						Debug.Log(response.Errors.ToString());
					}
			});	
	}
}


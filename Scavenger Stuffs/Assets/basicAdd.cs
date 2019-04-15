using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine.SceneManagement;
using System.IO;

public class basicAdd : MonoBehaviour {
    string oldText = "";
    public List<string> users = new List<string>();
    public List<string> animas = new List<string>();
    string path = "\\home\\pi\\userAnimas.txt";

	// Use this for initialization
	void Start () {
        //login the admin
       
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path); 
            
            while(!reader.EndOfStream){
                string thisLine = reader.ReadLine();
                oldText += thisLine;
                users.Add(thisLine.Substring(0,thisLine.IndexOf(",")));
                animas.Add(thisLine.Substring(thisLine.IndexOf(",")+1));
            }
            
            reader.Close();
				Debug.Log("Logging-in");
				new AuthenticationRequest()
				.SetPassword("me")
				.SetUserName("its")
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
					} else {
						Debug.Log("Something went wrong.");
						Debug.Log(response.Errors);
					}
				});
	}

    public void Update(){
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        string newText = reader.ReadToEnd();

        if(newText != oldText){
            reader.Close();
            reader = new StreamReader(path); 
            Debug.Log("Changed!");
            while(!reader.EndOfStream){
                string thisLine = reader.ReadLine();
                oldText += thisLine;
                string newUser = thisLine.Substring(0,thisLine.IndexOf(","));
                string newAnima = thisLine.Substring(thisLine.IndexOf(",")+1);
                if(!users.Contains(newUser) && !users.Contains(newAnima)){
                    users.Add(newUser);
                    animas.Add(newAnima);
                    addAnima(newUser, newAnima);
                }
            }
            oldText = newText;
        }

       reader.Close();
    }



	public void addAnima(string user, string anima){
			//anima added to server under user's name
			new GameSparks.Api.Requests.LogEventRequest().SetEventKey("addAnima")
			.SetEventAttribute("username", user)
			.SetEventAttribute("animaname", anima)
			.Send((response) => {
					if (!response.HasErrors) {
						Debug.Log("Anima added!");
					} else {
						Debug.Log(response.Errors);
					}
			});	
	}
}


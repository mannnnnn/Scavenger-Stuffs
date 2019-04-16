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
using System.Text.RegularExpressions;

public class basicAdd : MonoBehaviour {
    string oldText = "";
    public List<string> users = new List<string>();
    public List<string> animas = new List<string>();
    public Text currUser;
    public Text currAnima;
    string path = "C:\\Users\\Savannah\\AppData\\Local\\lxss\\rootfs\\tmp\\userAnimas.txt";

	// Use this for initialization
	void Start () {
        //login the admin
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path); 
            
            while(!reader.EndOfStream){
                string thisLine = reader.ReadLine();
                oldText += thisLine;
                string tempStore = thisLine.Substring(0,thisLine.IndexOf(","));
                tempStore.Replace(" ", string.Empty);
                tempStore = Regex.Replace(tempStore, @"[^A-Za-z0-9]+", "");
                tempStore = tempStore.Trim();
                users.Add(tempStore);
                currUser.text = tempStore;
                Debug.Log("The temp store is:" + tempStore);
                string anotherTemp = thisLine.Substring(thisLine.IndexOf(",")+1);
                anotherTemp = anotherTemp.Trim();
                currAnima.text = anotherTemp;
                animas.Add(anotherTemp);
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
                newUser = Regex.Replace(newUser, @"[^A-Za-z0-9]+", "");
                string newAnima = thisLine.Substring(thisLine.IndexOf(",")+1);
                newAnima = Regex.Replace(newAnima, @"[^A-Za-z0-9]+", "");
                newUser = newUser.Trim();
                newAnima = newAnima.Trim();
                if(!users.Contains(newUser) && !users.Contains(newAnima)){
                    users.Add(newUser);
                    animas.Add(newAnima);
                    currUser.text = newUser;
                    currAnima.text = newAnima;
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


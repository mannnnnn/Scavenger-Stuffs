using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaInfo : MonoBehaviour {

    public string animaName;
    public GameObject animaHolder;
    private GameObject loadedAnima;
    private GameObject readyAnima;
    public bool adventuring = false;
	
	void Start () {
        //find the prefab for the starting anima and load it in
        loadedAnima = (GameObject)Resources.Load("Prefabs/" + animaName, typeof(GameObject));
        readyAnima = Instantiate(loadedAnima, new Vector3(0, 2, -1), Quaternion.identity);
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
	
	// Update is called once per frame
	void Update () {
        if (adventuring)
        {
            //animaBase.GetComponent<SpriteMeshInstance>().Color = new Color(animaBase.GetComponent<SprinteMeshInstance>().Color.r, animaBase.GetComponent<SprinteMeshInstance>().Color.g, animaBase.GetComponent<SprinteMeshInstance>().Color.b, 0.4f);
        } else
        {
            //animaBase.GetComponent<SpriteMeshInstance>().Color = new Color(animaBase.GetComponent<SprinteMeshInstance>().Color.r, animaBase.GetComponent<SprinteMeshInstance>().Color.g, animaBase.GetComponent<SprinteMeshInstance>().Color.b, 1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnScript : MonoBehaviour {
    public GameObject advButton;
    public GameObject advBox;
    public GameObject animaHolder;
    public int currAnimaNum = 1;
    private GameObject readyAnima;

    public void changeAnima()
    {
        //TODO: change which anima is present
        GameObject[] animas = Resources.LoadAll("Prefabs") as GameObject[];
        for (int i = 0; i<animas.Length; i++)
        {
            //check if anima name matches
        }

        //TODO: for now and a simple test, switch between jody and goldenJody
        if (currAnimaNum == 1)
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject);
            readyAnima = Instantiate(animas[0], new Vector3(0, 2, -1), Quaternion.identity);
            readyAnima.transform.parent = animaHolder.transform;
            currAnimaNum = 0;
        } else
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject);
            readyAnima = Instantiate(animas[1], new Vector3(0, 2, -1), Quaternion.identity);
            readyAnima.transform.parent = animaHolder.transform;
            currAnimaNum = 1;
        }
    }


    public void beginAdventure()
    {
        advButton.SetActive(false);
        advBox.SetActive(true);
        GetComponent<AnimaInfo>().adventuring = true;
    }

    public void endAdventure()
    {
        advButton.SetActive(true);
        advBox.SetActive(false);
    }

    public void exit()
    {
        Application.Quit();
    }
}

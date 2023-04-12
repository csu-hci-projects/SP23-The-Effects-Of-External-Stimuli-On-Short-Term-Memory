using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataToFile : MonoBehaviour
{
    public static DataToFile instance;
    private string path;

    public void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    public void CreateFile(string roundSettings){
        string dateTime = System.DateTime.Now + " ";
        string newDateTime = (dateTime.Replace(" ", "_")).Replace("/", "-");

        string filename = "/PlayerData/" + newDateTime + "_" + roundSettings + ".txt";
        
        path = Application.dataPath + filename;

        Debug.Log(path);

        if(!File.Exists(path)){
            File.WriteAllText(path, "Participant Mode" + roundSettings + "\n");
        }

    }
}

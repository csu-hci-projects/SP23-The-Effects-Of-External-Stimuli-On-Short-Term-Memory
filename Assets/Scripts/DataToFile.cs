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
        string newDateTime = ((dateTime.Replace(" ", "_")).Replace("/", "-")).Replace(":", "-");

        string filename = "/PlayerData/" + newDateTime + "_" + roundSettings + ".txt";
        
        path = Application.dataPath + filename;

        if(!File.Exists(path)){
            File.WriteAllText(path, "Round Settings: " + roundSettings + "\n\nRound Number, Round Settings, Round Score\n");
        }
    }

    public void AppendToFile(int totalRounds, int roundSettings){
        int score = ScoreManager.instance.score;
        string roundStats = totalRounds.ToString() + "," + roundSettings.ToString() + "," + score.ToString() + "\n";
        File.AppendAllText(path, roundStats);
    }
}

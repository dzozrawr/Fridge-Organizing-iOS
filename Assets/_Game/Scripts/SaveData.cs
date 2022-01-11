using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int coinHighScore;
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

}

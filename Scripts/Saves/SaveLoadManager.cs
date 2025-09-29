using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public const string PlayerPrefsKeyName = "SavedGameState";
    public static void SaveData(PlayerSaveData  saveData)
    {
        string json = JsonUtility.ToJson(saveData,true);
        PlayerPrefs.SetString(PlayerPrefsKeyName, json);
        PlayerPrefs.Save();
    }
    public static PlayerSaveData LoadData()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKeyName))
            return null;
        string json = PlayerPrefs.GetString(PlayerPrefsKeyName);
        return JsonUtility.FromJson<PlayerSaveData>(json);
    }
}
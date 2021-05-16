
using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "Common/Setting")]
public class Setting : ScriptableObject
{

    private bool isSound = true;
    public bool IsSound { get { return isSound; } set { isSound = value; } }

    private bool isMusic = true;
    public bool IsMusic { get { return isMusic; } set { isMusic = value; } }

    [SerializeField]
    private string key = "Settings";

    private void OnEnable()
    {
        if (key == string.Empty)
        {
            key = name;
        }

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    private void OnDisable()
    {
        if (key == string.Empty)
        {
            key = name;
        }

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }



}

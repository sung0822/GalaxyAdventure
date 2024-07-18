using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerLevelData", menuName = "Unit Data/Player/PlayerLevelData", order = 1)]
public class PlayerLevelUpData : ScriptableObject
{
    /// <summary> key(string) : 헤더, List<string>: 각 헤더열에 해당하는 데이터들 </summary>
    public Dictionary<string, List<string>> headers { get { return _headers; } set { _headers = value; } }
    [SerializeField] Dictionary<string, List<string>> _headers = new Dictionary<string, List<string>>();
    /// <summary>
    /// key(int) : 순서, string: 헤더
    /// </summary>
    public Dictionary<int, string> headerOrder { get { return _headerOrder; } set { _headerOrder = value; } }
    [SerializeField] private Dictionary<int, string> _headerOrder = new Dictionary<int, string>();
    public List<float> expToLevelUp { get { return _expToLevelUp; } }
    [SerializeField] List<float> _expToLevelUp = new List<float>();
    public int maxLevel { get { Debug.Log("maxLevel:" + _maxLevel); return _maxLevel;  } set { _maxLevel = value; } }
    [SerializeField] int _maxLevel;

    public PlayerLevelUpData()
    {
    }

    public void SetUserLevelData()
    {
        List<string> levelColumn = new List<string>();
        List<string> requiredExpColumn = new List<string>();

        for (int i = 0; i < maxLevel; i++)
        {
            levelColumn.Add((i+1).ToString());
        }
        headers.Add("Level", levelColumn);
        headerOrder.Add(1, "Level");

        Debug.Log("expToLevelUP.Count: " + expToLevelUp.Count);
        for (int i = 0; i < expToLevelUp.Count; i++)
        {
            requiredExpColumn.Add(expToLevelUp[i].ToString());
        }

        headers.Add("expToLevelUp", requiredExpColumn);
        headerOrder.Add(2, "expToLevelUp");

    }
    
}

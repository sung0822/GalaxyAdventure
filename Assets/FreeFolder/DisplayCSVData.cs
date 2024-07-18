using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DisplayCSVData : MonoBehaviour
{
    public CSVReader csvReader; // CSVReader 스크립트를 드래그 앤 드롭으로 할당
    public GameObject dataTextPrefab; // DataText 요소를 드래그 앤 드롭으로 할당
    public GameObject panel;
    public TextMeshProUGUI dataText; // DataText 요소를 드래그 앤 드롭으로 할당

    void Start()
    {
        List<Dictionary<string, string>> data = csvReader.ReadCSV();
        DisplayData(data);
    }

    void DisplayData(List<Dictionary<string, string>> data)
    {
        TextMeshProUGUI dataText;
        
        dataText = Instantiate<GameObject>(dataTextPrefab, panel.transform).GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < data[0].Count; i++)
        {

        }

        foreach (Dictionary<string, string> row in data)
        {       
            foreach (var kvp in row)
            {
                dataText = Instantiate<GameObject>(dataTextPrefab, panel.transform).GetComponent<TextMeshProUGUI>();
                dataText.text += kvp.Key + ": " + kvp.Value + "\t";
            }
        }
    }
}
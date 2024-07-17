using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DisplayCSVData : MonoBehaviour
{
    public CSVReader csvReader; // CSVReader 스크립트를 드래그 앤 드롭으로 할당
    public TextMeshProUGUI dataText; // DataText 요소를 드래그 앤 드롭으로 할당

    void Start()
    {
        List<Dictionary<string, string>> data = csvReader.ReadCSV();
        DisplayData(data);
    }

    void DisplayData(List<Dictionary<string, string>> data)
    {
        dataText.text = ""; // 기존 텍스트 초기화

        foreach (var row in data)
        {
            foreach (var kvp in row)
            {
                dataText.text += kvp.Key + ": " + kvp.Value + "\t";
            }
            dataText.text += "\n";
        }
    }
}
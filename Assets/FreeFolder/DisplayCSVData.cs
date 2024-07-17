using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DisplayCSVData : MonoBehaviour
{
    public CSVReader csvReader; // CSVReader ��ũ��Ʈ�� �巡�� �� ������� �Ҵ�
    public TextMeshProUGUI dataText; // DataText ��Ҹ� �巡�� �� ������� �Ҵ�

    void Start()
    {
        List<Dictionary<string, string>> data = csvReader.ReadCSV();
        DisplayData(data);
    }

    void DisplayData(List<Dictionary<string, string>> data)
    {
        dataText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

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
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile; // CSV 파일을 TextAsset으로 불러옵니다.

    public List<Dictionary<string, string>> ReadCSV()
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        string[] lines = csvFile.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            Debug.Log(lines[i]);
        }

        if (lines.Length <= 1)
        {
            Debug.Log("데이터가 없음");
            return data; // 데이터가 없으면 빈 리스트 반환
        }

        string[] headers = lines[0].Split(',');
        
        for (int i = 0; i < headers.Length; i++)
        {
            Debug.Log(headers[i]);
        }


        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            Dictionary<string, string> entry = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length; j++)
            {
                entry[headers[j]] = fields[j];
            }

            data.Add(entry);
        }

        return data;
    }
}
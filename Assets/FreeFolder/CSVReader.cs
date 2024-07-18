using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile; // CSV ������ TextAsset���� �ҷ��ɴϴ�.

    public List<Dictionary<string, string>> ReadCSV()
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        
        // �� ������
        string[] lines = csvFile.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            Debug.Log(lines[i]);
        }

        if (lines.Length <= 1)
        {
            Debug.Log("�����Ͱ� ����");
            return data; // �����Ͱ� ������ �� ����Ʈ ��ȯ
        }

        string[] headers = lines[0].Split(',');
        
        // ��� �и�
        for (int i = 0; i < headers.Length; i++)
        {
            Debug.Log(headers[i]);
        }


        // ����� �����ϰ� for��
        for (int i = 1; i < lines.Length; i++)
        {
            // �ʵ� �� ������
            string[] fields = lines[i].Split(',');
            Dictionary<string, string> entry = new Dictionary<string, string>();

            // ��� ������ŭ for��
            for (int j = 0; j < headers.Length; j++)
            {
                //���
                entry[headers[j]] = fields[j];
            }

            data.Add(entry);
        }

        return data;
    }
}
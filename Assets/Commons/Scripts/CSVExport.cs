using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class CSVExport : MonoBehaviour
{
    private StreamWriter sw;
    Settings settings;

    public void SaveData(string txt1, string txt2, string txt3, string txt4)
    {
        if (settings.DoCSVEXport)
        {
            string[] s1 = { txt1, txt2, txt3, txt4 };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            sw.Flush();
        }
    }

    public void FinishCSVExport()
    {
        if (settings.DoCSVEXport)
        {
            sw.Flush();
            sw.Close();
            Debug.Log("Exported CSV file.");
        }
    }

    void Start()
    {
        DateTime dt = DateTime.Now;
        settings = GameObject.Find("Settings").GetComponent<Settings>();

        string FilePath = @"Only1F_result_" + dt.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv";
        if (settings.DoCSVEXport) {
            sw = new StreamWriter(FilePath, true, Encoding.GetEncoding("UTF-8"));
            string[] s1 = { "X", "Z", "Success?", "ReachedExit" };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);

            Debug.LogWarning("Exporting now.");
        }
        else Debug.LogWarning("Not exporting now.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && settings.DoCSVEXport)
        {
            sw.Flush();
            sw.Close();
            Debug.Log("Exported CSV file.");
        }
    }
}

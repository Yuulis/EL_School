using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DataCountScript : MonoBehaviour
{
    // Text Mesh Pro
    public GameObject EpisodeCount, SuccessCount, SuccessRateCount, ReachedExit1Count, ReachedExit2Count, ReachedExit3Count;

    [HideInInspector]
    public TextMeshProUGUI tmp_EpisodeCount, tmp_SuccessCount, tmp_SuccessRateCount, tmp_ReachedExit1Count, tmp_ReachedExit2Count, tmp_ReachedExit3Count;

    // Counter
    [HideInInspector]
    public int EpisodeCounter, SuccessCounter, ReachedExit1Counter, ReachedExit2Counter, ReachedExit3Counter;


    public void ResetCounter()
    {
        EpisodeCounter = 0;
        SuccessCounter = 0;
        ReachedExit1Counter = 0;
        ReachedExit2Counter = 0;
        ReachedExit3Counter = 0;
    }

    public void UpdateCountText()
    {
        double rate = Math.Round((double)SuccessCounter / EpisodeCounter * 100, 2, MidpointRounding.AwayFromZero);

        tmp_EpisodeCount.text = "Episodes : " + EpisodeCounter.ToString();
        tmp_SuccessCount.text = "Success : " + SuccessCounter.ToString();
        if (EpisodeCounter > 0) tmp_SuccessRateCount.text = "Success Rate : " + rate.ToString() + "%";
        tmp_ReachedExit1Count.text = "Reached Exit1 : " + ReachedExit1Counter.ToString();
        tmp_ReachedExit2Count.text = "Reached Exit2 : " + ReachedExit2Counter.ToString();
        tmp_ReachedExit3Count.text = "Reached Exit3 : " + ReachedExit3Counter.ToString();
    }

    void Start()
    {
        tmp_EpisodeCount = EpisodeCount.GetComponent<TextMeshProUGUI>();
        tmp_SuccessCount = SuccessCount.GetComponent<TextMeshProUGUI>();
        tmp_SuccessRateCount = SuccessRateCount.GetComponent<TextMeshProUGUI>();
        tmp_ReachedExit1Count = ReachedExit1Count.GetComponent<TextMeshProUGUI>();
        tmp_ReachedExit2Count = ReachedExit2Count.GetComponent<TextMeshProUGUI>();
        tmp_ReachedExit3Count = ReachedExit3Count.GetComponent<TextMeshProUGUI>();

        ResetCounter();
    }

    void Update()
    {

    }
}

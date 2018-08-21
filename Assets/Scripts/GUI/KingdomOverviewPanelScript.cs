using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KingdomOverviewPanelScript : MonoBehaviour
{
    public GameObject[] KingdomOverviewPanelStats = new GameObject[6];
    public List<Kingdom> Locations { get; set; } = new List<Kingdom> { };
 
    void Start()
    {
        for(int i=0;i<6;i++)
        { 
            Text my_text = KingdomOverviewPanelStats[i].GetComponent<Text>();
            my_text.text = Locations[i].Chaos + Environment.NewLine + Locations[i].Description;
        }
    }

    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            Text my_text = KingdomOverviewPanelStats[i].GetComponent<Text>();
            my_text.text = Locations[i].Chaos + Environment.NewLine + Locations[i].Description;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KingdomOverviewPanelScript : MonoBehaviour
{
    public Text[] KingdomOverviewPanelStats;

    public void UpdatePanels(List<Kingdom> kingdoms)
    {
        for(int i = 0; i < kingdoms.Count; i++)
        {
            KingdomOverviewPanelStats[i].text = $"{kingdoms[i].Description}\n" +
                $"Chaos: {kingdoms[i].Chaos}";
        }
    }
}

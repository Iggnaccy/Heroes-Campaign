using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public enum Panels
    {
        None,
        GuildManagement,
        MissionAssignment,
        HeroRecruitment,
        KingdomOverview
    }

    public Panels activePanel;

    public GameObject[] panels;

    void Start()
    {
        ChangePanel(0);
    }

    public void ChangePanel(int panel)
    {
        if((Panels)panel== Panels.None)
        {
            for(int i = 1; i < panels.Length; i++)
            {
                    panels[i].SetActive(false);
            }
            activePanel = (Panels)panel;
            return;
        }
        else if(panels[(int)activePanel] != null)
        {
            panels[(int)activePanel].SetActive(false);
        }
        activePanel = (Panels)panel;
        panels[(int)activePanel].SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour {
    public GameObject GuildManagementHeroButton;
    public GameObject RecruitHeroButton;
    public GameObject MissionAssignmentHeroButton;
    public GameObject MissionButton;
    public Sprite[,,] PortraitHolder;
    void Start()
    {
        PortraitHolder = new Sprite[2,10,7];
        //sex race profession
        for(int i=0; i<2; i++)
        {
            for(int j=0; j<10; j++)
            {
                for(int k=0; k<7; k++)
                {
                    PortraitHolder[i,j,k]=Resources.Load<Sprite>("Portraits/"+((Hero.HeroSex)i).ToString()+ ((Hero.HeroRace)j).ToString()+ ((Hero.HeroProfession)k).ToString());
                }
            }
        }
    }
}

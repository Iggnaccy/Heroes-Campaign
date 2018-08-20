using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionAssignmentGUI : MonoBehaviour {

    public Transform MissionPanel, HeroPanel;
    public Text MissionDesc;

    PrefabHolder ph;
    
	// Use this for initialization
	void Awake () {
        ph = GameObject.FindGameObjectWithTag("PrefabHolder").GetComponent<PrefabHolder>();
	}
	
    public void DisplayMissionPanel(Game game)
    {
        for(int i = 0; i < game.Missions.Count; i++)
        {
            GameObject missionButton = Instantiate(ph.MissionButton);
            missionButton.transform.parent = MissionPanel;
            missionButton.transform.localPosition = new Vector3(missionButton.transform.localPosition.x, (missionButton.transform.localPosition.y + (i * (missionButton.GetComponent<RectTransform>().rect.height + 5))), missionButton.transform.localPosition.z);
            missionButton.GetComponent<Button>().onClick.AddListener(() =>
            {

            });
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionAssignmentGUI : MonoBehaviour {

    public Transform MissionPanel, HeroPanel;
    public Text MissionDesc;

    PrefabHolder ph;

    int activeMission;

	// Use this for initialization
	void Awake () {
        ph = GameObject.FindGameObjectWithTag("PrefabHolder").GetComponent<PrefabHolder>();
        activeMission = -1;
	}
	
    public void DisplayMissionPanel(Game game)
    {
        for(int i = 0; i < game.Missions.Count; i++)
        {
            GameObject missionButton = Instantiate(ph.MissionButton);
            missionButton.transform.SetParent(MissionPanel, false);
            missionButton.transform.localPosition = new Vector3(missionButton.transform.localPosition.x, (missionButton.transform.localPosition.y - (i * (missionButton.GetComponent<RectTransform>().rect.height + 2))), missionButton.transform.localPosition.z);
            int id = i;
            missionButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                activeMission = id;
                UpdateDisplayMission(game);
            });
        }
        for(int i = 0; i < game.Player.Heroes.Count; i++)
        {
            GameObject heroButton = Instantiate(ph.MissionAssignmentHeroButton);
            heroButton.transform.SetParent(HeroPanel, false);
            heroButton.transform.localPosition = new Vector3((heroButton.transform.localPosition.x + (i * (heroButton.GetComponent<RectTransform>().rect.width + 2))), heroButton.transform.localPosition.y, heroButton.transform.localPosition.z);
            int id = i;
            heroButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                if ((activeMission < 0 || activeMission >= game.Missions.Count) || game.Missions[activeMission].ParticipatingHeroes.Contains(game.Player.Heroes[id])) return;
                game.Missions[activeMission].ParticipatingHeroes.Add(game.Player.Heroes[id]);
                Debug.Log($"Added hero {id}: {game.Player.Heroes[id].Name} to mission {activeMission}: {game.Missions[activeMission].MissionName}");
                UpdateDisplayMission(game);
            });
        }
    }

    void UpdateDisplayMission(Game game)
    {
        Mission m = game.Missions[activeMission];
        MissionDesc.text = $"<b><size=24>{m.MissionName}</size></b>\n\n" +
            $"{m.MissionDescription}\n\n" +
            $"Participating heroes:\n";
        string heroes = "";
        foreach(Hero h in m.ParticipatingHeroes)
        {
            if (heroes.Length > 0)
                heroes += $", {h.Name} ({h.Profession})";
            else heroes += $"{h.Name} ({h.Profession})";
        }
        MissionDesc.text += heroes;
    }

    public void Clear()
    {
        foreach(Transform child in MissionPanel)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child in HeroPanel)
        {
            Destroy(child.gameObject);
        }
        activeMission = -1;
    }
}

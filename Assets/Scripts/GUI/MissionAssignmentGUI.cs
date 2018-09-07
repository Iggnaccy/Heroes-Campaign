using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionAssignmentGUI : MonoBehaviour {

    public RectTransform MissionPanel, HeroPanel;
    public Text MissionDesc;
    public GameObject ConfirmButton;

    public RectTransform HeroButtonHolder;
    public RectTransform MissionButtonHolder;

    PrefabHolder ph;

    int activeMission;
    private List<Hero> ChosenHeroes;

	// Use this for initialization
	void Awake () {
        ph = GameObject.FindGameObjectWithTag("PrefabHolder").GetComponent<PrefabHolder>();
        activeMission = -1;
        ChosenHeroes = new List<Hero>();
        ConfirmButton.SetActive(false);
	}
	
    public void DisplayMissionPanel(Game game)
    {
        MissionButtonHolder.sizeDelta = new Vector2(MissionButtonHolder.rect.width, System.Math.Max(MissionPanel.rect.height, game.Missions.Count*(ph.MissionButton.GetComponent<RectTransform>().rect.height+2)));
        for (int i = 0; i < game.Missions.Count; i++)
        {
            GameObject missionButton = Instantiate(ph.MissionButton);
            missionButton.transform.SetParent(MissionButtonHolder, false);
            missionButton.transform.localPosition = new Vector3(missionButton.transform.localPosition.x, (missionButton.transform.localPosition.y - (i * (missionButton.GetComponent<RectTransform>().rect.height + 2))), missionButton.transform.localPosition.z);
            int id = i;
            missionButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Mission: " + game.Missions[id].MissionName;
            missionButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                activeMission = id;
                UpdateDisplayMission(game);
            });
        }

        HeroButtonHolder.sizeDelta = new Vector2(System.Math.Max(HeroPanel.rect.width, game.Player.Heroes.Count*(ph.MissionAssignmentHeroButton.GetComponent<RectTransform>().rect.width+2)), HeroButtonHolder.sizeDelta.y);

        for(int i = 0; i < game.Player.Heroes.Count; i++)
        {
            GameObject heroButton = Instantiate(ph.MissionAssignmentHeroButton);
            heroButton.transform.SetParent(HeroButtonHolder.transform, false);
            heroButton.transform.localPosition = new Vector3(( ((i+0.5f) * (heroButton.GetComponent<RectTransform>().rect.width + 2))), heroButton.transform.localPosition.y, heroButton.transform.localPosition.z);
            heroButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = game.Player.Heroes[i].Name;
            int id = i;
            heroButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                if ((activeMission < 0 || activeMission >= game.Missions.Count) ) return;
                if (!ChosenHeroes.Contains(game.Player.Heroes[id]))
                {
                    ChosenHeroes.Add(game.Player.Heroes[id]);
                }
                else
                {
                    ChosenHeroes.Remove(game.Player.Heroes[id]);
                }
                //Debug.Log($"Added hero {id}: {game.Player.Heroes[id].Name} to mission {activeMission}: {game.Missions[activeMission].MissionName}");
                UpdateDisplayMission(game);
            });
            if(game.Player.Heroes[id].AssignedMission != null)
            {
                heroButton.transform.GetChild(1).gameObject.SetActive(true);
                heroButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                heroButton.transform.GetChild(1).gameObject.SetActive(false);
                heroButton.GetComponent<Button>().interactable = true;
            }
        }
        UpdateDisplayMission(game);
    }

    public void UpdateDisplayMission(Game game)
    {
        if (activeMission < 0 || activeMission >= game.Missions.Count)
        {
            MissionDesc.text = "<b>Choose a mission from the panel on the left,\n" +
                "and assign heroes to a mission using the bottom panel</b>";
            ConfirmButton.SetActive(false);
        }
        else
        {
            Mission m = game.Missions[activeMission];
            MissionDesc.text = $"<b><size=24>{m.MissionName}</size></b>\n\n" +
                $"{m.MissionDescription}\n\n" +
                $"Participating heroes:\n";
            string heroes = "";
            foreach (Hero h in ChosenHeroes)
            {
                if (heroes.Length > 0)
                    heroes += $", {h.Name} ({h.Profession})";
                else heroes += $"{h.Name} ({h.Profession})";
            }
            MissionDesc.text += heroes;
            if (ChosenHeroes.Count > 0)
            {
                ConfirmButton.SetActive(true);
                ConfirmButton.GetComponent<Button>().onClick.RemoveAllListeners();
                ConfirmButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    game.BeginMission(ChosenHeroes, game.Missions[activeMission]);
                    Clear();
                    DisplayMissionPanel(game);
                    activeMission = -1;
                });
            }
            else ConfirmButton.SetActive(false);
        }
    }

    public void Clear()
    {
        foreach(Transform child in MissionButtonHolder)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child in HeroButtonHolder)
        {
            Destroy(child.gameObject);
        }
        activeMission = -1;
        ConfirmButton.SetActive(false);
    }
}

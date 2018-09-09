using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildManagementGUIDisplay : MonoBehaviour {

    PrefabHolder ph;
    public GameObject GuildManagementHeroPanel;
    public Text GuildManagementHeroDescription;

    public RectTransform HeroButtonHolder;

    float GuildManagementHeroButtonHeight;
    int DisplayHero;

    void Awake()
    {
        ph = GameObject.FindGameObjectWithTag("PrefabHolder").GetComponent<PrefabHolder>();
        GuildManagementHeroButtonHeight = ph.GuildManagementHeroButton.GetComponent<RectTransform>().rect.height;
        DisplayHero = -1;
    }

    public void DisplayHeroButtons(Player player)
    {
        foreach(Transform child in HeroButtonHolder)
        {
            if(child.tag.ToLower() != "slider")
            {
                Destroy(child);
            }
        }
        HeroButtonHolder.sizeDelta = new Vector2(HeroButtonHolder.rect.width, System.Math.Max(player.Heroes.Count* (GuildManagementHeroButtonHeight + 3), GuildManagementHeroPanel.GetComponent<RectTransform>().rect.height));
        for (int i = 0; i < player.Heroes.Count; i++)
        {
            GameObject rt = Instantiate(ph.GuildManagementHeroButton);
            rt.transform.SetParent(HeroButtonHolder, false);
            rt.transform.GetChild(1).GetComponent<Image>().sprite = Cache.GetPortrait(player.Heroes[i].Sex, player.Heroes[i].Race, player.Heroes[i].Profession);
            rt.transform.localPosition = new Vector3(rt.transform.localPosition.x, rt.transform.localPosition.y - i * (GuildManagementHeroButtonHeight + 3));
            var button = rt.GetComponent<Button>();
            int id = i;
            button.onClick.AddListener(() =>
            {
                Debug.Log($"Setting DisplayHero to {id}");
                DisplayHero = id;
                DisplayHeroDescription(player);
            });
        }
    }

    public void DisplayHeroDescription(Player player)
    {
        if(DisplayHero < 0 || DisplayHero >= player.Heroes.Count)
        {
            GuildManagementHeroDescription.text = "";
        }
        else
        {
            Hero h = player.Heroes[DisplayHero];
            GuildManagementHeroDescription.text =
                $"Hero {h.Name}, Level {h.Level} ({h.Exp}/{(h.Level <= StaticValues.ExpNeededToNextLevel.Length - 2 ? StaticValues.ExpNeededToNextLevel[h.Level + 1] : 0)})\n" +
                $"Race: {h.Race}, {(h.Sex == Hero.HeroSex.Male ? "M" : "F")}\n" +
                $"Profession: {h.Profession}\n" +
                $"Salary: {h.Salary} gold/10 min\n\n" +
                $"Stats:\n\n" +
                $"Health: {h.Stats.Health}\n" +
                $"Mana: {h.Stats.Mana}\n\n" +
                $"Agility: {h.Stats.Agility}\n" +
                $"Strength: {h.Stats.Strength}\n" +
                $"Intelligence: {h.Stats.Intelligence}\n" +
                $"{(h.AssignedMission != null ? $"On mission: {h.AssignedMission.MissionName}" : "")}";
        }
    }

    public void Clear()
    {
        foreach (Transform child in HeroButtonHolder)
        {
            if (child.tag.ToLower() != "slider")
            {
                Destroy(child.gameObject);
            }
        }
        DisplayHero = -1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroRecruitmentGUI : MonoBehaviour {
    public GameObject HeroPanel;
    public Text CostText;
    public Text HeroDesc;
    public Button Confirm, Cancel;

    PrefabHolder ph;

    List<Button> buttons;
    int activeHero;
    Player player;
    List<Hero> AvailableHeroes;

    void Awake()
    {
        ph = GameObject.FindGameObjectWithTag("PrefabHolder").GetComponent<PrefabHolder>();
        buttons = new List<Button>();
        activeHero = -1;

        Cancel.onClick.AddListener(() =>
        {
            activeHero = -1;
            Cancel.gameObject.SetActive(false);
            Confirm.gameObject.SetActive(false);
            UpdateDesc(null);
        });

        Confirm.onClick.AddListener(() =>
        {
            Debug.Log("Recruiting hero:");
            AvailableHeroes[activeHero].Log();
            player.RecruitHero(AvailableHeroes[activeHero]);
            AvailableHeroes.RemoveAt(activeHero);
            Destroy(buttons[activeHero].gameObject);
            buttons.RemoveAt(activeHero);
            for(int i = activeHero; i < buttons.Count; i++)
            {
                buttons[i].transform.localPosition = new Vector3(buttons[i].transform.localPosition.x, buttons[i].transform.localPosition.y + buttons[i].GetComponent<RectTransform>().rect.height + 2);
            }

            activeHero = -1;
            Cancel.gameObject.SetActive(false);
            Confirm.gameObject.SetActive(false);
            UpdateDesc(null);
            ReDisplayHeroRecruitment();
        });
    }

    public void DisplayHeroRecruitment(Game game)
    {
        AvailableHeroes = game.AvailableHeroes;
        player = game.Player;
        for(int i = 0; i < AvailableHeroes.Count; i++)
        {
            GameObject HeroButton = Instantiate(ph.RecruitHeroButton);
            HeroButton.transform.SetParent(HeroPanel.transform, false);
            HeroButton.transform.localPosition = new Vector3(HeroButton.transform.localPosition.x, HeroButton.transform.localPosition.y - (i * (HeroButton.GetComponent<RectTransform>().rect.height + 2)));
            /*
             Tutaj wstaw edycję obrazku,
             imienia,
             małego opisu bohatera.
             */
            HeroButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = AvailableHeroes[i].Portrait;
            HeroButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Material for a great hero";
            HeroButton.transform.GetChild(2).gameObject.GetComponent<Text>().text=AvailableHeroes[i].Name;
            int id = i;
            buttons.Add(HeroButton.GetComponent<Button>());
            buttons[id].onClick.AddListener(() =>
            {
                activeHero = id;
                Confirm.gameObject.SetActive(true);
                Cancel.gameObject.SetActive(true);
                UpdateDesc(AvailableHeroes);
            });
        }
    }

    void ReDisplayHeroRecruitment()
    {
        foreach(Transform child in HeroPanel.transform)
        {
            Destroy(child.gameObject);
        }
        buttons.Clear();
        for (int i = 0; i < AvailableHeroes.Count; i++)
        {
            GameObject HeroButton = Instantiate(ph.RecruitHeroButton);
            HeroButton.transform.SetParent(HeroPanel.transform, false);
            HeroButton.transform.localPosition = new Vector3(HeroButton.transform.localPosition.x, HeroButton.transform.localPosition.y - (i * (HeroButton.GetComponent<RectTransform>().rect.height + 2)));
            /*
             Tutaj wstaw edycję obrazku,
             imienia,
             małego opisu bohatera.
             */
            HeroButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = AvailableHeroes[i].Portrait;
            HeroButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Material for a great hero";
            HeroButton.transform.GetChild(2).gameObject.GetComponent<Text>().text = AvailableHeroes[i].Name;
            int id = i;
            buttons.Add(HeroButton.GetComponent<Button>());
            buttons[id].onClick.AddListener(() =>
            {
                activeHero = id;
                Confirm.gameObject.SetActive(true);
                Cancel.gameObject.SetActive(true);
                UpdateDesc(AvailableHeroes);
            });
        }
    }

    void UpdateDesc(List<Hero> AvailableHeroes)
    {
        if(activeHero < 0 || AvailableHeroes == null || AvailableHeroes.Count <= activeHero)
        {
            HeroDesc.text = "";
        }
        else
        {
            Hero h = AvailableHeroes[activeHero];
            HeroDesc.text = $"Hero {h.Name}, the {h.Profession}\n" +
                $"Race: {h.Race}, {(h.Sex == Hero.HeroSex.Male ? "M" : "F")}\n" +
                $"Level {h.Level}\n" +
                $"Salary: {h.Salary} gold/10 min\n\n" +
                $"Stats:\n\n" +
                $"Health: {h.Stats.Health}, Mana: {h.Stats.Mana}\n" +
                $"Agility: {h.Stats.Agility}\n" +
                $"Strength: {h.Stats.Strength}\n" +
                $"Intelligence: {h.Stats.Intelligence}\n";
            CostText.text = (-h.Cost).ToString();
        }
    }

    public void Clear()
    {
        foreach (Transform child in HeroPanel.transform)
        {
            Destroy(child.gameObject);
        }
        buttons.Clear();
        activeHero = -1;
    }
    
}

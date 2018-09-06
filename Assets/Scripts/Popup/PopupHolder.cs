using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupHolder : MonoBehaviour {

    private Queue<Popup> PopupQueue;
    public GameObject Button;
    public Sprite DefaultBackground;
    public Image Background;
    public GameObject Content;
    public GameObject ScrollView;
    public Text Description;

    void Start () {
        PopupQueue = new Queue<Popup>();
        //Background = this.gameObject.GetComponent<Image>();
        //Background.enabled = false;
	}

    public void MakeSimplePopup(string text, string buttonDescription)
    {
        PopupQueue.Enqueue(new Popup(text, new string[]{buttonDescription } ,new List<UnityAction> {new UnityAction(ClosePopup) } ,DefaultBackground));
        if(PopupQueue.Count==1)
        {
            NextPopup();
           // PopupQueue.Peek().Initialize();
        }
    }

    public void MakePopup(string text, string[] buttonDescription, List<UnityAction> buttonEffects, Sprite background)
    {
        for (int i = 0; i < buttonEffects.Count; i++)
        {
            buttonEffects[i] += ClosePopup;
        }
        PopupQueue.Enqueue(new Popup(text, buttonDescription, buttonEffects, background));
        if (PopupQueue.Count == 1)
        {
            NextPopup();
            //PopupQueue.Peek().Initialize();
        }

    }

    public void MakeCustomPopup(string text, string[] buttonDescription, List<UnityAction> buttonEffects, Sprite background)
    {
        buttonEffects[buttonEffects.Count - 1] += ClosePopup;
        PopupQueue.Enqueue(new Popup(text, buttonDescription, buttonEffects, background));
        if (PopupQueue.Count == 1)
        {
            NextPopup();
            //PopupQueue.Peek().Initialize();
        }

    }




    public void ClosePopup()
    {
        PopupQueue.Dequeue();
        foreach (Transform child in Content.transform)
        {
                Destroy(child.gameObject);
        }

        if (PopupQueue.Count == 0)
        {
            Background.enabled = false;
            Description.enabled = false;
            ScrollView.SetActive(false);
        }
        else
            NextPopup();
            //PopupQueue.Peek().Initialize();
    }


    public void NextPopup()
    {
        Background.enabled = true;
        Description.enabled = true;
        ScrollView.SetActive(true);
        Popup InitializedPopup = PopupQueue.Peek();
        Background.sprite = InitializedPopup.Background;
        Description.text = InitializedPopup.PopupText;
        var rt = Content.GetComponent<RectTransform>();
        var number = Button.GetComponent<RectTransform>().rect.height;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x,System.Math.Max(ScrollView.GetComponent<RectTransform>().rect.height, InitializedPopup.ButtonEffects.Count * (number + 2)));
        for (int i = 0; i < InitializedPopup.ButtonEffects.Count; i++)
        {
            GameObject PopupButton = MonoBehaviour.Instantiate(Button);
            PopupButton.transform.SetParent(Content.transform, false);
            PopupButton.transform.localPosition = new Vector3(PopupButton.transform.localPosition.x, PopupButton.transform.localPosition.y - (i * (number + 2)));

            PopupButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = InitializedPopup.ButtonDescription[i];

            Button ButtonReference = PopupButton.GetComponent<Button>();
            ButtonReference.onClick.AddListener(InitializedPopup.ButtonEffects[i]);
            //ButtonReference.onClick.AddListener(InitializedPopup.ClosePopup);

        }
    }


}

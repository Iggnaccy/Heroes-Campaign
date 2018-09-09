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
    public RectTransform Content;
    public GameObject ScrollView;
    public Text Description;

    void Start () {
        PopupQueue = new Queue<Popup>();
        //Background = this.gameObject.GetComponent<Image>();
        //Background.enabled = false;
	}

    public void MakeSimplePopup(string text, string buttonDescription)
    {
        PopupQueue.Enqueue(new Popup(text, new List<Popup.ButtonBase>{new Popup.ButtonBase(buttonDescription, new UnityAction(ClosePopup)) } ,DefaultBackground));
        if(PopupQueue.Count==1)
        {
            NextPopup();
           // PopupQueue.Peek().Initialize();
        }
    }

    public void MakePopup(string text, List<Popup.ButtonBase> buttons, Sprite background)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].ButtonAction += ClosePopup;
        }
        PopupQueue.Enqueue(new Popup(text, buttons, background));
        if (PopupQueue.Count == 1)
        {
            NextPopup();
            //PopupQueue.Peek().Initialize();
        }

    }

    public void MakePopup(string text, List<Popup.ButtonBase> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].ButtonAction += ClosePopup;
        }
        PopupQueue.Enqueue(new Popup(text, buttons, DefaultBackground));
        if (PopupQueue.Count == 1)
        {
            NextPopup();
            //PopupQueue.Peek().Initialize();
        }

    }

    public void MakeCustomPopup(string text, List<Popup.ButtonBase> buttons, Sprite background)
    {
        buttons[buttons.Count - 1].ButtonAction += ClosePopup;
        PopupQueue.Enqueue(new Popup(text, buttons, background));
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
        var height = Button.GetComponent<RectTransform>().rect.height;
        Content.sizeDelta = new Vector2(Content.sizeDelta.x,System.Math.Max(ScrollView.GetComponent<RectTransform>().rect.height, InitializedPopup.Buttons.Count * (height + 2)+4));
        for (int i = 0; i < InitializedPopup.Buttons.Count; i++)
        {
            GameObject PopupButton = MonoBehaviour.Instantiate(Button);
            PopupButton.transform.SetParent(Content.transform, false);
            Debug.Log(Button.transform.localPosition.y);
            PopupButton.transform.localPosition = new Vector3(PopupButton.transform.localPosition.x, -158 - ((i-2.5f) * (height + 2)));

            PopupButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = InitializedPopup.Buttons[i].ButtonName;

            Button ButtonReference = PopupButton.GetComponent<Button>();
            ButtonReference.onClick.AddListener(InitializedPopup.Buttons[i].ButtonAction);
            //ButtonReference.onClick.AddListener(InitializedPopup.ClosePopup);

        }
    }


    public void DoNothing()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupHolder : MonoBehaviour {

    private Queue<Popup> PopupQueue;
    public GameObject Button;
    public Sprite DefaultBackground;
    private Image Background;

    void Start () {
        PopupQueue = new Queue<Popup>();
        Background = this.gameObject.GetComponent<Image>();
        Background.enabled = false;
	}

	public void MakeSimplePopup(string text, string buttonDescription)
    {
        PopupQueue.Enqueue(new Popup(text, buttonDescription, DefaultBackground, this.gameObject));
        if(PopupQueue.Count==1)
        {
            PopupQueue.Peek().Initialize();
            Background.enabled = true;
        }
    }

    public void MakePopup(string text, string[] buttonDescription, List<UnityAction> buttonEffects, Sprite background)
    {
        PopupQueue.Enqueue(new Popup(text, buttonDescription, buttonEffects, background, this.gameObject));
        if (PopupQueue.Count == 1)
        {
            PopupQueue.Peek().Initialize();
            Background.enabled = true;
        }

    }

    public void ClosePopup()
    {
        PopupQueue.Dequeue();
        foreach (Transform child in this.gameObject.transform)
        {
            if (child.name != "Description")
            {
                Destroy(child.gameObject);
            }
            else
            {
                child.gameObject.GetComponent<Text>().text = "";
            }
        }
        if (PopupQueue.Count == 0)
            Background.enabled = false;
        else
            PopupQueue.Peek().Initialize();
    }


}

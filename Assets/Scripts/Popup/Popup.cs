using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class Popup {
    public string PopupText;
    //List<Button> ListOfButtons;
    public Sprite Background;
    GameObject ParentObject;
    List<UnityAction> ButtonEffects;
    string[] ButtonDescription;

    public Popup(string text, string[] buttonDescription, List<UnityAction> buttonEffects,  Sprite background, GameObject parentObject)
    {
        PopupText = text;
        ButtonDescription = buttonDescription;
        ButtonEffects = buttonEffects;
        Background = background;
        ParentObject = parentObject;
    }

    public Popup(string text, string buttonDescription, Sprite background, GameObject parentObject)
    {
        PopupText = text;
        ButtonDescription =new string[] { buttonDescription};
        ButtonEffects = new List<UnityAction> { new UnityAction(DoNothing) };
        Background = background;
        ParentObject = parentObject;
    }

    public virtual void Initialize()
    {
        //ListOfButtons = new List<Button>();
        PopupHolder TemporaryPointer = ParentObject.GetComponent<PopupHolder>();
        ParentObject.GetComponent<Image>().sprite = Background;
        ParentObject.transform.GetChild(0).GetComponent<Text>().text = PopupText;
        for (int i=0; i<ButtonEffects.Count; i++)
        {
            GameObject PopupButton = MonoBehaviour.Instantiate(TemporaryPointer.Button);
            PopupButton.transform.SetParent(ParentObject.transform, false);
            PopupButton.transform.localPosition = new Vector3(PopupButton.transform.localPosition.x, PopupButton.transform.localPosition.y - (i * (PopupButton.GetComponent<RectTransform>().rect.height + 2)));

            PopupButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = ButtonDescription[i];

            Button ButtonReference=PopupButton.GetComponent<Button>();
            ButtonReference.onClick.AddListener(ButtonEffects[i]);
            ButtonReference.onClick.AddListener(TemporaryPointer.ClosePopup);

        }

    }

    public void DoNothing()
    {

    }


}

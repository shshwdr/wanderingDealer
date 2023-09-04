using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonOption
{
    public string text;
    public UnityAction action;

    public ButtonOption(string t,UnityAction a)
    {
        text = t;
        action = a;
    }
}
public class TransactionDialogueMenu : MonoBehaviour
{
    public Text mainText;
    public Text title;
    public List<Button> options;

    public void init(string title, string text, List<ButtonOption> options)
    {
        this.mainText.text = text;
        this.title.text = title;
        int i = 0;
        for (; i < options.Count; i++)
        {
            this.options[i].GetComponentInChildren<Text>().text = options[i].text;
            this.options[i].onClick.RemoveAllListeners();
            this.options[i].onClick.AddListener(options[i].action);
            this.options[i].gameObject.SetActive(true);
        }

        for (; i < this.options.Count; i++)
        {
            this.options[i].gameObject.SetActive(false);
        }
    }
}

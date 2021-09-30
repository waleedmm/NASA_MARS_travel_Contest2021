using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevionGames.UIWidgets;

public class DialogBoxTrigger : MonoBehaviour
{
    public string title;
    [TextArea]
    public string text;
    public Sprite icon;
    public string[] options;

    public DialogBox m_DialogBox;
    public bool m_ShowAtStart = false;
    public float m_WaitingSecondsAtloadingMessage = 1;
    private void Start()
    {
        if(m_DialogBox==null)
            this.m_DialogBox = FindObjectOfType<DialogBox>();
        if (m_ShowAtStart)
        {
            StartCoroutine(DelayStartShow());
        }
    }
    IEnumerator DelayStartShow()
    {
        yield return new WaitForSeconds(m_WaitingSecondsAtloadingMessage);
        Show();
    }
    public void Show() {
        m_DialogBox.Show(title, text, icon, null, options);
    }

    public void ShowText(string messageTitle, string messageContent)
    {
        m_DialogBox.Show(messageTitle, messageContent, icon, null, options);
    }

    public void ShowWithCallback()
    {
        m_DialogBox.Show(title, text, icon, OnDialogResult, options);
    }

    private void OnDialogResult(int index)
    {
        m_DialogBox.Show("Result", "Callback Result: "+options[index], icon, null, "OK");
    }
}

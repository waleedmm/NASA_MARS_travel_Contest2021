using UnityEngine;
using System.Collections;


public class UIManager : MonoBehaviour
{
    //[SerializeField]
    //public NotificationTrigger m_NotificationTrigger;
    //[SerializeField]
    //public DialogBoxTrigger m_DialogTrigger;

    public RPGTalk m_RpgTalker;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TalkRPGDialog(int startLine, int endLine)
    {
        m_RpgTalker.NewTalk(startLine.ToString(), endLine.ToString());
    }
    public void ShowMessageNotification(string message)
    {
        print(message);
        //m_NotificationTrigger.AddNotificationText(message);
    }
    public void ShowMessageDialog(string title, string message)
    {
        print(message);
        //m_NotificationTrigger.AddNotificationText( message);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //[SerializeField]
    //public NotificationTrigger m_NotificationTrigger;
    //[SerializeField]
    //public DialogBoxTrigger m_DialogTrigger;

    public RPGTalk m_RpgTalker;
    public GameObject m_NotificationWindow;
    public GameObject m_LosingWindow;
    public GameObject m_WinningWindow;
    
    public Text m_LosingTextArea;
    public Text m_WinningTextArea;
    public  Text m_NotificationTextArea;
    public int m_NotificationPeriod = 5;//seconds
    private int m_RemainingTimeForNotification;
    public AudioSource m_AudioSource;
    public AudioClip m_NotificationClip, m_WinningClip, m_LosingClip;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayAudio(AudioClip clip)
    {
        if (m_AudioSource != null && clip != null)
        {
            m_AudioSource.PlayOneShot(clip);
        }
    }
    public void ShowNotification(string message)
    {
        if (m_NotificationWindow)
        {
            PlayAudio(m_NotificationClip);
            m_NotificationWindow.SetActive(true);
            m_RemainingTimeForNotification = m_NotificationPeriod;
            m_NotificationTextArea.text = message;
            StartCoroutine(AutoHideNotification());
        }
    }
    public IEnumerator AutoHideNotification()
    {
        while (m_RemainingTimeForNotification > 0)
        {
            yield return new WaitForSeconds(1);
            m_RemainingTimeForNotification--;
        }
        if (m_NotificationWindow)
        {
            m_NotificationWindow.SetActive(false);
        }
    }
    public void ShowLoseWindow(string message)
    {
        if (m_LosingWindow)
        {
            m_LosingWindow.SetActive(true);
            m_LosingTextArea.text = message;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionItemSlotManager : MonoBehaviour
{
    public Text m_Name, m_Description;
    public Image m_StateImage;
    public Sprite m_doneSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowMissionData(string title, Mission m)
    {
        m_Name.text = title;
        m_Description.text = m.description;

    }
    public void SetMissionAsCompleted()
    {
        m_StateImage.sprite = m_doneSprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

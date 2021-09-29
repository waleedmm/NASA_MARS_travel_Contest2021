using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeterController : MonoBehaviour
{
    public RectTransform m_PointerSpriteTransform;
    public int m_MinAngle = 90, m_MaxAngle = -90;
    public int m_AngleDirection = -1;
    public int m_MinValue = 0, m_MaxValue = 100, m_Value=45;

    public Text m_TextValue;
    public TMPro.TextMeshProUGUI m_TextFeature;

    public string m_FeatureName = "Speed";
    public void RedrawPointerWithCurrentValue()
    {
        m_TextValue.text = m_Value.ToString()+"%";
        int AngleRange = Mathf.Abs(m_MaxAngle - m_MinAngle);
        int ValueRange = Mathf.Abs(m_MaxValue - m_MinValue);

        float anglePercentage = (float)(m_Value - m_MinValue) / ValueRange; 
        float desiredAngle = m_MinAngle + m_AngleDirection * anglePercentage * AngleRange;
        print("angle =" + desiredAngle);
        m_PointerSpriteTransform.rotation= Quaternion.Euler(0, 0, desiredAngle);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_TextFeature.text = m_FeatureName;
        RedrawPointerWithCurrentValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

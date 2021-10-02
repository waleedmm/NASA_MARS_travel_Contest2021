using UnityEngine;
using System.Collections;

public class ShipMovmentManager : MonoBehaviour
{
     Rigidbody m_Rigid;
    public Vector3 m_SpinningRotation = new Vector3(0, 10, 0);
    public void StartSpin()
    { 
        m_Rigid.AddRelativeTorque(m_SpinningRotation, ForceMode.Impulse);
    }
    // Use this for initialization
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        StartSpin();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
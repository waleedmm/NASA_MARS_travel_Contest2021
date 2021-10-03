using UnityEngine;
using System.Collections;

public class ShipMovmentManager : MonoBehaviour
{

    public static ShipMovmentManager _Instance;

    Rigidbody m_Rigid;
    public Vector3 m_SpinningRotation = new Vector3(0, 10, 0);
    //public Transform m_TargetPosition;
    //public GameObject m_LandingMarker;
    public MaterialSequencer m_TargetLandingSpot;

    public string m_TargetMaterialTag = "TargetArea";
    public Vector3 m_MovementDirection = new Vector3(0, -1, 0);
    public float m_MovementSpeed = 20,
        m_slowDownSpeed = 10;

    public MeterController m_SpeedMeter, m_HeightMeter;
    float m_DistanceToTarget;
    bool m_IsMoving = true;

    private void Awake()
    {
        _Instance = this;
    }
    public  void Report01_SolarStageSplit()
    {
        print("called on animation end");
    }
    public void CheckLandingToTarget()
    {
        GetDistanceToTarget();
        RaycastHit hit;

        if (Physics.Raycast(transform.position, m_MovementDirection, out hit, m_DistanceToTarget))
        {
            if (hit.collider.tag == m_TargetMaterialTag)
            {
                m_TargetLandingSpot.SetCorrectLandingMaterial();
            }
            else
            {
                m_TargetLandingSpot.EnableBlinkingTarget();
            }
        }
    }

    private void GetDistanceToTarget()
    {
        m_DistanceToTarget = Vector3.Distance(m_TargetLandingSpot.transform.position, this.transform.position);
    }

    public void StartSpin()
    { 
        m_Rigid.AddRelativeTorque(m_SpinningRotation, ForceMode.Impulse);
    }
    public void DeSpin()
    {
        m_Rigid.angularVelocity = Vector3.zero;
    }
    // Use this for initialization
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        GetDistanceToTarget();
        m_HeightMeter.m_MaxValue =(int) m_DistanceToTarget;
        m_HeightMeter.RedrawPointerWithCurrentValue();
        StartSpin();
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        m_Rigid.velocity = m_MovementSpeed * m_MovementDirection.normalized;
        m_SpeedMeter.RedrawPointerWithCurrentValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_MovementDirection = m_TargetLandingSpot.transform.position - this.transform.position;
            UpdateVelocity();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_MovementSpeed = m_slowDownSpeed;
            UpdateVelocity();
        }
        m_HeightMeter.m_Value = (int) (m_DistanceToTarget*100/ m_HeightMeter.m_MaxValue);
        m_HeightMeter.RedrawPointerWithCurrentValue();

        if (m_IsMoving)
        {
            Vector3 NewPosition = this.transform.position + m_MovementSpeed * m_MovementDirection.normalized*Time.deltaTime;
            m_Rigid.MovePosition(NewPosition);
        }
    }
}
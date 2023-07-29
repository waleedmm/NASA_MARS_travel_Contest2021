using UnityEngine;
using System.Collections;

public class ShipMovmentManager : MonoBehaviour
{
    public float m_GameNormalSpeed = 1; 
    public Camera m_MainCamera;
    private LerpCameraFollow m_lerpCamera;
    private FlyCamera m_flyCamera;

    public static ShipMovmentManager _Instance;
    private PlayerShipState m_StateObject;
    public MissionManager m_MissionManager;
    Rigidbody m_Rigid;
    public Vector3 m_SpinningRotation = new Vector3(0, 10, 0);
    //public Transform m_TargetPosition;
    //public GameObject m_LandingMarker;
    public MaterialSequencer m_TargetLandingSpot;
    private MeshRenderer m_ShipRender;
    public string m_TargetMaterialTag = "TargetArea";
    public Vector3 m_MovementDirection = new Vector3(0, -1, 0);
    public Transform[] m_CameraPositions;
    public GameObject m_DescendingUnit, m_RoverUnit;
    public Animator m_DescendingAnimation, m_RoverAnimation;
    public SimulationPhases m_CurrentPhase = SimulationPhases.Phase01_StageSeparation;
    
    private bool m_lastKinematic = false;
    private Vector3 m_lastVelocity;
    public float m_MovementSpeed = 20,
        m_slowDownSpeed = 10;

    public MeterController m_SpeedMeter, m_HeightMeter;
    float m_DistanceToTarget;
    bool m_IsMoving = true;
    public GameObject m_HeatParticle;
    public GameObject m_ExplosionParticle;
    public int m_HeatEffectDuration = 1;
    public Mission_01_03_AtmosphereEnterance m_AtmosphereMission3;
    private bool m_IsPaused = false;
    private void Awake()
    {
        _Instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AtomSphere")
        {
            if (m_HeatParticle)
            {
                if (m_StateObject.m_IsHeatShieldOn)
                {
                    m_HeatParticle.SetActive(true);
                    StartCoroutine(TurnOffHeatEffect());
                }
                else
                {
                    
                    ReportFailure("Your Heat Shield is Not Active, too much heat");

                }
            }
        }
    }
    public void DropDescendingUnit()
    {
        m_StateObject.DropDescendingUnit();

        StartCoroutine(waitToWatchDescendingUnit());
    }
    public IEnumerator waitToWatchDescendingUnit()
    {
        yield return new WaitForSeconds(5);
        m_DescendingUnit.SetActive(true);
        SwitchCameraTo(m_CameraPositions[0], m_DescendingUnit);
    }

    public void CollectSample()
    {
        m_RoverUnit.SetActive(true);
        SwitchCameraTo(m_CameraPositions[1], m_RoverUnit);

    }
    
    public void ReportFailure(string message)
    {
        m_ExplosionParticle.SetActive(true);
        m_MissionManager.ReportFailureofCurrentMission(message);

    }
    IEnumerator TurnOffHeatEffect()
    {
        yield return new WaitForSeconds(m_HeatEffectDuration);
        if (m_HeatParticle)
        {
            m_HeatParticle.SetActive(false);
            m_AtmosphereMission3.EndMission();
        }

    }
    public void DeployParachute()
    {
        m_StateObject.DeployParachute();
        
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

    public void OnTogglePause()
    {
       
    }
    public void PauseGame()
    {
        m_IsPaused = !m_IsPaused;
        Time.timeScale = (m_IsPaused) ? 0 : m_GameNormalSpeed;
        if (m_IsPaused)
        {
            //Time.timeScale = 0.1f;
            m_lastKinematic = m_Rigid.isKinematic;
            m_lastVelocity = m_Rigid.velocity;

            m_Rigid.isKinematic = true;
            m_Rigid.velocity = Vector3.zero;
        }
        else
            ResumeGame();
    }
    public void ResumeGame()
    {
        //Time.timeScale = 1;
         m_Rigid.isKinematic= m_lastKinematic;
         m_Rigid.velocity= m_lastVelocity;
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

    public void RemoveHeatShield()
    {
        m_StateObject.RemoveHeatShield();
    }

    
    // Use this for initialization
    void Start()
    {
        m_MainCamera = Camera.main;
        m_lerpCamera = m_MainCamera.GetComponent<LerpCameraFollow>();
        m_flyCamera = m_MainCamera.GetComponent<FlyCamera>();
        //Time.timeScale = m_GameNormalSpeed;

        m_StateObject = GetComponentInChildren<PlayerShipState>();
        m_ShipRender = GetComponent<MeshRenderer>();
        m_Rigid = GetComponent<Rigidbody>();
        GetDistanceToTarget();
        m_HeightMeter.m_MaxValue =(int) m_DistanceToTarget;
        m_HeightMeter.RedrawPointerWithCurrentValue();
        StartSpin();
        UpdateVelocity();
    }

    public void SwitchCameraTo(Transform pos, GameObject target)
    {
        m_MainCamera.transform.position = pos.position;
        m_lerpCamera.target = target.transform;
    }
    private void UpdateVelocity()
    {
        m_Rigid.velocity = m_MovementSpeed * m_MovementDirection.normalized;
        m_SpeedMeter.RedrawPointerWithCurrentValue();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    m_MovementDirection = m_TargetLandingSpot.transform.position - this.transform.position;
        //    UpdateVelocity();
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    m_MovementSpeed = m_slowDownSpeed;
        //    UpdateVelocity();
        //}
        m_HeightMeter.m_Value = (int) (m_DistanceToTarget*100/ m_HeightMeter.m_MaxValue);
        m_HeightMeter.RedrawPointerWithCurrentValue();

        if (m_IsMoving)
        {
            Vector3 NewPosition = this.transform.position + m_MovementSpeed * m_MovementDirection.normalized*Time.deltaTime;
            m_Rigid.MovePosition(NewPosition);
        }
    }
}
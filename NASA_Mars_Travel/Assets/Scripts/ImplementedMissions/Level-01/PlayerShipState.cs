using UnityEngine;
using System.Collections;


public class PlayerShipState : MonoBehaviour
{
    public static int m_Height;
    public bool m_IsCrusieStageSepearated = false,
        m_IsParachute_Deployed=false;
    public string m_AnimationName_StageSeperation = "m_AnimationName_StageSeperation",
        m_AnimationName_DeployParachute= "DeployParachute";
    public Animator m_CrusieAnimator, m_RoverAnimator;
    public Transform m_Parachute, m_rover, m_heatShield, m_HeatParticleEffect;
    public void SeparateStatgeCruise()
    {
        m_CrusieAnimator.SetTrigger(m_AnimationName_StageSeperation);
        m_IsCrusieStageSepearated = true;
    }
    public void DeployParachute()
    {
        m_CrusieAnimator.SetTrigger(m_AnimationName_DeployParachute);
        m_IsParachute_Deployed = true;
    }
    //IEnumerator CheckForCruiseSeparationEnd()
    //{ 
    //    while()
    //}
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

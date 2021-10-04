using UnityEngine;
using System.Collections;


public class PlayerShipState : MonoBehaviour
{
    public static int m_Height;
    public bool m_IsCrusieStageSepearated = false,
        m_IsParachute_Deployed=false,
        m_IsHeatShieldOn=true,
        m_hasDescedingUnit=false;
    public string m_AnimationName_StageSeperation = "m_AnimationName_StageSeperation",
        m_AnimationName_DeployParachute= "DeployParachute",
        m_AnimationName_SplitHeatShield="SplitHeatShield",
        m_AnimationName_DropDescedningUnit= "DropDescendingUnit";
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
    public void RemoveHeatShield()
    {
        m_CrusieAnimator.SetTrigger(m_AnimationName_SplitHeatShield) ;
        m_IsHeatShieldOn = false;
    }
    public void DropDescendingUnit()
    {
        m_CrusieAnimator.SetTrigger(m_AnimationName_DropDescedningUnit);
        m_hasDescedingUnit = true;
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

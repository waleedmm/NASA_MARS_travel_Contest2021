using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

	//Lista de missões
	//public Mission[] missions;
	public int currentMissionId;
	public string currentMissionDescription;
	public List<Mission> missions;
	public Mission m_CurrentMission = null;
	public UIManager m_UIManager;
	//gerenciador de limiters da cena
	//[SerializeField] LimiterManager limiterManager;

	[SerializeField] AudioSource source;
	[SerializeField]AudioClip clip;
	//SetNextMission

	public GameObject m_SideWindowSlotsContainer;
	public GameObject m_UISlotPrefab;

	private List<MissionItemSlotManager> m_UISlotsManagers = new List<MissionItemSlotManager>();
	public int m_LastDoneMissionIndex = -1;
	

	// Use this for initialization
	void Start () {
		//a missão sempre começa na numero 1
		currentMissionId = 1;
		currentMissionDescription = missions [currentMissionId -1].description;
		missions [currentMissionId - 1].isCurrent = true;
		missions[currentMissionId - 1].ActivateMission();

		GameObject tempSlot;
		MissionItemSlotManager tempSlotManager;
		int counter = 1;
		//show missions
		foreach (Mission m in missions)
		{
			tempSlot = Instantiate(m_UISlotPrefab);
			tempSlot.transform.parent = m_SideWindowSlotsContainer.transform;
			tempSlotManager = tempSlot.GetComponent<MissionItemSlotManager>();
			tempSlotManager.ShowMissionData("Mission-"+counter, m);
			m_UISlotsManagers.Add(tempSlotManager);
			m.m_MissionIndex = counter - 1;
			counter++;

		}

		//referenciando o limiterManager
		//limiterManager = FindObjectOfType<LimiterManager> ();
	}
	public void ReportFailureofCurrentMission(string message)
	{
		m_UIManager.ShowLoseWindow(message);
	}
	public void CompleteMission(int MissionIndex){
		source.PlayOneShot(clip);
		if (MissionIndex > m_LastDoneMissionIndex)
			m_LastDoneMissionIndex = MissionIndex;

		missions[MissionIndex].SetCompleted ();
		m_UISlotsManagers[MissionIndex].SetMissionAsCompleted();
		if (currentMissionId < missions.Count)
		{
			SetNextMission();
		}
		else
		{
			DeclareFinalMissionCompletion();
		}
	}
	void DeclareFinalMissionCompletion()
	{
		if (m_UIManager != null)
			m_UIManager.ShowMessageNotification("All missions are done");
	}
	void SetNextMission(){
		//currentMissionId = m_LastDoneMissionIndex + 1 ;
		//limiterManager.DeactivateLimiter(currentMissionId -1);
		//print (currentMissionId -1 + "ID DA MISSAO");
		if (!missions[m_LastDoneMissionIndex].isCurrent)
		{
			m_CurrentMission = missions[m_LastDoneMissionIndex];
			currentMissionDescription = missions[m_LastDoneMissionIndex].description;

			missions[m_LastDoneMissionIndex].ActivateMission();
		}
		//destrói o limiter ao setar a nova missão
		else
		{
			m_CurrentMission = null;
		}
	}

//	public bool CheckCurrentMission(int id){
//		if (currentMissionId == id)
//			return true;
//		else
//			return false;
//	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Mission : MonoBehaviour {
	//variáveis em comum nas missões
	[SerializeField] int id;
	[SerializeField] string m_MissionName;
	[SerializeField] int level;
	public bool isCompleted;
	public bool isCurrent;
 	public string description;
	public UnityEvent m_CallOnDone;
	public UIManager m_UIManager;
	public string 	m_CompletionMessage = "done message" ;
	public string m_FailureMessage = "Failure To Complete mission";
	public MissionManager missionMan;
	public int m_MissionIndex;

	public virtual void PeriodicalCheck()
	{ 
		//TODO: Override
	}
	public void ReportMissionFailure(string extraFailureMessage = "")
	{
		missionMan.ReportFailureofCurrentMission(m_FailureMessage + "." + extraFailureMessage) ;
	}
	public void SetCompleted(){
		isCompleted = true;
		isCurrent = false;
		Debug.Log ("Mission Completed");
		if (m_CallOnDone!=null)
			m_CallOnDone.Invoke();
	}

	public void ActivateMission(){
		isCurrent = true;
		Debug.Log ("Mission Started");
		m_UIManager.ShowMessageDialog("Mission"+ id+ "("+m_MissionName+")",description );
	}
}

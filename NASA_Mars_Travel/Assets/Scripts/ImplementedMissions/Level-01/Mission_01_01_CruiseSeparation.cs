using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_01_01_CruiseSeparation : Mission, IComplete
{

	public PlayerShipState m_ShipPlayer;
	
	public GameObject m_ToBeFoundGlasses;
	public Vector2Int m_RPG_Init_Lines, m_RPG_Done_lines;
	void Start()
	{
		if (missionMan == null)
			missionMan = FindObjectOfType<MissionManager>();
		

	}

	public void EndMission()
	{
		m_ShipPlayer.SeparateStatgeCruise();
		
		//m_PlayerGlasses.SetActive(true);
		missionMan.CompleteMission(this.m_MissionIndex);
		if (m_RPG_Done_lines.magnitude > 0)
		{
			m_UIManager.TalkRPGDialog(m_RPG_Done_lines.x, m_RPG_Done_lines.y);
		}
	}


}
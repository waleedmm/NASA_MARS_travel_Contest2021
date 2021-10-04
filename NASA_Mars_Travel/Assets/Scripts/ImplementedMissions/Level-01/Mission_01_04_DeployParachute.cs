

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_01_04_DeployParachute : Mission, IComplete
{

	public PlayerShipState m_ShipPlayer;
	ShipMovmentManager m_ShipManager;

	public Vector2Int m_RPG_Init_Lines, m_RPG_Done_lines;


	void Start()
	{
		if (missionMan == null)
			missionMan = FindObjectOfType<MissionManager>();

		m_ShipManager = m_ShipPlayer.GetComponent<ShipMovmentManager>();

	}



	public void EndMission()
	{
		m_ShipManager.DeployParachute();
		print("mission end");
		//m_PlayerGlasses.SetActive(true);
		missionMan.CompleteMission(this.m_MissionIndex);
		if (m_RPG_Done_lines.magnitude > 0)
		{
			m_UIManager.TalkRPGDialog(m_RPG_Done_lines.x, m_RPG_Done_lines.y);
		}
	}

	public void Mission_Update()
	{
		throw new System.NotImplementedException();
	}

	public bool AreRequriedPrerequistsGood()
	{
		throw new System.NotImplementedException();
	}

	public bool IsMissionCompleted()
	{
		throw new System.NotImplementedException();
	}
}

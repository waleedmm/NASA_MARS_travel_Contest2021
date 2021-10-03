using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComplete {

	// Use this for initialization
	void EndMission();

	void Mission_Update();
	bool AreRequriedPrerequistsGood();
	bool IsMissionCompleted();
	
}

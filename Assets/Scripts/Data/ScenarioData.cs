using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/ScenarioData")]
public class ScenarioData : ScriptableObject
{
	public string PresentedName;
	public string SceneName;
	public int StartingMoney;
	public int NumberOfSpawns;
	public List<WaveData> Waves;
}

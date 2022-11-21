using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bot Driver", menuName = "ScriptableObjects/Bot Driver", order = 1)]
public class BotDriverSettings : ScriptableObject
{
	public float turnSensitivity = 0.2f;
	public float nextTurnSensitivity = 0.1f;
	public bool autoCalcOffset = false;
	public float inputMultiplier = 1;
	public float pointCompletionDistance = 2;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometre : MonoBehaviour {
	
	private Text waveText;

	// Use this for initialization
	void Start () {
		waveText = GetComponent<Text> ();
	}

	void CountDownTime(float waveTimer){
		
		if(waveTimer > 0){
			waveTimer = (float)Math.Round(waveTimer, 1);
			waveText.text = "Waves: "+ waveTimer.ToString ("0.0");
		}
	}
	void OnEnable()
	{
		WaveSpawner.OnCountDown += CountDownTime;
	}
}
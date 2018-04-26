using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

	private GameObject[] pipeHolders;
	private int distance = 5;
	private float lastPipesX;
	private float pipeMin = -1.5f;
	private float pipeMax = 2.5f;

	// Use this for initialization
	void Awake () {

		pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

		for (int i = 0; i < pipeHolders.Length; i++) {
			Vector3 tempPos = pipeHolders[i].transform.position;
			tempPos.y = Random.Range(pipeMin, pipeMax);
			pipeHolders[i].transform.position = tempPos;
		}

		lastPipesX = pipeHolders[0].transform.position.x;

		for (int i = 1; i < pipeHolders.Length; i++) {
			if (lastPipesX < pipeHolders[i].transform.position.x) {
				lastPipesX = pipeHolders [i].transform.position.x;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target) {

		if(target.tag == "PipeHolder") {
			Vector3 tempPos = target.transform.position;
			tempPos.x = lastPipesX + distance;
			tempPos.y = Random.Range(pipeMin, pipeMax);
			target.transform.position = tempPos;
			lastPipesX = tempPos.x;
		}
	}



}

using UnityEngine;
using System.Collections;
using System.Threading;

public class ThreadingScript : MonoBehaviour {

	//Variable that both threads will access
	private string _threadOutput = "";

	//Bool to stop threads from running
	private bool _stopThreads = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DisplayThread1()
	{
		while (_stopThreads == false) {
			Debug.Log ("Display Thread 1");
		}
	}
}

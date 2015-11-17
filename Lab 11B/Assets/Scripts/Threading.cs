using UnityEngine;
using System.Collections;
using System.Threading;

public class Threading : MonoBehaviour {

	//Variable that both threads will access
	private string _threadOutput = "";
	
	//Bool to stop threads from running
	private bool _stopThreads = false;

	/// <summary>
	/// Loop thread 1 continuously
	/// Assign the thread information into _threadOutput
	/// </summary>
	void DisplayThread1()
	{
		while(_stopThreads == false)
		{
			lock(this) //Lock on the CURRENT instance of the CLASS for thread#1
			{
				Debug.Log ("Display Thread 1");
				_threadOutput = "Hello Thread1";
				
				//Put the thread to sleep (stop processing) for 1000 ms
				//This is to simulate a lot of processing and calculations that
				//would normally occur in a thread
				Thread.Sleep(1000);
				Debug.Log ("Thread 1 Output --> " + _threadOutput);
			}//Release the lock for thread 1 here
		}
	}

	/// <summary>
	/// Loop thread2 continuously
	/// Assign the thread information into _threadOutput
	/// </summary>
	void DisplayThread2()
	{
		while(_stopThreads == false)
		{
			lock(this)//Lock on the CURRENT instance of the CLASS for thread#2
			{
				Debug.Log ("Display Thread 2");
				_threadOutput = "Hello Thread2";
				
				//Put the thread to sleep (stop processing) for 1000 ms
				//This is to simulate a lot of processing and calculations that
				//would normally occur in a thread
				Thread.Sleep(1000);
				Debug.Log ("Thread 2 Output --> " + _threadOutput);
			}//Release the lock for thread 2 here
		}
	}

	/// <summary>
	/// Triggers to flag to stop threads from running
	/// This is so unity doesn't lock up!
	/// </summary>
	void StopThreads()
	{
		_stopThreads = true;
	}

	// Use this for initialization
	void Start () {
	
		//Invoke the stop threads after 10 seconds so you can review the results
		Invoke ("StopThreads", 10);
		
		//Create two new threads starts that points to the functions
		//that the threads will be running
		ThreadStart firstThread = new ThreadStart (DisplayThread1);
		ThreadStart secondThread = new ThreadStart (DisplayThread2);

		//Create the two threads
		Thread thread1 = new Thread (firstThread);
		Thread thread2 = new Thread (secondThread);
		
		//Start the two threads
		thread1.Start ();
		thread2.Start ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

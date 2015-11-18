using UnityEngine;
using System.Collections;
using System.Threading;

// Author: Tiffany Fisher
// Modified by: Nathan Boehning
// Purpose: Demonstrate threading / locked threading
public class ScriptThreading : MonoBehaviour {

	// Variable that both threads can access
	private string _threadOutput = "";

	// Bool to stop the threads
	private bool _stopThreads = false;

	// Use this for initialization
	void Start ()
	{
		// Invoke the stop threads after 10 seconds to review the results
		Invoke("StopThreads", 10);

		// Create two new threads that points to the functions
		// That the threads will be running
		ThreadStart firstThread = new ThreadStart(DisplayThread1);
		ThreadStart secondThread = new ThreadStart(DisplayThread2);

		// Create the two threads
		Thread thread1 = new Thread(firstThread);
		Thread thread2 = new Thread(secondThread);

		// Start the two threads
		thread1.Start();
		thread2.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// Loop thread 1 continuously
	// Assign the thread information into _threadOutput
	void DisplayThread1()
	{
		while (!_stopThreads)
		{
			lock (this) // Lock on the current instance of the class for thread 1
			{
				Debug.Log("Display Thread 1");
				_threadOutput = "Hello Thread1";

				// Put the thread to sleep for 1000 ms
				// This simulates a lot of processing calculations
				// That would normally occur in a thread
				Thread.Sleep(1000);
				Debug.Log("Thread 1 Output ---> " + _threadOutput);
			}   // Realease the lock

		}
	}

	// Loop thread 2 continuously
	// Assign the thread information into _threadOutput
	void DisplayThread2()
	{
		while (!_stopThreads)
		{
			lock (this) // Lock on the current instance of the class for thread 2
			{
				Debug.Log("Display Thread 2");
				_threadOutput = "Hello Thread2";

				// Put the thread to sleep for 1000 ms
				// This simulates a lot of processing calculations
				// That would normally occur in a thread
				Thread.Sleep(1000);
				Debug.Log("Thread 2 Output ---> " + _threadOutput);
			}   // Realease the lock
		}
	}

	// Triggers to flag to stop threads from running
	// Done to ensure Unity doesn't lock up.
	void StopThreads()
	{
		_stopThreads = true;    
	}
}

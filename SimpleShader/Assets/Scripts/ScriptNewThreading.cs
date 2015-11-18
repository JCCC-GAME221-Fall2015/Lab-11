using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEditor;

// Author: Nathan Boehning
// Purpose: Threading test
public class ScriptNewThreading : MonoBehaviour
{
	private bool done = false;

	// Use this for initialization
	void Start ()
	{
		// Create four new threads that points to the functions
		// That the threads will be running
		ThreadStart firstThread = new ThreadStart(ThreadFunction1);
		ThreadStart secondThread = new ThreadStart(ThreadFunction2);
		ThreadStart thirdThread = new ThreadStart(ThreadFunction3);
		ThreadStart fourthThread = new ThreadStart(ThreadFunction3);

		// Create the four threads
		Thread thread1 = new Thread(firstThread);
		Thread thread2 = new Thread(secondThread);
		Thread thread3 = new Thread(thirdThread);
		Thread thread4 = new Thread(fourthThread);

		// Start the four threads
		thread1.Start();
		thread2.Start();
		thread3.Start();
		thread4.Start();
	}
	
	// Function that prints x 10 times
	private void ThreadFunction1()
	{
		for (int i = 0; i < 10; i++)
		{
			Debug.Log("X");
		}
	}

	// Function that prints y 10 times
	private void ThreadFunction2()
	{
		for (int i = 0; i < 10; i++)
		{
			Debug.Log("Y");
		}
	}

	// Function that sets a bool to done
	void ThreadFunction3()
	{
		if (!done)
		{
			Debug.Log("Done");
			done = true;
		}
	}
}

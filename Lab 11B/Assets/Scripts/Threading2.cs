using UnityEngine;
using System.Collections;
using System.Threading;

public class Threading2 : MonoBehaviour {
	
	private bool done = false;

	void Function1()
	{
		for (int i = 0; i < 10; i++)
		{
			Debug.Log("x");
		}
	}
	
	void Function2()
	{
		for (int i = 0; i < 10; i++)
		{
			Debug.Log("y");
		}
	}
	
	void Function3()
	{
		if (!done)
		{
			Debug.Log("Done");
			done = true;
		}
	}

	// Use this for initialization
	void Start () {
		
		//Create four new threads starts that points to the functions
		//that the threads will be running
		ThreadStart firstThread = new ThreadStart (Function1);
		ThreadStart secondThread = new ThreadStart (Function2);
		ThreadStart thirdThread = new ThreadStart (Function3);
		ThreadStart fourthThread = new ThreadStart (Function3);

		//Create the four threads
		Thread thread1 = new Thread (firstThread);
		Thread thread2 = new Thread (secondThread);
		Thread thread3 = new Thread (thirdThread);
		Thread thread4 = new Thread (fourthThread);

		//Start the four threads
		thread1.Start ();
		thread2.Start ();
		thread3.Start ();
		thread4.Start ();
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}
}

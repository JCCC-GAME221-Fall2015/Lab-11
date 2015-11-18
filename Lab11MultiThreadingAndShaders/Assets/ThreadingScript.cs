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
        //Create 2 new threads starts that points to the functions
        //that the threads will be running
        ThreadStart firstThread = new ThreadStart(DisplayThread1);
        ThreadStart secondThread = new ThreadStart(DisplayThread2);

        //Create 2 new threads
        Thread thread1 = new Thread(firstThread);
        Thread thread2 = new Thread(secondThread);

        //Start the 2 threads
        thread1.Start();
        thread2.Start();

        //Invoke the stop threads after 10 seconds so you can review the results
        Invoke("StopThreads", 10);
    }

    // Update is called once per frame
    void Update () {
	
	}

    /// <summary>
    /// Loop thread 1 continuously
    /// Assign the thread info into _threadOutput
    /// </summary>
	void DisplayThread1()
    {
        while (_stopThreads == false)
        {
            lock (this)//lock on the current instance of the class for thread #1
            {
                Debug.Log("Display Thread 1");
                _threadOutput = "Hello Thread1";

                //Put the thread to sleep (stop processing for 1000ms
                //This is to simulate a lot of processing & clac.s that
                //normally would occur in a thread
                Thread.Sleep(1000);
                Debug.Log("Thread 1 Output --> " + _threadOutput);
            }//Release lock for thread 1 here
        }
    }

    /// <summary>
    /// Loop thread 2 continuously
    /// Assign the thread info into _threadOutput
    /// </summary>
	void DisplayThread2()
    {
        while (_stopThreads == false)
        {
            lock (this)//lock on the current instance of the class for thread #2
            {
                Debug.Log("Display Thread 2");
                _threadOutput = "Hello Thread2";

                //Put the thread to sleep (stop processing for 1000ms
                //This is to simulate a lot of processing & clac.s that
                //normally would occur in a thread
                Thread.Sleep(1000);
                Debug.Log("Thread 2 Output --> " + _threadOutput);
            }//Release lock for thread 2 here
        }
    }

    /// <summary>
    /// Triggers to flag to stop threads from running
    /// Th.is is so Unity doesn't lock up!
    /// </summary>
    void StopThreads()
    {
        _stopThreads = true;
    }

}

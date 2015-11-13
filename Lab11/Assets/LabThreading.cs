using UnityEngine;
using System.Collections;
using System.Threading;

public class LabThreading : MonoBehaviour 
{
    //Variable both threads will access
    private string _threadOutput = "";
    //Flag to stop running threads
    private bool _stopThreads = false;

    //Start creates threads used for this project.
	void Start () 
    {
        //Create new thread starts that point to functions threads will run
        ThreadStart firstThread = new ThreadStart(DisplayThread1);
        ThreadStart secondThread = new ThreadStart(DisplayThread2);

        //Create the threads
        Thread thread1 = new Thread(firstThread);
        Thread thread2 = new Thread(secondThread);

        //Start threads
        thread1.Start();
        thread2.Start();

        Invoke("StopThreads", 10);
	}

    /// <summary>
    /// Loop Thread1 continuously
    /// Assigns thread information into _threadOutput
    /// </summary>
    void DisplayThread1()
    {
        while(_stopThreads == false)
        {
            lock(this)
            {
                Debug.Log("Display Thread 1");
                _threadOutput = "Hello Thread1";
                Thread.Sleep(1000); //Stop processing for 1000ms
                Debug.Log("Thread 1 Output -->" + _threadOutput);
            }
        }
    }

    /// <summary>
    /// Loop Thread2 continuously
    /// Assigns thread information into _threadOutput
    /// </summary>
    void DisplayThread2()
    {
        while (_stopThreads == false)
        {
            lock (this)
            {
                Debug.Log("Display Thread 2");
                _threadOutput = "Hello Thread2";
                Thread.Sleep(1000); //Stop processing for 1000ms
                Debug.Log("Thread 2 Output -->" + _threadOutput);
            }
        }
    }

    /// <summary>
    /// Triggers flag to stop threads.
    /// Keeps Unity from locking.
    /// </summary>
    void StopThreads()
    {
        _stopThreads = true;
    }
}

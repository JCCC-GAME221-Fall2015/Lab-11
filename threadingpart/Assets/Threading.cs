using UnityEngine;
using System.Threading;
using System.Collections;

public class Threading : MonoBehaviour {

    //variable that both threads will access
    private string _threadOutput = "";

    //Bool to stop threads from running
    private bool _stopThreads = false;


    void Start()
    {
        //Create two new threads starts that points to the function
        //that the threads wil be running
        ThreadStart firstThread = new ThreadStart(DisplayThread1);
        ThreadStart secondThread = new ThreadStart(DisplayThread2);

        //Create the two threads
        Thread thread1 = new Thread(firstThread);
        Thread thread2 = new Thread(secondThread);

        //Start the two threads
        thread1.Start();
        thread2.Start();

        //Invoke the stop threads after 10 seconds so you can review the results
        Invoke("StopThreads", 10);
    }

    /// <summary>
    /// Loop thread 1 continuously
    /// Assign the thread information into _threadOutput
    /// </summary>
    void DisplayThread1()
    {
        while(_stopThreads == false)
        {
            Debug.Log("Display Thread 1");
            _threadOutput = "Hello Thread1";

            //put the thread to sleep (stop processing) for 1000ms
            //This is to simulate a lot of processing and calculation that
            //would normally occur in a thread
            Thread.Sleep(1000);
            Debug.Log("Thread 1 Output --> " + _threadOutput);
        }
    }

    /// <summary>
    /// Loop thread 2 continuously
    /// Assign the thread information into _threadOutput
    /// </summary>
    void DisplayThread2()
    {
        while(_stopThreads == false)
        {
            Debug.Log("Display Thread 2");
            _threadOutput = "Hello Thread2";

            //put the thread to sleep ( stop processing) for 1000ms
            //this is to simulate a lot of processing and alculation that
            //would normally occur in a thread
            Thread.Sleep(1000);
            Debug.Log("Thread 2 output --> " + _threadOutput);
        }
    }

    //Triggers to flag to stop threads from running
    //This is so unity doesn't lock up!
    void StopThreads()
    {
        _stopThreads = true;
    }
}

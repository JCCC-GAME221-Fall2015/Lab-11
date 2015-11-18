using UnityEngine;
using System.Collections;
using System.Threading;

public class SimpleThreadingScript : MonoBehaviour {

    //both threads will access
    string isAString = "";

    //stop thread from running bool
    bool isABool = false;

    bool done = false;

    /// <summary>
    /// Loop thread 1 continuously
    /// Assign the thread information into isAString
    /// </summary>
    void DisplayThread1()
    {
        while(isABool == false)
        {
            lock(this)//lock on the current instance of the class for thread#1
            {
                Debug.Log("Display Thread 1");
                isAString = "Hello Thread 1";

                //Put the thread to sleep for 1000 ms
                //this is to simulate a lot of processing and calculations that
                //would normally occure in a thread
                Thread.Sleep(1000);
                Debug.Log("Thread 1 Output --> " + isAString);
            }//Release the lock for thread 1 here
        }
    }

    /// <summary>
    /// Loop thread 2 continuously
    /// Assign the thread information into isAString
    /// </summary>
    void DisplayThread2()
    {
        while (isABool == false)
        {
            lock(this)//Lock on the current instance of the class for thread #2
            {
                Debug.Log("Display Thread 2");
                isAString = "Hello Thread 2";

                //Put the thread to sleep for 1000 ms
                //this is to simulate a lot of processing and calculations that
                //would normally occure in a thread
                Thread.Sleep(1000);
                Debug.Log("Thread 2 Output --> " + isAString);
            }//Release the lock for thread 2 here
        }
    }

    /// <summary>
    /// Triggers the flag to stop threads from running
    /// This is so unity doesn't break and destroy the
    /// world trying to figure out how it is going to
    /// process forever
    /// </summary>
    void StopThreads()
    {
        isABool = true;
    }

	// Use this for initialization
	void Start () {
	    //invoke the stop theads after 10 seconds so thecomputer doesn't bork
        //Invoke("StopThreads", 10);

        //create the two threads starts that point to the functions
        //that the threads will be running
        //ThreadStart firstThread = new ThreadStart(DisplayThread1);
        //ThreadStart secondThread = new ThreadStart(DisplayThread2);

        //create the two threads?
        //Thread thread1 = new Thread(firstThread);
        //Thread thread2 = new Thread(secondThread);

        //Start the two threads
        //thread1.Start();
        //thread2.Start();

        ThreadStart firstThread = new ThreadStart(Function1);
        ThreadStart secondThread = new ThreadStart(Function2);
        ThreadStart thirdThread = new ThreadStart(Function3);
        ThreadStart fourthThread = new ThreadStart(Function3);

        Thread thread1 = new Thread(firstThread);
        Thread thread2 = new Thread(secondThread);
        Thread thread3 = new Thread(thirdThread);
        Thread thread4 = new Thread(fourthThread);

        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Function1()
    {
        for(int i = 0; i < 10; i++)
        {
            Debug.Log("Thread 1 --> x");
        }
    }

    void Function2()
    {
        for(int x = 0; x < 10; x++)
        {
            Debug.Log("Thread 2 --> y");
        }

    }

    void Function3()
    {
        if(done == false)
        {
            Debug.Log("Done");
            done = true;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Threading;

public class OYOThreadingScript : MonoBehaviour {


    //Bool to stop threads from running
    private bool done = false;

    // Use this for initialization
    void Start()
    {
        //Create 2 new threads starts that points to the functions
        //that the threads will be running
        ThreadStart firstThread = new ThreadStart(DebugX);
        ThreadStart secondThread = new ThreadStart(DebugY);
        ThreadStart thirdThread = new ThreadStart(Zuul);
        ThreadStart forthThread = new ThreadStart(Zuul);

        //Create 2 new threads
        Thread thread1 = new Thread(firstThread);
        Thread thread2 = new Thread(secondThread);
        Thread thread3 = new Thread(thirdThread);
        Thread thread4 = new Thread(forthThread);

        //Start the 2 threads
        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();
        
    }


    void Zuul()//gatekeeper
    {
        if(!done)
        {
            Debug.Log("Done");
            done = true;
        }
    }

    void DebugX()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("X");
        }
    }

    void DebugY()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("Y");
        }
    }
}

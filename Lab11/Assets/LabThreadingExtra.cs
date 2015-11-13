using UnityEngine;
using System.Collections;
using System.Threading;

public class LabThreadingExtra : MonoBehaviour
{
    // Values used for outputting x and y values
    const int X_PRINT = 10;
    const int Y_PRINT = 10;
    // x and y values
    short x = 3;
    short y = 7;
    // Flag to determine if done
    bool done = false;

	void Start () 
    {
        ThreadStart firstThread = new ThreadStart(Function1);
        ThreadStart secondThread = new ThreadStart(Function2);
        ThreadStart thirdThread = new ThreadStart(Function3);
        //ThreadStart fourthThread = new ThreadStart(Function4);

        Thread thread1 = new Thread(firstThread);
        Thread thread2 = new Thread(secondThread);
        Thread thread3 = new Thread(thirdThread);
        //Thread thread4 = new Thread(fourthThread);

        thread1.Start();
        thread2.Start();
        thread3.Start();
	}

    /// <summary>
    /// Function1 prints the x-value 10 times.
    /// </summary>
    void Function1()
    {
        for (int i = 0; i < X_PRINT; i++)
        {
            Debug.Log("X value --> " + x);
        }
    }

    /// <summary>
    /// Function2 prints the y-value 10 times.
    /// </summary>
    void Function2()
    {
        for (int i = 0; i < Y_PRINT; i++)
        {
            Debug.Log("Y value --> " + y);
        }
    }

    /// <summary>
    /// Function3 checks if done is false.
    /// Function3 sets done to true if done is false.
    /// </summary>
    void Function3()
    {
        if (done.Equals(false))
        {
            Debug.Log("Done");
            done = true;
        }
    }
}

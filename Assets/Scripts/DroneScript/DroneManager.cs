using System;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float timer = 0;
    void Start()
    {
        //StartCoroutine(SummonA(1,1,3,3,"DroneShooterE"));
        //StartCoroutine(SummonB(1, 1, 3, 1, "DroneShooterE"));
        //StartCoroutine(SummonC(1, 1, 4, 1, "DroneShooterC"));
        //StartCoroutine(SummonD(1, 1, 3, 3, "DroneShooterB"));
        //StartCoroutine(SummonE(1, 0.3f, 3, "DroneShooterE"));
        //StartCoroutine(SummonE(3, 0.3f, 3, "DroneShooterE"));
        //StartCoroutine(SummonF(3, 0.3f, 3, "DroneShooterE"));
        //StartCoroutine(SummonG(1, 1, 7, 3, "DroneShooterF"));
        //StartCoroutine(SummonH(1, 1, 7, 3, "DroneShooterF"));
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitSecond(int n)
    {
        yield return new WaitForSeconds(n);
    }

    IEnumerator SummonA(float delay, float term, float yOffset, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternA(term, yOffset, enemyNumber, DroneName);
    }

    IEnumerator SummonB(float delay, float term, float yOffset, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternB(term, yOffset, enemyNumber, DroneName);
    }

    IEnumerator SummonC(float delay, float term, float yOffset, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternC(term, yOffset, enemyNumber, DroneName);
    }

    IEnumerator SummonD(float delay, float term, float yOffset, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternD(term, yOffset, enemyNumber, DroneName);
    }


    IEnumerator SummonE(int delay, float term, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternE(term, enemyNumber, DroneName);
    }

    IEnumerator SummonF(int delay, float term, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternF(term, enemyNumber, DroneName);
    }

    IEnumerator SummonG(float delay, float term, float xOffset, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternG(term, xOffset, enemyNumber, DroneName);
    }

    IEnumerator SummonH(float delay, float term, float xOffset, int enemyNumber, String DroneName)
    {
        yield return new WaitForSeconds(delay);
        yield return ShooterPatternH(term, xOffset, enemyNumber, DroneName);
    }



    IEnumerator ShooterPatternA(float sec, float y, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            Drone.transform.position = new Vector3(11, 3, 3);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.wavy;
            dm.yOffset = y;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator ShooterPatternB(float sec, float y, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            Drone.transform.position = new Vector3(11, y, 3);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.stay;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator ShooterPatternC(float sec, float y, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            Drone.transform.position = new Vector3(11, y, 3);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.not;
            yield return new WaitForSeconds(sec);

        }
    }
    IEnumerator ShooterPatternD(float sec, float y, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            Drone.transform.position = new Vector3(12, y, 3);
            dm.pattern = DroneMove.Pattern.circle;
            dm.xCenter = 12;
            dm.yOffset = y;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator ShooterPatternE(float sec, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            Drone.transform.position = new Vector3(11, 6, 0);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.arc;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator ShooterPatternF(float sec, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            Drone.transform.position = new Vector3(11, -6, 0);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.arcrev;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator ShooterPatternG(float sec, float x, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            Drone.transform.position = new Vector3(x, 8, 0);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.up2down;
            dm.xOffset = x;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator ShooterPatternH(float sec, float x, int num, String DroneName)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject(DroneName);
            Drone.transform.position = new Vector3(x, -8, 0);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.down2up;
            dm.xOffset = x;
            yield return new WaitForSeconds(sec);
        }
    }

}

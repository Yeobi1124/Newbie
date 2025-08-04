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
        StartCoroutine(SummonD(1));
        //StartCoroutine(SummonB(3));
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitSecond(int n)
    {
        yield return new WaitForSeconds(n);
    }

    IEnumerator SummonA(int n)
    {
        yield return new WaitForSeconds(n);
        yield return ShooterPatternA(1, 3);
    }

    IEnumerator SummonB(int n)
    {
        yield return new WaitForSeconds(n);
        yield return ShooterPatternB(1, -3);
    }

    IEnumerator SummonC(int n)
    {
        yield return new WaitForSeconds(n);
        yield return ShooterPatternC(1, 0);
    }

    IEnumerator SummonD(int n)
    {
        yield return new WaitForSeconds(n);
        yield return ShooterPatternD(1);
    }



    IEnumerator ShooterPatternA(int sec, int y)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject("DroneShooter");
            Drone.transform.position = new Vector3(11, 3, 3);
            DroneMove dm = Drone.GetComponent<DroneMove>();
            dm.pattern = DroneMove.Pattern.wavy;
            dm.yOffset = y;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator ShooterPatternB(int sec, int y)
    {
        GameObject Drone = DroneObjectManager.Instance.PullObject("DroneShooterB");
        Drone.transform.position = new Vector3(11, y, 3);
        DroneMove dm = Drone.GetComponent<DroneMove>();
        dm.pattern = DroneMove.Pattern.stay;
        yield return new WaitForSeconds(sec);
    }

    IEnumerator ShooterPatternC(int sec, int y)
    {
        GameObject Drone = DroneObjectManager.Instance.PullObject("DroneShooterC");
        Drone.transform.position = new Vector3(11, y, 3);
        DroneMove dm = Drone.GetComponent<DroneMove>();
        dm.pattern = DroneMove.Pattern.not;
        yield return new WaitForSeconds(sec);
    }

    IEnumerator ShooterPatternD(int sec)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject Drone = DroneObjectManager.Instance.PullObject("DroneShooterD");
            DroneMove dm = Drone.GetComponent<DroneMove>();
            Drone.transform.position = new Vector3(12, 7, 3);
            dm.pattern = DroneMove.Pattern.circle;
            dm.xCenter = 12;
            yield return new WaitForSeconds(sec);
        }
    }
}

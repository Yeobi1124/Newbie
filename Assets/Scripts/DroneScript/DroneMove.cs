using System.Threading;
using Unity.Collections;
using UnityEngine;

public class DroneMove : MonoBehaviour
{
    public float xCenter;
    public float yOffset;
    public float Amplitude = 1f;
    public float Frequency = 1f;

    private float DroneSpeed;
    public enum Pattern
    {
        direct, wavy, stay, not, circle
    };

    public Pattern pattern;

    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnEnable()
    {
        DroneSpeed = gameObject.GetComponent<DroneInfo>().DroneSpeed;
        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Move(pattern);
        timer += Time.deltaTime;
    }

    public void Move(Pattern p)
    {
        switch (p)
        {
            case Pattern.direct:
                DirectMove();
                break;
            case Pattern.wavy:
                WavyMove();
                break;
            case Pattern.stay:
                StayMove();
                break;
            case Pattern.not:
                NotMove();
                break;
            case Pattern.circle:
                CircleMove();
                break;
            default:
                Debug.Log("Error! None Pattern!");
                break;
        }
    }

    //직선이동
    private void DirectMove()
    {
        transform.Translate(DroneSpeed * Time.deltaTime, 0, 0);
    }

    //위아래로 파형을 그리며 이동
    private void WavyMove()
    {
        float x = transform.position.x - DroneSpeed * Time.deltaTime;
        float y = yOffset + Mathf.Sin(timer * Frequency) * Amplitude;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    //대기 후 퇴장
    private void StayMove()
    {
        if (gameObject.GetComponent<DroneBShoot>().ShootNum < 5)
        {
            if (transform.position.x > 6.5) transform.Translate(DroneSpeed * Time.deltaTime, 0, 0);
            else gameObject.GetComponent<DroneBShoot>().Shootable = true;
        }
        else
        {
            gameObject.GetComponent<DroneBShoot>().Shootable = false;
            transform.Translate(-DroneSpeed * Time.deltaTime, 0, 0);
        }

    }

    //퇴장 x
    private void NotMove()
    {
        if (transform.position.x > 6.5) transform.Translate(DroneSpeed * Time.deltaTime, 0, 0);
    }

    //회전이동
    private void CircleMove()
    {
        
        float radius = 1f;               
        float angularSpeed = 2f;       
        Vector3 circleCenter = new Vector3(xCenter, yOffset, 0);

        float angle = timer * angularSpeed; // 시간에 따라 증가하는 각도 (라디안)

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = circleCenter + new Vector3(x, y, 0);
        xCenter -= Time.deltaTime * DroneSpeed;
    }
}

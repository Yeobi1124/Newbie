using System.Threading;
using Unity.Collections;
using UnityEngine;
using DG.Tweening;
using System.Xml.Serialization;

public class DroneMove : MonoBehaviour
{
    public float xCenter;
    public float xOffset;
    public float yOffset;
    public float Amplitude = 1f;
    public float Frequency = 1f;


    private float startangle = 120;
    private float startanglerev = 240;
    private float DroneSpeed;
    public enum Pattern
    {
        direct, wavy, stay, not, circle, test, arc, arcrev, up2down, down2up
    };

    public Pattern pattern;

    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.Init();
        startangle = Mathf.Deg2Rad * startangle;
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
            case Pattern.test:
                TestMove();
                break;
            case Pattern.arc:
                ArcMove();
                break;
            case Pattern.arcrev:
                ArcRevMove();
                break;
            case Pattern.up2down:
                Up2DownMove();
                break;
            case Pattern.down2up:
                Down2UpMove();
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
        if (gameObject.GetComponent<DroneInfo>().ShootNum < 5)
        {
            if (transform.position.x > 6.5)
            {
                gameObject.GetComponent<DroneInfo>().Shootable = false;
                transform.Translate(DroneSpeed * Time.deltaTime, 0, 0);
            }
            else gameObject.GetComponent<DroneInfo>().Shootable = true;
        }
        else
        {
            gameObject.GetComponent<DroneInfo>().Shootable = false;
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

    //DOTween test
    private void TestMove()
    {
        Vector3 targetPos = new Vector3(8, 6, 0);
        gameObject.transform.DOLocalMove(targetPos, 10f).SetEase(Ease.OutQuart);
        //transform.DOMove()
    }

    //호선이동
    private void ArcMove()
    {
        float radius = 7f;
        float angularSpeed = DroneSpeed;
        Vector3 circleCenter = new Vector3(13, 0, 0);
        startangle += Time.deltaTime * angularSpeed; // 시간에 따라 증가하는 각도 (라디안)

        float x = Mathf.Cos(startangle) * radius;
        float y = Mathf.Sin(startangle) * radius;

        transform.position = circleCenter + new Vector3(x, y, 0);
    }

    //반대호선
    private void ArcRevMove()
    {
        float radius = 7f;
        float angularSpeed = DroneSpeed;
        Vector3 circleCenter = new Vector3(13, 0, 0);
        startanglerev -= Time.deltaTime * angularSpeed; // 시간에 따라 감소하는 각도 (라디안)

        float x = -Mathf.Cos(startanglerev) * radius;
        float y = -Mathf.Sin(startanglerev) * radius;

        transform.position = circleCenter + new Vector3(x, y, 0);
    }

    //위에서 아래로
    private void Up2DownMove()
    {
        Frequency = 3;
        float x = xOffset + Mathf.Sin(timer * Frequency) * Amplitude;
        float y = transform.position.y - DroneSpeed * Time.deltaTime;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    //아래에서 위로
    private void Down2UpMove()
    {
        Frequency = 3;
        float x = xOffset + Mathf.Sin(timer * Frequency) * Amplitude;
        float y = transform.position.y + DroneSpeed * Time.deltaTime;
        transform.position = new Vector3(x, y, transform.position.z);
    }

}

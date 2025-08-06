using UnityEngine;
using System.Collections;

public class DroneCShoot : MonoBehaviour
{
    private float ShootCounter = 0;
    public GameObject EnemyBullet;
    public float ShootDelay;
    private bool Charging;

    //반동
    public float RecoilDistance;
    public float RecoilDuration;
    public float ReturnDuration;

    private Vector3 originalPosition;
    private bool isRecoiling = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<DroneInfo>().Shootable)
        {
            if (ShootCounter >= ShootDelay)
            {
                Shoot();
                ShootCounter = 0;
            }

            ShootCounter += Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject bullet = DroneObjectManager.Instance.PullObject("BulletEnemyBig");
        bullet.transform.position = transform.position;
        bullet.transform.Translate(0.8f, -0.3f, 0);

        originalPosition = transform.localPosition;
        if (!isRecoiling) StartCoroutine(DoRecoil());
        gameObject.GetComponent<DroneInfo>().ShootNum++;

    }


    IEnumerator DoRecoil()
    {
        isRecoiling = true;

        Vector3 recoilPosition = originalPosition - transform.right * RecoilDistance;

        float elapsed = 0f;

        // 뒤로 밀리기
        while (elapsed < RecoilDuration)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, recoilPosition, elapsed / RecoilDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = recoilPosition;

        elapsed = 0f;

        // 제자리로 복귀
        while (elapsed < ReturnDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsed / ReturnDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        isRecoiling = false;
    }
}

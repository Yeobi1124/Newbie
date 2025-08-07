using UnityEngine;
using System.Collections.Generic;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> backgrounds;
    public float speed;

    void Update()
    {
        foreach (GameObject go in backgrounds)
        {
            go.transform.Translate(-speed * Time.deltaTime, 0, 0);
            if (go.transform.position.x < -22) go.transform.Translate(45, 0, 0);
        }
    }
}

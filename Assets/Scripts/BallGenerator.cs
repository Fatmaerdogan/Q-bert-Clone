using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public List<GameObject> BallList;
    private double timer=0;
    private Vector3 left,right;
    private bool finish;
    void Start()
    {
        left = new Vector3(-0.7071f, 3.0f, 10.5355f);
        right = new Vector3(0.7071f, 3.0f, 10.5355f);
        Events.GameFinish += GameFinish;
    }
    private void OnDestroy()=>Events.GameFinish -= GameFinish;
    public void GameFinish(bool Temp)=>finish = true;
    void Update()
    {
        if (!finish)
        {
            if (timer > 5)
            {
                timer = 0;
                if (Random.Range(1, 6) == 1) SpawnBall(true);
                else SpawnBall(false);
            }
            timer += Time.deltaTime;
        }
    }

    void SpawnBall(bool value)
    {
        GameObject r;
        r = (value ==true)?Instantiate(BallList[0]) :Instantiate(BallList[1]);
        r.transform.position =( Random.Range(1, 3) == 1)?left : right;  
    }
}

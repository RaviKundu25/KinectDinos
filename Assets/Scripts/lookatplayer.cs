using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatplayer : MonoBehaviour
{


    public Transform Player1;
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;




    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player1);

        if (Vector3.Distance(transform.position, Player1.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player1.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }


}

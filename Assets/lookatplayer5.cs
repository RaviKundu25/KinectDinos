using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatplayer5 : MonoBehaviour
{


    public Animator dinoAnim;
    public Transform Player1;
    public float MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 2;




    void Start()
    {

    }

    void Update()
    {

        if (Vector3.Distance(transform.position, Player1.position) >= MaxDist)
        {
            dinoAnim.Play("Tric|Idle1A");
        }

        if (Vector3.Distance(transform.position, Player1.position) >= MinDist && Vector3.Distance(transform.position, Player1.position) <= MaxDist)
        {
            transform.LookAt(Player1);


            dinoAnim.Play("Tric|Walk");
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player1.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
        else
        {
            if (Vector3.Distance(transform.position, Player1.position) <= MinDist)

                dinoAnim.Play("Tric|Idle1A");
        }
    }


}

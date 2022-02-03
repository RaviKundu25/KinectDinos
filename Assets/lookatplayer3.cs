using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatplayer3 : MonoBehaviour
{


    public Animator dinoAnim;
    public Transform Player1, left, right;
    public float MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 2;




    void Start()
    {
        transform.LookAt(right);
    }

    void Update()
    {

   
        if (Vector3.Distance(transform.position, Player1.position) >= MaxDist) 
        {
            if (Vector3.Distance(transform.position, left.position) < 1)
            transform.LookAt(right);

            if (Vector3.Distance(transform.position, right.position) < 1)
                transform.LookAt(left);

            dinoAnim.Play("Ptera|Flight");
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        
       }
        


        if (Vector3.Distance(transform.position, Player1.position) >= MinDist && Vector3.Distance(transform.position, Player1.position) <= MaxDist)
        {
            transform.LookAt(Player1);


            dinoAnim.Play("Ptera|Flight");
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player1.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
        else
        {
            if (Vector3.Distance(transform.position, Player1.position) <= MinDist)
            dinoAnim.Play("Ptera|Statio");
        }
    }


}

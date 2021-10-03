using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    //public int numberOfRays;
    //public float angle;

    //public float rayRange;

    //The player that the enemy has to chase and all the obstacles to avoid crashing into
    private Transform player;
    private Transform obstacle;
    private Transform obstacle1;
    private Transform obstacle2;
    private Transform obstacle3;

    // Start is called before the first frame update
    void Start()
    {
        //Here the game finds all the tags and makes it so that the "Enemy" can interact with them
        player = GameObject.FindGameObjectWithTag("Player").transform;

        obstacle = GameObject.FindGameObjectWithTag("Obstacle0").transform;
        obstacle1 = GameObject.FindGameObjectWithTag("Obstacle1").transform;
        obstacle2 = GameObject.FindGameObjectWithTag("Obstacle2").transform;
        obstacle3 = GameObject.FindGameObjectWithTag("Obstacle3").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Ignore!!
        /*var deltaPosition = Vector3.zero;
        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis(angle: (i / ((float)numberOfRays - 1)) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var ray = new Ray(origin: this.transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {
                deltaPosition -= (1.0f / numberOfRays) * speed * direction;
            }
            else
            {
                deltaPosition -= (1.0f / numberOfRays) * speed * direction;
            }
        }
        this.transform.position += deltaPosition * Time.deltaTime;*/

        //Here the "Enemy" avoids the obstacles if it comes too close
        if(Vector3.Distance(transform.position, obstacle.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle.position, -speed * Time.deltaTime);
        }else
        if (Vector3.Distance(transform.position, obstacle1.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle1.position, -speed * Time.deltaTime);
        }else
        if (Vector3.Distance(transform.position, obstacle2.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle2.position, -speed * Time.deltaTime);
        }else
        if (Vector3.Distance(transform.position, obstacle3.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle3.position, -speed * Time.deltaTime);
        }

        //Here the "Enemy" chases the player
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        //If the "Enemy" comes close it stops chasing
        else if(Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        //If the "Enemy" comes too close it starts to retreat
        else if(Vector3.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        

       

    }


    //Ignore!!
    /*void OnDrawGizmos()
    {
         for(int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis(angle: (i / ((float)numberOfRays - 1)) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;
            Gizmos.DrawRay(from: this.transform.position, direction);
        }
    }*/
}

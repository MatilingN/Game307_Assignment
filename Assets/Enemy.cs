using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float slowingDistance;
    public float stoppingDistance;
    public float retreatDistance;

    //The player that the AI has to chase and all the obstacles to avoid crashing into
    //Need help with this part, dont know how to make it so that it only requires one obstacle for all four spheres
    //Doesn't work if I just copy/paste multiple obstacles and just add the same tag, tried
    private Transform player;
    private Transform obstacle;
    private Transform obstacle1;
    private Transform obstacle2;
    private Transform obstacle3;

    

    // Start is called before the first frame update
    void Start()
    {
        //Here the game finds all the tags and makes it so that the AI can interact with them
        player = GameObject.FindGameObjectWithTag("Player").transform;

        obstacle  = GameObject.FindGameObjectWithTag("Obstacle0").transform;
        obstacle1 = GameObject.FindGameObjectWithTag("Obstacle1").transform;
        obstacle2 = GameObject.FindGameObjectWithTag("Obstacle2").transform;
        obstacle3 = GameObject.FindGameObjectWithTag("Obstacle3").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Here we get the distance between the AI and the obstacles and the player
        float objectdistance = Vector3.Distance(transform.position, obstacle.position);
        float objectdistance2 = Vector3.Distance(transform.position, obstacle1.position);
        float objectdistance3 = Vector3.Distance(transform.position, obstacle2.position);
        float objectdistance4 = Vector3.Distance(transform.position, obstacle3.position);

        float distance = Vector3.Distance(transform.position, player.position);


        //Determine in which direction to rotate towards
        Vector3 targetDirection = player.position - transform.position;

        //Here the speed at which the AI rotates is locked to its movement speed
        float rotationSpeed = speed * Time.deltaTime;

        //Rotate the forward vector towards the target direction
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed, 0.0f);

        //Here we calculate the rotation and apply rotation to the AI
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDirection), 0.15f);

        
        //Here the AI avoids the obstacles if it comes too close
        if (objectdistance < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle.position, 
                -speed * Time.deltaTime);
        }
        else if (objectdistance2 < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle1.position, 
                -speed * Time.deltaTime);
        }
        else if (objectdistance3 < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle2.position, 
                -speed * Time.deltaTime);
        }
        else if (objectdistance4 < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, obstacle3.position, 
                -speed * Time.deltaTime);
        }


        //Here the AI chases the player
        if (distance > slowingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 
                speed * Time.deltaTime);
        }
        //If the AI comes close it starts slowing down
        else if (distance < slowingDistance && distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 
                (speed * distance / slowingDistance) * 0.75f * Time.deltaTime);
        }
        //If the AI comes too close it stops completely
        else if (distance < stoppingDistance && distance > retreatDistance)
        {
            if (distance > 4 && distance < stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, 
                    (speed * distance / slowingDistance) * 0.5f * Time.deltaTime);
            } 
            
            if(distance <= 4.0f)
            {
                transform.position = this.transform.position;
            }
        }
        //If the AI enters the retreat distance it starts to retreat
        else if (distance < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 
                -speed * Time.deltaTime);
        }


    }
}

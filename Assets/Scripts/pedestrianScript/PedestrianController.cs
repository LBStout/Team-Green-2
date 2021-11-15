
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


public class PedestrianController : MonoBehaviour
{

    public NavMeshAgent agent;

    public ThirdPersonCharacter character;

    public float maxWalkDistance = 1.0f;

    private bool depart = false;

    private void Start()
    {
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;


        //    if (Physics.Raycast(ray, out hit)) {
        //        agent.SetDestination(hit.point);
        //    }
        //}

        //if (agent.remainingDistance > agent.stoppingDistance) {
        //    character.Move(agent.desiredVelocity, false, false);
        //}
        //else
        //{
        //    character.Move(Vector3.zero, false, false);
        //}

        if (!depart)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxWalkDistance;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, maxWalkDistance, 1);
            agent.SetDestination(hit.position);
            depart = true;
        }
       

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
            depart = false;
        }


    }
    }

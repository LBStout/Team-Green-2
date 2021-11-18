using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerRenderDistance : MonoBehaviour
{

    private GameObject player;
    private GameObject[] flowers;
    private Renderer[] childRenderer;

    // Start is called before the first frame update
    void Start()
    {
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        childRenderer = gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            for (int i = 0; i < flowers.Length; i++)
            {
                float distance = Vector3.Distance(player.transform.position, flowers[i].transform.position);
                if (distance > 20f || !childRenderer[i].isVisible)
                {
                    childRenderer[i].enabled = false;
                }
                else if (distance < 20f && childRenderer[i].isVisible)
                {
                    childRenderer[i].enabled = true;
                }
            }
        }
    }
}

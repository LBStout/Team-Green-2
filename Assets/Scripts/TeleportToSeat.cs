using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportToSeat : MonoBehaviour
{

    private Vector3 disToSeat;
    public GameObject player;

    public InputActionReference leftHand;
    public InputActionReference rightHand;
    public InputActionAsset asset;

    // Start is called before the first frame update
    void Start()
    {
        if(asset != null)
            asset.Enable();

        // Start player in seat (more likely).
        RecenterPlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHand.action.ReadValue<float>() == 1 && rightHand.action.ReadValue<float>() == 1)
            RecenterPlayerPosition();
    }

    void RecenterPlayerPosition()
    {
        disToSeat.x = transform.position.x - player.transform.position.x;
        disToSeat.z = transform.position.z - player.transform.position.z;
        player.transform.parent.transform.position += new Vector3(disToSeat.x, 0f, disToSeat.z);
    }
}

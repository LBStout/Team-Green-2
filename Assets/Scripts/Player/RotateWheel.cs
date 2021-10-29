using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{

    public SteeringWheel wheel;

    public void setTarget() {
        wheel.Target = this.gameObject;
    }

    public void removeTarget() {
        wheel.Target = null;
    }
}

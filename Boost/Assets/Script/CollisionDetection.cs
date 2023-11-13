using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollsionDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
         switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly!");
                break;

            case "Finish":
                Debug.Log("Finished!");
                break;

            default:
                Debug.Log("Dead :(");
                break;


        }
    }

}

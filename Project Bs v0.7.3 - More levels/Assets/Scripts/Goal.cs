using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public int speed = 300;

    void Update () {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
	}
}

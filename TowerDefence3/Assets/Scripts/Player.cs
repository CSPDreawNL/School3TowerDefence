using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD3.Core;

public class Player : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Health>())
            UIManager.instance.GameOver();
    }
}

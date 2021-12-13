using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEvent : MonoBehaviour {
    
    private Coroutine lazerCountdown;

    public void Start() {
        lazerCountdown = StartCoroutine(LazerCountdown());
    }

    private IEnumerator LazerCountdown() {
        yield return null;
    }
}
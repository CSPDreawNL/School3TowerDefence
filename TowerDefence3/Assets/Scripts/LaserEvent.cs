using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEvent : MonoBehaviour {

    public int laserCountdown = 2;
    public int laserDestructionTimer = 1;
    public int targetDetectDistance = 1;

    private GameObject target;
    private Coroutine laserCoroutine = null;

    private void Update() {
        if (target) {
            if (Vector3.Distance(ZeroYPos(transform.position), ZeroYPos(target.transform.position)) < targetDetectDistance) {
                GetComponent<Steering.SimpleBrain>().enabled = false;
                GetComponent<Steering.Steering>().enabled = false;
                laserCoroutine = StartCoroutine(laserShootEvent());
            }
        }
    }

    private IEnumerator laserShootEvent() {
        yield return new WaitForSeconds(laserCountdown);
        Destroy(target);

        yield return new WaitForSeconds(laserDestructionTimer);
        Destroy(gameObject);
    }

    private Vector3 ZeroYPos(Vector3 _pos) {
        return new Vector3(_pos.x, 0, _pos.z);
    }

    public void InstantiateLaser(GameObject _laserTarget) {
        target = _laserTarget;
    }
}
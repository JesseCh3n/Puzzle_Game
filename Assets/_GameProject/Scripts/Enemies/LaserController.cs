using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserController : MonoBehaviour
{
    [SerializeField] public Health _playerHealth;
    [SerializeField] public float _maxLaserDistance;
    private float _damagePerSecond = 25f;
    private RaycastHit _rayCastHit;
    private LineRenderer lr;

    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
}

    void Update()
    {
        lr.SetPosition(0, transform.localPosition);

        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out _rayCastHit, _maxLaserDistance))
        {
            if (_rayCastHit.collider != null)
            {
                Vector3 localHit = transform.InverseTransformPoint(_rayCastHit.point);
                lr.SetPosition(1, localHit);
                if (_rayCastHit.collider.gameObject.CompareTag("Player"))
                {
                    if (_playerHealth != null)
                    {
                        _playerHealth.DeductHealth(_damagePerSecond * Time.deltaTime);
                    }
                }
            }
        }
        else if (_rayCastHit.collider == null)
        {
            lr.SetPosition(1, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + _maxLaserDistance));
        }
    }


}

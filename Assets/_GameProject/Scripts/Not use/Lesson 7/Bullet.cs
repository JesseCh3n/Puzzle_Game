using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public Health _playerHealth;
    [SerializeField] Transform _shootPoint;
    [SerializeField] float _damage;
    private Transform _enemy;

    void Damage(IDamageable damage)
    {
        if (damage != null)
        {
            damage.GetDamage(_damage);
        }
    }
    private void OnTriggerCollision(Collider other)
    {
        if (!other.gameObject.CompareTag("Robot") && !other.gameObject.CompareTag("Turret"))
        {
            return;
        }
        IDamageable damageable = other.GetComponent<IDamageable>();
        Damage(damageable);
    }
}

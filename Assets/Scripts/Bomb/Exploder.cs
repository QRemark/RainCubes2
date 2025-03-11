using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _explosionForce = 30f;
    private float _explosionRadius = 15f;
    private float _upwardsModifier = 3f;

    public void Detonate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collieder in colliders)
        {
            if (collieder.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
        }
    }
}

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _knockbackForce = 1000f;
    private Transform _parent;

    private void Start()
    {
        _parent = transform.parent;
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        WeaponTransform(_parent.transform.position, _parent.transform.rotation);
    }

    private void WeaponTransform(Vector3 position, Quaternion rotation)
    {
        float currentAnimationSpeed = _moveSpeed;

        float distance = Vector3.Distance(transform.position, position);
        Vector3 targetPosition = Vector3.MoveTowards(GetComponent<Rigidbody>().position, position, currentAnimationSpeed * Time.fixedDeltaTime * distance * distance);
        GetComponent<Rigidbody>().MovePosition(targetPosition);

        Quaternion deltaRotation = Quaternion.RotateTowards(GetComponent<Rigidbody>().rotation, rotation, currentAnimationSpeed * Time.fixedDeltaTime * 10f);
        GetComponent<Rigidbody>().MoveRotation(deltaRotation * Quaternion.AngleAxis(1f, transform.position));

        GetComponent<Rigidbody>().velocity *= .8f;
        GetComponent<Rigidbody>().angularVelocity *= .8f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Rigidbody>() != null)
        {
            collision.transform.GetComponent<Rigidbody>().AddForce(_parent.transform.right * _parent.transform.parent.GetComponent<Rigidbody>().velocity.magnitude * _knockbackForce);
        }
    }
}

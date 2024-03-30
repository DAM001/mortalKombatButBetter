using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _knockbackForce = 1000f;
    private Transform _parent;

    public void SetParent(Transform parent)
    {
        _parent = parent;
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
        Attack(collision.transform, 1f);
    }

    private void OnCollisionStay(Collision collision)
    {
        Attack(collision.transform, .1f);
    }

    private void Attack(Transform hitTrans, float forceModifier)
    {
        if (hitTrans.childCount < 1 || hitTrans.GetChild(0) == _parent) return;
        if (hitTrans.gameObject.tag == "Weapon") return;

        if (hitTrans.GetComponent<Rigidbody>() != null)
        {
            hitTrans.GetComponent<Rigidbody>().AddForce(_parent.transform.right * GetComponent<Rigidbody>().velocity.magnitude * _parent.transform.parent.GetComponent<Rigidbody>().velocity.magnitude * _knockbackForce * forceModifier);
        }
    }
}

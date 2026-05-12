using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask m_hittableMask;
    [SerializeField] private float m_radius = 5.0f;

    private PlayerInput _pi;
    private InputAction _punch;

    private void Awake()
    {
        _pi = GetComponent<PlayerInput>();
        _punch = _pi.actions.FindAction("Punch", true);
    }
    private void OnEnable()
    {
        _punch.performed += Punch;
    }
    private void OnDisable()
    {
        _punch.performed -= Punch;
    }


    private void Punch(InputAction.CallbackContext _)
    {

        Debug.Log("펀치 동작");
        Vector3 center = attackPoint.position;

        Collider[] hitCollisions = Physics.OverlapSphere(center, m_radius, m_hittableMask);

        foreach(Collider hit in hitCollisions)
        {
            IDamageable target = hit.GetComponent<IDamageable>();
            Debug.Log("타겟 인터페이스 검사");
            if(target != null)
            {
                target.TakeDamage(1);
                Debug.Log("타겟 인터페이스 검사 통과");
            }
        }
    }

    /*    void OverlapExample(Vector3 centerPosition)
        {
            Vector3 center = centerPosition;
            float radius = 5.0f;

            Collider[] hitCollisions = Physics.OverlapSphere(center, radius);

            foreach (var hitCollider in hitCollisions)
            {
                Debug.Log($"Detected : {hitCollider.name}");
            }
        }*/

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, m_radius);
    }

}

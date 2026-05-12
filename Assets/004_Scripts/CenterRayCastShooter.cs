using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CenterRayCastShooter : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField]private Camera m_cam;
    [SerializeField] private LayerMask m_hittableMask;
    [SerializeField] private float m_maxDistance = 100.0f;
    [Header("HW")]
    [SerializeField] private LayerMask m_InteracMask;
    [SerializeField] private float m_maxDistan = 100.0f;
    private PlayerInput _pi;
    private InputAction _fire;
    private Collider[] results = new Collider[10];
    private bool IsFind;
    private void Awake()
    {
        _pi = GetComponent<PlayerInput>();
        _fire = _pi.actions.FindAction("Fire", true);

        if (m_cam == null) m_cam = Camera.main;
    }
    private void OnEnable()
    {
        _fire.performed += OnRayFire;
    }
    private void OnDisable()
    {
        _fire.performed -= OnRayFire;
    }
    private void Update()
    {
        Ray_Interact();
    }
    private void OnRayFire(InputAction.CallbackContext _)
    {
        Vector2 _screenCenter = new(Screen.width *0.5f, Screen.height*0.5f);
        Ray _ray = m_cam.ScreenPointToRay(_screenCenter);
        if(Physics.Raycast(_ray, out RaycastHit hit, m_maxDistance, m_hittableMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log($"[CenterRaycastShooter] hit {hit.collider.name} at {hit.point}");
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null) rend.material.color = Color.red;
            Debug.DrawLine(_ray.origin, hit.point, Color.green, 1.0f);
        }
        else
        {
            Debug.DrawLine(_ray.origin, _ray.direction * m_maxDistance, Color.yellow, 0.5f);

        }

    }

    private void Ray_Interact()
    {
        Vector2 _screenCenter = new(Screen.width * 0.5f, Screen.height * 0.5f);
        Ray _ray = m_cam.ScreenPointToRay(_screenCenter);
        if (Physics.Raycast(_ray, out RaycastHit hit, m_maxDistan, m_InteracMask, QueryTriggerInteraction.Ignore))
        {
            

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            Debug.DrawLine(_ray.origin, hit.point, Color.green);
            if (interactable != null && !IsFind)
            {
                interactable.Descrption();
                IsFind = true;
            }
        }
        else
        {
            Debug.DrawLine(_ray.origin, _ray.origin + _ray.direction * m_maxDistan, Color.yellow);
            if (IsFind)
            {
                IsFind = false;
                Debug.Log("감지거리 벗어남");
            }
        }
    }

    void SphereCastExample()
    {
        float radius = 2.0f;
        float maxDistance = 10.0f;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if(Physics.SphereCast(origin,radius,direction, out RaycastHit hit, maxDistance, m_hittableMask))
        {
            Debug.Log($"Sphere Hit {hit.collider.name}");
        }
    }

    void OverlapExample(Vector3 centerPosition)
    {
        Vector3 center = centerPosition;
        float radius = 5.0f;

        Collider[] hitCollisions = Physics.OverlapSphere(center,radius);

        foreach(var hitCollider in hitCollisions)
        {
            Debug.Log($"Detected : {hitCollider.name}");
        }
    }

    void OptimizedOverlap()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, 5.0f, results);

        for(int i = 0; i < count; i++)
        {
            Debug.Log($"NonAlloc Hit : {results[i]}");
        }
    }

/*    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;

        *//*Gizmos.DrawWireSphere(transform.position + transform.forward * 10.0f, 2.0f);*//*
        if (m_cam == null)
            return;

        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Ray ray = m_cam.ScreenPointToRay(screenCenter);

        bool isHit = Physics.Raycast(
            ray,
            out RaycastHit hit,
            m_maxDistan,
            m_InteracMask,
            QueryTriggerInteraction.Ignore
        );

        Vector3 endPoint = isHit
            ? hit.point
            : ray.origin + ray.direction * m_maxDistan;

        Gizmos.color = isHit ? Color.green : Color.yellow;
        Gizmos.DrawLine(ray.origin, endPoint);

        Gizmos.color = isHit ? Color.green : Color.red;
        Gizmos.DrawWireSphere(endPoint, 0.15f);

    }*/
}

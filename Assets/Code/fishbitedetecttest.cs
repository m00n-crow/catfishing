using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    Vector3 throwVector;
    Rigidbody2D _rb;
    LineRenderer _lr;
    Collider2D _collider;
    bool isDragging = false;
    bool hasThrown = false; 
    bool onlyOnce = true; 
    public bool stuerungErlaubt = false; 
    bool colliderSmall = true;

    public const int multiplier = 100;  
    public const float maxThrowDistance = 2;  
    public const int arrowLength = 10;

    private float _maxYPosition;
    private float _startXPosition;
    private const float maxHorizontalDistance = 10f;

    [SerializeField] 
    private float _baseSpeed = 5f;
    [SerializeField] 
    private float _minSpeedFactor = 0.1f;
    private float originalColliderRadius;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
        _collider = GetComponent<Collider2D>();
        if (_collider is CircleCollider2D circleCollider) 
        {
            originalColliderRadius = circleCollider.radius;
        }
    }

    void OnMouseDown()
    {
        if (hasThrown) return;
        isDragging = true;
        CalculateThrowVector();
        SetArrow();
    }

    void OnMouseUp()
    {
        if (hasThrown) return; 
        RemoveArrow();
        Throw();
    }

    void Update()
    {   
        if (isDragging && !hasThrown)
        {
            CalculateThrowVector();
            SetArrow();
        }

        if (hasThrown && transform.position.y < -2) 
        {
            DisableGravity();
        }

        if (stuerungErlaubt)
        {
            CalculateMovement();
        }

        if (hasThrown && transform.position.y < -2 && Input.GetKeyDown(KeyCode.Space) && colliderSmall)
        {
            colliderSmall = false;
            ResizeCollider(3.0f); 
            StartCoroutine(ResetCollider(1.0f));
        }

        // Überprüfen ob Rechtsklick erfolgt
        if (hasThrown && Input.GetMouseButtonDown(1))
        {
            RetrieveHook();
        }
    }

    IEnumerator ResetCollider(float time) 
    {
        yield return new WaitForSeconds(time);
        CircleCollider2D circleCollider = (CircleCollider2D)_collider;
        circleCollider.radius = originalColliderRadius;
        colliderSmall = true;
    }
    
    void ResizeCollider(float factor)
    {
        CircleCollider2D circleCollider = (CircleCollider2D)_collider;
        circleCollider.radius *= factor;
    }
    
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        float distanceFromStart = Mathf.Abs(transform.position.x - _startXPosition);

        float speed = _baseSpeed;
        if ((horizontalInput > 0 && transform.position.x > _startXPosition) || (horizontalInput < 0 && transform.position.x < _startXPosition))
        {
            float speedFactor = Mathf.Lerp(1.0f, _minSpeedFactor, distanceFromStart / maxHorizontalDistance);
            speed *= speedFactor;
        }

        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        if (newPosition.y > _maxYPosition)
        {
            newPosition.y = _maxYPosition;
        }

        if (Mathf.Abs(newPosition.x - _startXPosition) > maxHorizontalDistance)
        {
            newPosition.x = _startXPosition + Mathf.Sign(newPosition.x - _startXPosition) * maxHorizontalDistance;
        }

        transform.position = newPosition;
    }

    void DisableGravity()
    {   
        _rb.gravityScale = 0.5f;
        _rb.velocity = new Vector2(_rb.velocity.y, 0f);
        if (onlyOnce) 
        {
            stuerungErlaubt = true; 
            _maxYPosition = transform.position.y;
            _startXPosition = transform.position.x;
            onlyOnce = false;
        }
    }

    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 distance = mousePos - transform.position;
        
        if (distance.magnitude > maxThrowDistance)
        {
            distance = distance.normalized * maxThrowDistance;
        }

        throwVector = -distance * multiplier; 
    }

    void SetArrow()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0, transform.position);
        _lr.SetPosition(1, transform.position + throwVector / arrowLength);
        _lr.enabled = true;
    }

    void RemoveArrow()
    {
        _lr.enabled = false;
    }

    public void Throw()
    {
        if (hasThrown) return;
        _rb.AddForce(throwVector);
        _rb.gravityScale = 0.1f;

        hasThrown = true;
    }

    public void RetrieveHook()
    {
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0f;
        transform.position = new Vector3(_startXPosition, _maxYPosition, transform.position.z);
        hasThrown = false;
        isDragging = false;
        onlyOnce = true;
        stuerungErlaubt = false;
        colliderSmall = true;
        CircleCollider2D circleCollider = (CircleCollider2D)_collider;
        circleCollider.radius = originalColliderRadius;
    }
}
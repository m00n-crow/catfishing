using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSKRIPTDASERRORT : MonoBehaviour

{
    // Eigenschaften für das Werfen
    Vector3 throwVector;
    Rigidbody2D _rb;
    LineRenderer _lr;
    Collider2D _collider;
    bool isDragging = false;
    bool hasThrown = false;
    bool onlyOnce = true;
    public bool stuerungErlaubt = false;
    bool colliderSmall = true;

    // Konstanten
    public const int multiplier = 100;
    public const float maxThrowDistance = 2;
    public const int arrowLength = 10;

    // Steuerungseigenschaften
    private float _maxYPosition;
    private float _startXPosition;
    private const float maxHorizontalDistance = 10f;

    [SerializeField]
    private float _baseSpeed = 5f;
    [SerializeField]
    private float _minSpeedFactor = 0.1f;
    private float originalColliderRadius;

    // Eigenschaften für das Fangen von Fischen
    public float speed = 2.0f;
    public bool isFishNear = false;
    public GameObject nearestFish;

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
        // Für das Werfen zuständig
        if (isDragging && !hasThrown)
        {
            CalculateThrowVector();
            SetArrow();
        }

        // Aktiviert die Physik des Wassers, sobald der Haken eine gewisse Höhe erreicht hat
        if (hasThrown && transform.position.y < -2)
        {
            DisableGravity();
        }

        // Bewegung nach dem Werfen
        if (stuerungErlaubt)
        {
            MoveHook();
        }

        // Collider vergrößern, wenn die Leertaste gedrückt wird
        if (hasThrown && transform.position.y < -2 && Input.GetKeyDown(KeyCode.Space) && colliderSmall)
        {
            colliderSmall = false;
            ResizeCollider(3.0f);
            StartCoroutine(ResetCollider(1.0f));
        }

        // Rechtsklick zum Einholen
        if (hasThrown && Input.GetMouseButtonDown(1))
        {
            RetrieveHook();
        }

        // Fisch angeln
        if (isFishNear && Input.GetMouseButtonDown(0))
        {
            CatchFish();
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

    void MoveHook()
    {
        // Bewegungscode hier hinzufügen (zum Beispiel mit Pfeiltasten)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    void CatchFish()
    {
        // Ziehe den Fisch hoch
        if (nearestFish != null)
        {
            Destroy(nearestFish);
            isFishNear = false;
            nearestFish = null;
            Debug.Log("Fisch geangelt!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fisch in der Nähe");
            isFishNear = true;
            nearestFish = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fisch weg");
            isFishNear = false;
            nearestFish = null;
        }
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
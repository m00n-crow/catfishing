using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    /** Für Wurf: */  
    Vector3 throwVector;
    Rigidbody2D _rb;
    LineRenderer _lr;
    Collider2D _collider;
    bool isDragging = false;
    bool hasThrown = false; // überprüft ob der Haken schon geworfen wurde 
    bool onlyOnce = true; // Wird nur einmal ausgeführt, um die maximale y-Position zu bestimmen
    public bool stuerungErlaubt = false; // Erlaubt die Steuerung nachdem der Haken geworfen wurdeq
    bool colliderSmall = true; // Variable für die Collider-Größe

    /** AB HIER: Konstanten für den Haken, welche geändert werden können um die Eigenschaften des Hakens zu verändern: */
    public const int multiplier = 100;  // Bestimmt wie schnell der Pfeil ausgemalt werden soll 
    public const float maxThrowDistance = 2;  // Bestimmt wie schnell der Haken maximal geworfen werden soll  
    public const int arrowLength = 10;  // Bestimmt die Länge des Pfeiles -> Höherer Wert = kleinerer Pfeil

    // Für WASD Steuerung: 
    private float _maxYPosition;  // Höchster Punkt für das Movement
    private float _startXPosition;  // Startpunkt der x-Position
    private const float maxHorizontalDistance = 10f;  // Maximale horizontale Bewegungsweite

    [SerializeField] 
    private float _baseSpeed = 5f;  // Basisgeschwindigkeit
    [SerializeField] 
    private float _minSpeedFactor = 0.1f;  // Minimale Geschwindigkeitsfaktor
    private float originalColliderRadius;  // Ursprünglicher Radius des Colliders

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
        _collider = GetComponent<Collider2D>();
        if (_collider is CircleCollider2D circleCollider) // geht irgendwie nicht ohne if 
        {
            originalColliderRadius = circleCollider.radius;
        }
    }

    void OnMouseDown()
    {
        if (hasThrown) return;  // Wenn der Haken schon geworfen wurde, dann wird nichts gemacht
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
        // Für das werfen zuständig 
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

        // Bewegung: 
        if (stuerungErlaubt)
        {
            CalculateMovement();
        }

        // Collider vergrößern, wenn die Leertaste gedrückt wird 
        if (hasThrown && transform.position.y < -2 && Input.GetKeyDown(KeyCode.Space) && colliderSmall)
        {
            colliderSmall = false;
            ResizeCollider(3.0f); 
            StartCoroutine(ResetCollider(1.0f));
        }
    }

    IEnumerator ResetCollider(float time) 
    {
        yield return new WaitForSeconds(time);
        CircleCollider2D circleCollider = (CircleCollider2D)_collider;
        circleCollider.radius = originalColliderRadius;
        colliderSmall = true;
    }
    
    void ResizeCollider(float factor) // Neue Methode
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

        // Bestimme die Geschwindigkeit abhängig von der Bewegungsrichtung
        float speed = _baseSpeed;
        if ((horizontalInput > 0 && transform.position.x > _startXPosition) || (horizontalInput < 0 && transform.position.x < _startXPosition))
        {
            // Langsamer werden, je weiter man zur Grenze geht
            float speedFactor = Mathf.Lerp(1.0f, _minSpeedFactor, distanceFromStart / maxHorizontalDistance);
            speed *= speedFactor;
        }

        // Neue Position mit der berechneten Geschwindigkeit
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // Überprüfen, ob die neue Position den maximalen y-Wert überschreitet
        if (newPosition.y > _maxYPosition)
        {
            newPosition.y = _maxYPosition;
        }

        // Überprüfen, ob die neue Position die maximale horizontale Distanz überschreitet
        if (Mathf.Abs(newPosition.x - _startXPosition) > maxHorizontalDistance)
        {
            newPosition.x = _startXPosition + Mathf.Sign(newPosition.x - _startXPosition) * maxHorizontalDistance;
        }

        transform.position = newPosition;
    }

    void DisableGravity()
    {   
        _rb.gravityScale = 0.5f;
        _rb.velocity = new Vector2(_rb.velocity.y, 0f);  // Setze die y-Komponente der Geschwindigkeit auf 0 (Wurf ausstellen)
        if (onlyOnce) 
        {
            stuerungErlaubt = true; 
            _maxYPosition = transform.position.y;  // Speichere die aktuelle y-Position als Maximum
            _startXPosition = transform.position.x;  // Speichere die aktuelle x-Position als Startpunkt
            onlyOnce = false;
        }
    }

    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;  // Ensure z value is zero for 2D calculations
        Vector2 distance = mousePos - transform.position;
        
        if (distance.magnitude > maxThrowDistance)
        {
            distance = distance.normalized * maxThrowDistance;
        }

        throwVector = -distance * multiplier; 
    }

    // bestimmt die länge des Hakens
    void SetArrow()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0, transform.position);  // Start of the arrow at the object's position
        _lr.SetPosition(1, transform.position + throwVector / arrowLength);  // Ende des Vektors, + vektor um entgegengesetzt
        _lr.enabled = true;
    }

    void RemoveArrow()
    {
        _lr.enabled = false;
    }

    public void Throw()
    {
        if (hasThrown) return;  // Prevent further interactions
        _rb.AddForce(throwVector);
        _rb.gravityScale = 0.1f;  // Enable gravity 

        hasThrown = true;  // Mark als geworfen 
    }
}
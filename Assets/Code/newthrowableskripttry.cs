using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newthrowableskripttry : MonoBehaviour
{
    // Für Wurf:
    Vector3 throwVector;
    public BiteAlertController biteAlertController;
    Rigidbody2D _rb;
    LineRenderer _lr;
    Collider2D _collider;
    bool isDragging = false;
    bool hasThrown = false; // Überprüft ob der Haken schon geworfen wurde
    private bool hasFishBite = false;
    bool onlyOnce = true; // Wird nur einmal ausgeführt, um die maximale y-Position zu bestimmen
    bool startRetrieving = false;
    bool gravityDisabledAfterRestart = false;
    public bool steuerungErlaubt = false; // Erlaubt die Steuerung nachdem der Haken geworfen wurde
    bool neustart = false;
    bool colliderSmall = true; // Variable für die Collider-Größe
    bool hasFish = false;

    // Konstanten für den Haken, welche geändert werden können, um die Eigenschaften des Hakens zu verändern:
    public const int multiplier = 110; // Bestimmt wie schnell der Pfeil ausgemalt werden soll
    public const float maxThrowDistance = 2.5f; // Bestimmt wie schnell der Haken maximal geworfen werden soll
    public const int arrowLength = 9; // Bestimmt die Länge des Pfeiles -> Höherer Wert = kleinerer Pfeil
    
    public float retrieveSpeed = 9.0f; // Neue Variable für die Einholgeschwindigkeit

    // Für WASD Steuerung:
    private float _maxYPosition; // Höchster Punkt für das Movement
    private float _startXPosition; // Startpunkt der x-Position
    private const float maxHorizontalDistance = 10f; // Maximale horizontale Bewegungsweite

    [SerializeField]
    private float _baseSpeed = 5f; // Basisgeschwindigkeit
    [SerializeField]
    private float _minSpeedFactor = 0.1f; // Minimaler Geschwindigkeitsfaktor
    private float originalColliderRadius; // Ursprünglicher Radius des Colliders

    // Eigenschaften für das Fangen von Fischen:
    public float speed = 2.0f;
    public bool isFishNear = false;
    public GameObject nearestFish;
    public string currentlyHookedFishName;
    public bool hasFinishedRetrieving = true;

    // Ausgangsposition:
    private Vector3 ausgangsPosition;

    public HandleCatchInventory handleCatchInventory;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
        _collider = GetComponent<Collider2D>();
        ausgangsPosition = transform.position; // Initialisiere die Ausgangsposition hier
        if (_collider is CircleCollider2D circleCollider) // geht irgendwie nicht ohne if
        {
            originalColliderRadius = circleCollider.radius;
        }

    
        if (biteAlertController == null)
        {
            biteAlertController = FindObjectOfType<BiteAlertController>();
            if (biteAlertController == null)
            {
                Debug.LogError("BiteAlertController not found. Please assign it in the inspector.");
            }
        }
    }

    void OnMouseDown()
    {
        if (hasThrown) return; // Wenn der Haken schon geworfen wurde, dann wird nichts gemacht
        neustart = false;
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
        // Aktiviert die Physik des Wassers, sobald der Haken eine gewisse Höhe erreicht hat
        if (hasThrown && transform.position.y < -2f && !gravityDisabledAfterRestart)
        {   
            // Debug.Log("Gravity disabled slow");
            DisableGravity();
        }
        
        if (Input.GetMouseButtonDown(0) && hasFishBite)
        {
            // Fisch einholen und Alert verstecken
            CatchFish();
            biteAlertController.HideAlert();
            hasFishBite = false;
        }

        // Für das Werfen zuständig
        if (isDragging && !hasThrown)
        {
            CalculateThrowVector();
            SetArrow();
        }



        // Bewegung nach dem Werfen
        if (steuerungErlaubt)
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

        // Rechtsklick zum Einholen
        if (hasFinishedRetrieving && hasThrown && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(RetrieveHookWithFish());
        }

        // Fisch angeln
        if (isFishNear && Input.GetMouseButtonDown(0))
        {
            if (hasFish == false)
            {
            CatchFish();
            // add HideAlert();
            biteAlertController.HideAlert();
            }
        }

        if (startRetrieving || neustart || gravityDisabledAfterRestart)
        {
            //Debug.Log("Gravity disabled");
            DisableGravityCompletely();
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
        if ((newPosition.y > _maxYPosition && !startRetrieving))
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
        _rb.velocity = new Vector2(_rb.velocity.y, 0f); // Setze die y-Komponente der Geschwindigkeit auf 0 (Wurf ausstellen)
        if (onlyOnce)
        {
            steuerungErlaubt = true;
            _maxYPosition = transform.position.y; // Speichere die aktuelle y-Position als Maximum
            _startXPosition = transform.position.x; // Speichere die aktuelle x-Position als Startpunkt
            onlyOnce = false;
        }
    }

    void DisableGravityCompletely()
    {
        Debug.Log("Gravity disabled completely");
        _rb.gravityScale = 0f;
        steuerungErlaubt = false;
    }
    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure z value is zero for 2D calculations
        Vector2 distance = mousePos - transform.position;

        if (distance.magnitude > maxThrowDistance)
        {
            distance = distance.normalized * maxThrowDistance;
        }

        throwVector = -distance * multiplier;
    }

    // Bestimmt die Länge des Hakens
    void SetArrow()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0, transform.position); // Start of the arrow at the object's position
        _lr.SetPosition(1, transform.position + throwVector / arrowLength); // Ende des Vektors, + Vektor um entgegengesetzt
        _lr.enabled = true;
    }

    void RemoveArrow()
    {
        _lr.enabled = false;
    }

    public void Throw()
    {
        if (hasThrown) return; // Prevent further interactions
        _rb.AddForce(throwVector);
        _rb.gravityScale = 0.1f; // Enable gravity

        hasThrown = true; // Mark als geworfen
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fisch in der Nähe");
            isFishNear = true;
            nearestFish = other.gameObject;

            // Alert nu so lange anzeigen, wie kein Fisch am Haken ist
            if (!hasFish)
            {
                Debug.Log("Fisch beißt an");
                biteAlertController.ShowAlert();
                hasFishBite = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fisch weg");
            isFishNear = false;
            nearestFish = null;
            biteAlertController.HideAlert();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void CatchFish()
    {
        hasFish = true; 
        // Ziehe den Fisch hoch
        currentlyHookedFishName = nearestFish.transform.parent.name; // The collision happens on the child object, retrieving the child objects name, which does not help here.

        nearestFish.transform.SetParent(transform); // Setze den Fisch als Kind des Haken-Objekts
        nearestFish.transform.localPosition = Vector3.zero; // Platziere den Fisch am Haken
        isFishNear = false;
        Debug.Log("Fisch geangelt!");
    }

    
    IEnumerator RetrieveHookWithFish()
    {
        gravityDisabledAfterRestart = true;
        startRetrieving = true;
        hasFinishedRetrieving = false;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = ausgangsPosition; // Verwende die Instanzvariable hier
        float journeyLength = Vector3.Distance(startPosition, endPosition);
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, endPosition) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * retrieveSpeed; // Verwende retrieveSpeed
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            yield return null;
        }

        // Nach dem Einholen:
        transform.position = endPosition;
        steuerungErlaubt = false;

        handleCatchInventory.UpdateCatchInventory(currentlyHookedFishName);
        Debug.Log("Haken eingeholt");


        // Falls ein Fisch gefangen wurde, ihn entfernen
        if (hasFish)
        {
            yield return new WaitForSeconds(1);
            Transform destroyChild;
            destroyChild = this.gameObject.transform.GetChild(1);
            Destroy(destroyChild.gameObject); // Optional: Zerstöre den Fisch
            Debug.Log($"Should destroy the nearest fish {destroyChild.gameObject} now!");
            spawnFishies.amountOfFish = spawnFishies.amountOfFish - 1;
            nearestFish = null;
            hasFish = false;
        }

        hasFinishedRetrieving = true;
        ResetScript();
    }

    
    void ResetScript()
    {
        Debug.Log("Reset Script starts now...");
        hasThrown = false;
        isDragging = false;
        onlyOnce = true;
        startRetrieving = false;
        gravityDisabledAfterRestart = false;
        neustart = true;
        steuerungErlaubt = false;
        hasFish = false; 
        colliderSmall = true;
        currentlyHookedFishName = "Kein Fisch am Haken";
        DisableGravityCompletely();


        _rb.gravityScale = 1.0f; // Setzen Sie die Standard-Gravitation zurück
        _rb.velocity = Vector2.zero; // Setzen Sie die Geschwindigkeit zurück
        transform.position = ausgangsPosition; // Setzen Sie die Position zurück

        CircleCollider2D circleCollider = (CircleCollider2D)_collider;
        circleCollider.radius = originalColliderRadius;
    }
}
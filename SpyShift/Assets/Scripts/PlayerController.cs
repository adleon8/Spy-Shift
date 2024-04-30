using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    // References ItemType enum.
    //public ItemType type;

    //move
    private Vector2 moveDir;
    private Rigidbody rigid;
    private int speed = 5;

    public float minSpinDistance;

    private Transform spawnPos; 

    private bool isCrawling;

    public bool isSpawning;

    public static PlayerController Instance;

    [Header("Projectile")]
    public float minPickUpDistance;
    public Slider projectileBar;
    public int forceChangeMultiple;
    private float shootingForce;
    private float addedForce = 0;
    private GameObject rock = null;
    private bool hasRock;
    private bool isShootingRock;
    public LineRenderer lineRenderer;
    public Transform projectileLight;
    private int linePointCount = 20;

    [Header("Item")]
    public bool hasKey;
    public ItemUI currentItemUI;
    public GameObject transquillizerPrefab;
    public GameObject minePrefab;

    [Header("Stamina")]
    public Slider staminaSlider;
    private float stamina = 100;
    private bool isSpeeding;
    public int staminaChangeMultiple;

    // Merged addition from inventory.
    [Header("Inventory")]
    public InventoryObject inventory;
    public GameObject inventoryScreen; // Reference to the inventory UI screen

    public float Stamina { get => stamina; 
        set
        {
            if (value>100)
            {
                value = 100;
            }
            else if (value<0)
            {
                value = 0;
            }
            stamina = value;
            staminaSlider.value = Stamina / 100;
            Color barColor = Color.green;
            if (stamina>=0&&stamina<=20)
            {
                barColor = Color.red;
            }
            else if (stamina>20&&stamina<=60)
            {
                barColor = Color.yellow;
            }
            else
            {
                barColor = Color.green;
            }
            staminaSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = barColor;
        }
    }

    public float ShootingForce { get => shootingForce; 
        set 
        { 
            shootingForce = value;
            projectileBar.value = ShootingForce / 15;
            
        }
    }

    public bool IsCrawling { get => isCrawling; 
        set 
        { 
            isCrawling = value;      
            float Yscale = isCrawling ? 0.5f : 1;
            transform.localScale = new Vector3(1, Yscale, 1);            
        } 
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        if (rigid == null)
        {
            Debug.LogError("Rigidbody not found on the GameObject.");
        }

        Instance = this;
        // Singleton setup
        if (Instance == null)
        {
            Debug.LogError("Failed to set instance");
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the player object consistent across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Ensures only one instance of this GameObject exists
            return;
        }

        spawnPos = GameObject.Find("PlayerSpawnPoint").transform;
        if (spawnPos == null)
        {
            Debug.LogError("PlayerSpawnPoint not found in the scene.");
        }

        if (staminaSlider == null)
        {
            Debug.LogError("Stamina slider is not assigned in the inspector.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        staminaSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
        transform.position = spawnPos.position;
     //   lineRenderer.positionCount = linePointCount;
    }

    public void SetCrawling(bool crawling)
    {
        IsCrawling = crawling;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (isShootingRock)
        {

            if (ShootingForce >= 15)
            {
                addedForce = -Time.deltaTime * forceChangeMultiple;
            }
            else if (ShootingForce <= 1)
            {
                addedForce = Time.deltaTime * forceChangeMultiple;
            }
            ShootingForce += addedForce;
            float g = Physics.gravity.magnitude;
            float zPos = 0;
            float initialVelocity = shootingForce / transform.GetComponent<Rigidbody>().mass;
            float flyTime = (initialVelocity / g) * 2;
            float zOffset = rock.transform.position.z;
            //    float tOffset = (-initialVelocity + Mathf.Sqrt(initialVelocity * initialVelocity + 2 * g * zOffset)) / g;
            //     flyTime += tOffset;
            zPos = initialVelocity * flyTime + 1.5f;
            projectileLight.transform.localPosition = new Vector3(0, 8, zPos);
            //     Projectileline();
        }


        if (IsCrawling)
        {
            Stamina -= Time.deltaTime * staminaChangeMultiple * 2;
            speed = 5;
            if (Stamina <= 0)
            {
                IsCrawling = false;
            }
        }
        else if (isSpeeding)
        {

            Stamina -= Time.deltaTime * staminaChangeMultiple;
            speed = 10;
            if (Stamina <= 0)
            {
                isSpeeding = false;
            }
        }
        else
        {
            Stamina += Time.deltaTime * staminaChangeMultiple;
            speed = 5;
        }


        /*

        if (Input.GetMouseButtonDown(0)&&currentItemUI!=null)
        {
            if (currentItemUI.itemType==ItemType.TranquilizerGun)
            {
                GameObject go = Instantiate(transquillizerPrefab);
            //    Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.nearClipPlane));
                go.transform.position = transform.position;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    go.transform.LookAt(hit.point);
                }
            }
            else if (currentItemUI.itemType==ItemType.Mine)
            {
                GameObject go = Instantiate(minePrefab);
                go.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
            }
            UseItem();
            
        }
        if (Input.GetMouseButtonDown(1)&& currentItemUI != null)
        {
            currentItemUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
            currentItemUI = null;
        }

        
    }

    private void UseItem()
    {
        currentItemUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        currentItemUI.UseNum--;
        currentItemUI = null;
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
    }

        */

    }

    public void HandleInput()
    {
        // Merged addition.
        // Check for item use
        if (Input.GetMouseButtonDown(0) && currentItemUI != null && currentItemUI.Item != null)
        {
            // Use the item
            currentItemUI.Item.Use(gameObject);

            // MAYBE - Manage item UI or inventory here
            currentItemUI.UseItem(); // Need to adjust itemUI accordingly.
        }

        // Deselection
        if (Input.GetMouseButtonDown(1) && currentItemUI != null)
        {
            currentItemUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
            currentItemUI = null;
        }

        // Toggle inventory screen visibility
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Save the inventory when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }

        // Load the inventory when the enter key on the keypad is pressed
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }

        // Tranquilizer
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseTranquilizerGun();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaceMine();
        }
    }

    void UseTranquilizerGun()
    {

    }

    public void ToggleInventory()
    {
        inventoryScreen.SetActive(!inventoryScreen.activeSelf);
    }

    private void Projectileline()
    {
        float initiateVelocity = ShootingForce / rock.GetComponent<Rigidbody>().mass;
        float yAccesserate = Physics.gravity.y / rock.GetComponent<Rigidbody>().mass;
        for (int i = 1; i < linePointCount; i++)
        {
            float zPos = initiateVelocity * i * Time.fixedDeltaTime * 50;
            float yPos = (initiateVelocity + (initiateVelocity - yAccesserate * i*Time.fixedDeltaTime * 50)) / 2 * Time.fixedDeltaTime *i* 50;
            Vector3 pos = transform.TransformDirection(new Vector3(rock.transform.position.x, yPos, zPos));
            lineRenderer.SetPosition(i, pos);
        }
    }
    private void FixedUpdate()
    {
        rigid.velocity = transform.TransformDirection(new Vector3(moveDir.x, rigid.velocity.y, moveDir.y) * speed);
    }

    public void Move(InputAction.CallbackContext input )
    {
        //if (input.performed)
        //{
        //    Vector2 v= input.ReadValue<Vector2>();
        //    rigid.velocity = new Vector3(v.x, 0, v.y);
        //}
        moveDir = input.ReadValue<Vector2>();
    }

    public void Spin(InputAction.CallbackContext input)
    {
    
        if (input.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, minSpinDistance))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    GetComponent<Renderer>().material.color = hit.transform.GetComponent<Renderer>().material.color;
                  
                }
            }
        }    
    }

    public void PickUp(InputAction.CallbackContext input)
    {
        
        if (input.performed)
        {
            if (!hasRock)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, minPickUpDistance))
                {
                    if (hit.transform.gameObject.tag == "Rock")
                    {
                        //     hit.transform.SetParent(transform);
                        hit.transform.GetComponent<Rock>().SetOnPlayer();
                        hasRock = true;
                        rock = hit.transform.gameObject;
                        projectileBar.gameObject.SetActive(true);
                        projectileBar.value = 0;
                        projectileLight.gameObject.SetActive(true);
                        projectileLight.SetParent(transform);
                        projectileLight.transform.localPosition = new Vector3(0, 8, 0);
                    }
                }
            }
            else
            {
                isShootingRock = true;
                ShootingForce = 0;
           //     lineRenderer.SetPosition(0, rock.transform.position);

            }        
        }

        if (input.canceled)
        {
            if (isShootingRock)
            {
                isShootingRock = false;
                hasRock = false;
                rock.transform.GetComponent<Rock>().SetOnPlayer();
                rock.transform.GetComponent<Rigidbody>().AddRelativeForce(0, ShootingForce, ShootingForce,ForceMode.Impulse);
                rock = null;
                projectileBar.gameObject.SetActive(false);
                projectileLight.transform.SetParent(null); 
                projectileLight.gameObject.SetActive(false);
            }
        }

    }

    public void Crawl(InputAction.CallbackContext input)
    {
        if (input.performed && Stamina > 0)
        {
            IsCrawling = !IsCrawling;
           
        }
    }

    public void Speed(InputAction.CallbackContext input)
    {
        if (input.performed&&Stamina>0)
        {
            isSpeeding = true;
        }
        if (input.canceled)
        {
            isSpeeding = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Key
        if (other.tag == "Key")
        {
            hasKey = true;
            Destroy(other.gameObject);
        }

        // Enemy
        if (other.tag == "Enemy")
        {
            transform.position = spawnPos.position;
            isSpawning = true;
            GetComponent<Renderer>().material.color = Color.black;
            Invoke("ResetSpawn", 5);
        }

        // Check for collision with items and add them to the inventory
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject); // Destroy the item after picking it up
        }
    }

    private void ResetSpawn()
    {
        isSpawning = false;
        GetComponent<Renderer>().material.color = Color.gray;
    }

    // Clear the inventory when the application quits
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }


}
   

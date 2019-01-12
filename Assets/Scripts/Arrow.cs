using UnityEngine;

public class Arrow : MonoBehaviour {
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode shootKey;
    public int maxUpAngle;
    public int maxDownAngle;
    public int rotationSpeed;
    public int shootingPower;
    public GameObject hitSound;
    public GameObject spawner;

    private bool shooting = false;
    private bool hit = false;

    void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    void Update ()
    {
	
        if(Input.GetKey(keyUp))
        {
            RotateUp();
        }

        if(Input.GetKey(keyDown))
        {
            RotateDown();
        }

        if(Input.GetKeyDown(shootKey))
        {
            Shoot();
        }

        if(shooting && GetComponent<Rigidbody>())
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        }

	}

    public void RotateUp()
    {
        if(transform.localEulerAngles.x > maxUpAngle)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x - rotationSpeed, transform.localEulerAngles.y, transform.localEulerAngles.z));
        }
    }

    public void RotateDown()
    {
        if(transform.localEulerAngles.x < maxDownAngle)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x + rotationSpeed, transform.localEulerAngles.y, transform.localEulerAngles.z));
        }
    }

    public void Shoot()
    {
        GetComponent<Rigidbody>().velocity = shootingPower * transform.forward;
        GetComponent<Rigidbody>().useGravity = true;
        shooting = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(GetComponent<Rigidbody>() && other.tag == "Target")
        {
            ArrowHit();
            other.gameObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            transform.parent = other.gameObject.transform;
        }

        if(GetComponent<Rigidbody>() && other.tag == "Solid")
        {
            ArrowHit();
        }
    }

    public void ArrowHit()
    {
        if(!hit)
        {
            MainController.mainController.activeArrowList.Remove(gameObject);
            GameObject hitSoundClone = Instantiate(hitSound, transform.position, transform.rotation) as GameObject;
            GetComponent<Item>().spawner.GetComponent<Spawn>().SpawnItem();

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Arrow>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(GetComponent<Item>());
        }

        hit = true;
    }

}

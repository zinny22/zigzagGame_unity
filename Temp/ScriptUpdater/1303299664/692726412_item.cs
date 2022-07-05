using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private GameController gameController;
    [SerializeField]
    private GameObject itemgeteffectPrefab;
    private float rotateSpeed;
    public AudioClip coin;
    public AudioSource audioSource;
    //public AudioClip List;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

    }
    public void SetUp(GameController gameController)
    {
        this.gameController = gameController;
        itemgeteffectPrefab = Instantiate(itemgeteffectPrefab, transform.position, Quaternion.identity);
        itemgeteffectPrefab.SetActive(false);
    }

    private void OnEnable()
    {
        rotateSpeed = Random.Range(10, 100);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(1, 1, 0) * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(coin);
            itemgeteffectPrefab.transform.position = transform.position;
            itemgeteffectPrefab.SetActive(true);

            gameController.IncreaseCoin();
            gameObject.SetActive(false);

        }
    }
}

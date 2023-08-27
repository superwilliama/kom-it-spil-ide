using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private GameObject house;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        transform.position = house.transform.position + offset;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}

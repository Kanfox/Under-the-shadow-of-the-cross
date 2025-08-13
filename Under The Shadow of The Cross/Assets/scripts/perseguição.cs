using UnityEngine;

using UnityEngine.UIElements;

public class EnemieFollowing : MonoBehaviour

{

    [SerializeField] private float speed = 9;

    [SerializeField] private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()

    {

        void Awake()

        {

            target = GameObject.FindGameObjectWithTag("Player").transform;

        }

    }

    // Update is called once per frame

    void Update()

    {

        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }

}


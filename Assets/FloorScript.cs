using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public float speed = 500f;  // Kecepatan lantai bergerak
    public float resetPosition = -1500f; // Jika lantai sudah melewati batas ini, akan di-reset
    public float startPosition = 1287f; // Posisi awal setelah di-reset

    public Transform[] floors;

    void Start()
    {
        // Mengambil semua anak (child) dari GameObject ini
        floors = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            floors[i] = transform.GetChild(i);
        }
    }

    void Update()
    {

        // Gerakkan lantai ke kiri
        foreach (Transform floor in floors)
        {
            floor.position += Vector3.left * speed * Time.deltaTime;

            // Jika lantai sudah melewati batas kiri, pindahkan ke kanan
            if (floor.position.x < resetPosition)
            {
                floor.position = new Vector3(startPosition, floor.position.y, floor.position.z);
            }
        }
    }
}

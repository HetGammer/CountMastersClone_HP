using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    [ExecuteInEditMode]
    public class ObjectPlacer : MonoBehaviour
    {
        public enum PlacementType { Grid, Circle }
        [SerializeField] private PlacementType placementType;

        [Header(" Grid Placement ")]
        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private Vector2 spacing;

        [Header(" Circle Placement ")]
        [SerializeField] private int amount;
        [SerializeField] private float radius;

        [Header(" Settings ")]
        [SerializeField] private GameObject objectToPlace;

        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            if(Application.isPlaying)
                enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            ClearOldObjects();
            PlaceObjects();
        }

        private void ClearOldObjects()
        {
            transform.Clear();
        }

        private void PlaceObjects()
        {
            switch(placementType)
            {
                case PlacementType.Grid:
                    PlaceOnGrid();
                    break;

                case PlacementType.Circle:
                    PlaceOnCircle();
                    break;
            }
        }

        private void PlaceOnGrid()
        {
            float startXOffset = spacing.x / 2f + (float)(rows - 2) / 2f * spacing.x;
            float startZOffset = spacing.y / 2f + (float)(columns - 2) / 2f * spacing.y;


            Vector3 startPosition = transform.position + Vector3.left * startXOffset + Vector3.back * startZOffset;
            float xStep = spacing.x;
            float yStep = spacing.y;

            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    Vector3 spawnPosition = startPosition + xStep * x * Vector3.right + Vector3.forward * y * yStep;
                    Instantiate(objectToPlace, spawnPosition, Quaternion.identity, transform);
                }
            }
        }

        private void PlaceOnCircle()
        {
            float angleStep = 360f / amount;

            for (int i = 0; i < amount; i++)
            {
                float x = radius * Mathf.Cos(i * angleStep * Mathf.Deg2Rad);
                float z = radius * Mathf.Sin(i * angleStep * Mathf.Deg2Rad);

                Vector3 spawnPosition = transform.position + new Vector3(x, 0, z);
                Instantiate(objectToPlace, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}

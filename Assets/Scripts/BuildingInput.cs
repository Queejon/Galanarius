using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

public class BuildingInput : MonoBehaviour
{
    // Grid selection system
    [SerializeField]
    private GridPartial<Tile> grid;
    private Tile selectedTile;
    [SerializeField]
    private Color highlightColor = Color.green;
    [SerializeField]
    private Color selectionColor = Color.red;
    private Tile lastSelectedTile;
    private Color lastSelectedColor;
    private bool tileSaved;
    private Tile savedTile;
    private Color savedColor;

    // Camera movement system
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float panSpeed = 20f;
    [SerializeField]
    private float rotateSpeed = 20f;
    [SerializeField]
    private float zoomSpeed = 20f;
    [SerializeField]
    private float minZoom = 0f;
    [SerializeField]
    private float maxZoom = 110f;
    private float currentZoom = 0f;

    private void Start()
    {
        SetUpGrid();
        cam = Camera.main;
    }

    void Update()
    {
        Selection();
        CameraMovement();
    }

    private void Selection()
    {
        if (grid == null)
            SetUpGrid();
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(grid.GetGridObject(0, 0).z - Camera.main.transform.position.z)));
        selectedTile = grid.GetGridObject(screenPos);
        // Unhighlight logic
        if (lastSelectedTile != null)
        {
            lastSelectedTile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = lastSelectedColor;
            lastSelectedColor = selectedTile == null ? Color.gray : selectedTile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color;
        }
        // Highlight logic
        if (selectedTile != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Reset old tile
                if (savedTile != null)
                    savedTile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = savedColor;
                // Save new tile
                savedColor = selectedTile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color;
                savedTile = selectedTile;
            }
            lastSelectedColor = Input.GetMouseButtonDown(0) ? selectionColor : selectedTile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color;
            selectedTile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Input.GetMouseButtonDown(0) && !tileSaved ? selectionColor : highlightColor;
            if (savedTile != null && selectedTile.CoordinateMatch(savedTile))
                selectedTile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = selectionColor;
            tileSaved = true;
            lastSelectedTile = selectedTile;
        }
    }

    private void CameraMovement()
    {
        // Rotation Controls
        if(savedTile != null && Input.GetMouseButton(1))
        {
            Transform camTransform = cam.transform;
            camTransform.LookAt(savedTile.transform);
            camTransform.RotateAround(savedTile.transform.position, cam.transform.up, Input.GetAxis("Mouse X") * rotateSpeed);
            camTransform.RotateAround(savedTile.transform.position, cam.transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
            
            camTransform.rotation.SetEulerAngles(new Vector3(Mathf.Clamp(camTransform.rotation.eulerAngles.x, -180, 180), Mathf.Clamp(camTransform.rotation.eulerAngles.y, -180, 180), 0));
        }
        // Panning Controls
        if (Input.GetKey(KeyCode.W))
            cam.transform.position += (Vector3.up * panSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            cam.transform.position += (Vector3.down * panSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            cam.transform.position += (Vector3.left * panSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            cam.transform.position += (Vector3.right * panSpeed * Time.deltaTime);
        
        // Zoom Controls
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentZoom < maxZoom)
        {
            cam.transform.position += (cam.transform.forward * zoomSpeed * Time.deltaTime);
            currentZoom += zoomSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && currentZoom > minZoom)
        {
            cam.transform.position += (-cam.transform.forward * zoomSpeed * Time.deltaTime);
            currentZoom -= zoomSpeed * Time.deltaTime;
        }
    }    

    private void SetUpGrid()
    {
        grid = grid == null ? GetComponent<Test>().Tiles : grid;
    }
}

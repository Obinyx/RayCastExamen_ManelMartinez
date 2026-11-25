using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{
    [SerializeField] private Text _text;

    private float _timer;
    private bool _counting = false;
    private int _sceneToLoad;

    InputAction _mouseClick;
    Vector2 _mousePosition;
    InputAction _positionInput;

    void Awake()
    {
        _mouseClick = InputSystem.actions["Attack"];
        _positionInput = InputSystem.actions["MousePosition"];
    }

    void Start()
    {
        _text.text = "";
    }

    void Update()
    {
        _mousePosition = _positionInput.ReadValue<Vector2>();

        if (_mouseClick.WasPressedThisFrame() && !_counting)
        {
            ShootRayCast();
        }

        if (_counting)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                _timer = Mathf.Max(0, _timer);

                _text.text = _timer.ToString("F1");
            }

            if (_timer <= 0)
            {
                _counting = false;
                SceneManager.LoadScene(_sceneToLoad);
            }
        }
    }

    void ShootRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Cube1")
            {
                _sceneToLoad = 1;
            }
            else if (hit.collider.gameObject.name == "Sphere")
            {
                _sceneToLoad = 2;
            }
            else if (hit.collider.gameObject.name == "Cube2")
            {
                _sceneToLoad = 3;
            }
            else
            {
                return;
            }

            _timer = 5f;
            _counting = true;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    //Menu Panel
    private Button _nextButton;
    private Button _backButton;
    private Transform _scene1;
    private Transform _scene2;
    //Camera
    private Camera _camera;
    private float _currentTime = 0f;
    private float _timeToMove = 0.1f;  //the time taken to rotate.
    private bool _isRotating = false; // set this bool true where you wish (such as on input)
    private enum MoveState
    {
        left,
        right,
    }
    private MoveState move;

    private void OnEnable()
    {
        _nextButton = this.transform.Find("Scene1/Next").GetComponent<Button>();
        _backButton = this.transform.Find("Canvas2/Scene2/Back").GetComponent<Button>();
        _scene1 = this.transform.Find("Scene1").GetComponent<Transform>();
        _scene2 = this.transform.Find("Canvas2/Scene2").GetComponent<Transform>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _nextButton.onClick.AddListener(NextClicked);
        _backButton.onClick.AddListener(BackClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRotating == true)
        {
            _camera.transform.eulerAngles = Vector3.Lerp(this._camera.transform.eulerAngles,
                (move==MoveState.right? new Vector3(0f, 90f, 0f): new Vector3(0f,0f,0f))
                , _currentTime / _timeToMove);
            if (_currentTime <= _timeToMove)
            {
                _currentTime += Time.deltaTime/10;
            }
            else
            {
                _isRotating = false; // turns off rotating
                _currentTime = 0f; // resets this variable
            }
        }

    }

    private void NextClicked()
    {
        _isRotating = true;
        move = MoveState.right;
        _scene1.gameObject.SetActive(false);
        _scene2.gameObject.SetActive(true);
    }

    private void BackClicked()
    {
        _isRotating = true;
        move = MoveState.left;
        _scene1.gameObject.SetActive(true);
        _scene2.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Forest");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}

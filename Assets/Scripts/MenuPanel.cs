using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    bool isOpenOption;
    //Menu Panel
    private Button _nextButton;
    private Button _backButton;
    private Button _backButtonOption;
    private Transform _scene1;
    private Transform _scene2;
    private Transform _optionPanel;
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

    //Option Panel
    private Slider _bgmSlider;
    private Slider _seSlider;

    //Flicker 3d sound
    private AudioSource _flickerSound;

    //Global Variable
    AudioManager _audioManager;

    private void OnEnable()
    {
        isOpenOption = false;
        _nextButton = this.transform.Find("Scene1/Next").GetComponent<Button>();
        _backButton = this.transform.Find("Canvas2/Scene2/Back").GetComponent<Button>();
        _backButtonOption = this.transform.Find("OptionPanel/BackOption").GetComponent<Button>();
        _scene1 = this.transform.Find("Scene1").GetComponent<Transform>();
        _scene2 = this.transform.Find("Canvas2/Scene2").GetComponent<Transform>();
        _optionPanel = this.transform.Find("OptionPanel").GetComponent<Transform>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _bgmSlider = this.transform.Find("OptionPanel/BGMSlider").GetComponent<Slider>();
        _seSlider = this.transform.Find("OptionPanel/SESlider").GetComponent<Slider>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _flickerSound = GameObject.Find("FlickerLamp").GetComponent<AudioSource>();

        //Slider
        _bgmSlider.value = _audioManager.BGMSource;
        _seSlider.value = _audioManager.SESource;
        if (_seSlider.value == 0)
        {
            _flickerSound.volume = 0;
        }
        else
        {
            _flickerSound.volume = 0.15f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _nextButton.onClick.AddListener(NextClicked);
        _backButton.onClick.AddListener(BackClicked);
        _backButtonOption.onClick.AddListener(BackOption);
    }

    // Update is called once per frame
    void Update()
    {
        //Camera
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
        //Option Panel
        if (isOpenOption)
        {
            _audioManager.AttachBGMSource.volume = _bgmSlider.value;
            _audioManager.BGMSource = _bgmSlider.value;
            _audioManager.AttachSESource.volume = _seSlider.value;
            _audioManager.SESource = _seSlider.value;
        }

        //Flicker 3D
        if (_seSlider.value == 0)
        {
            _flickerSound.volume = 0;
        }
        else
        {
            _flickerSound.volume = 0.15f;
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

    public void Options()
    {
        isOpenOption = true;
        _scene1.gameObject.SetActive(false);
        _optionPanel.gameObject.SetActive(true);
    }

    public void BackOption()
    {
        isOpenOption = false;
        _scene1.gameObject.SetActive(true);
        _optionPanel.gameObject.SetActive(false);
    }
}

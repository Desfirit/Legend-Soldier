using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private float minSwap = 100f;

    [SerializeField]
    private HeroController _player;

    [SerializeField]
    private RectTransform _leftJoystick;

    [SerializeField]
    private Button _fastBulletButton;

    [SerializeField]
    private Button _stoppingBulletButton;

    [SerializeField]
    private SkillDefinition[] skills;

    private Dictionary<BulletType, SkillDefinition> _skills;

    private Vector3 _defaultPosition;

    private Vector2 _firstPointTouch = Vector2.zero;

    private bool _isLeftSideScreen;

    private BulletType _currentBullets = BulletType.SimpleBullet;

    private Vector2 GetMouseButton()
    {
        if (Input.GetMouseButton(0))
        {
            var vec3 = Input.mousePosition;
            return new Vector2(vec3.x, vec3.y);
        }
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }

        return Vector2.zero;

    }

    private void ChangeAlphaColor(RectTransform leftJoystick, float alpha)
    {
        Image[] images = leftJoystick.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }

    bool GetMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var vec3 = Input.mousePosition;
            _firstPointTouch = new Vector2(vec3.x, vec3.y);
            if (_firstPointTouch.x < Screen.width / 2)
            {
                _leftJoystick.position = _firstPointTouch;
                ChangeAlphaColor(_leftJoystick, 1);
                _isLeftSideScreen = true;
            }
            else
            {
                _isLeftSideScreen = false;
            }
            
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _firstPointTouch = Input.GetTouch(0).position;
            if (_firstPointTouch.x < Screen.width / 2)
            {
                _leftJoystick.position = _firstPointTouch;
                ChangeAlphaColor(_leftJoystick, 1);
                _isLeftSideScreen = true;
            }
            else
            {
                _isLeftSideScreen = false;
            }
        }
        return _firstPointTouch != Vector2.zero;
    }

    bool GetMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _firstPointTouch = Vector2.zero;
            _leftJoystick.position = _defaultPosition;
            ChangeAlphaColor(_leftJoystick, 0.5f);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _firstPointTouch = Vector2.zero;
            _leftJoystick.position = _defaultPosition;
            ChangeAlphaColor(_leftJoystick, 0.5f);
        }
        
        return _firstPointTouch == Vector2.zero;
    }

    public Vector3 SetDirection()
    {

        var moveVector = Vector3.zero;

        if (_firstPointTouch != Vector2.zero && _isLeftSideScreen)
        {
            moveVector += new Vector3(GetMouseButton().x - _firstPointTouch.x, 0f, GetMouseButton().y - _firstPointTouch.y);
            if (moveVector.magnitude == _firstPointTouch.magnitude)
                return Vector3.zero;
            if(moveVector.magnitude > minSwap)
            {
                moveVector = moveVector.normalized;
            }
            else
            {
                return Vector3.zero;
            }
        }
        return moveVector;
    }

    void HandleFastBullet()
    {
        ChangeBullet(BulletType.FastBullet);
    }

    void HandleStoppingBullet()
    {
        ChangeBullet(BulletType.StoppingBullet);
    }

    void ChangeBullet(BulletType bulletType)
    {
        switch (_currentBullets)
        {
            case BulletType.FastBullet:
                _fastBulletButton.GetComponent<SkillButton>().StartCooldown(_skills[_currentBullets].coolDown);
                break;

            case BulletType.StoppingBullet:
                _stoppingBulletButton.GetComponent<SkillButton>().StartCooldown(_skills[_currentBullets].coolDown);
                break;
        }
        switch(bulletType)
        {
            case BulletType.FastBullet:
                _fastBulletButton.GetComponent<SkillButton>().StartUsing(_skills[bulletType].timeWorking);
                _player.SetBulletType(bulletType);
                break;
            case BulletType.SimpleBullet:
                _player.SetBulletType(bulletType);
                break;
            case BulletType.StoppingBullet:
                _stoppingBulletButton.GetComponent<SkillButton>().StartUsing(_skills[bulletType].timeWorking);
                _player.SetBulletType(bulletType);
                break;
        }
        _currentBullets = bulletType;
    }

    private void Start()
    {
        _defaultPosition = _leftJoystick.position;
        ChangeAlphaColor(_leftJoystick, 0.5f);
        _fastBulletButton.onClick.AddListener(HandleFastBullet);
        _stoppingBulletButton.onClick.AddListener(HandleStoppingBullet);
        _skills = new Dictionary<BulletType, SkillDefinition>(skills.Length);
        foreach(var skill in skills)
        {
            _skills[skill.BulletType] = skill;
        }
        _fastBulletButton.GetComponent<SkillButton>().OnFinishUsing += HandleOnFinishSkillUsing;
        _stoppingBulletButton.GetComponent<SkillButton>().OnFinishUsing += HandleOnFinishSkillUsing;
    }

    private void HandleOnFinishSkillUsing()
    {
        ChangeBullet(BulletType.SimpleBullet);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            if (GetMouseButtonDown())
            {
                _player.Move(SetDirection());
            }
            if (GetMouseButtonUp())
            {
                _player.Move(Vector3.zero);
            }
        }
    }
}

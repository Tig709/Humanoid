using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class KeypadTask : MonoBehaviour
{
    public Text _cardSer;
    public Text _cardCode;
    public Text _inputCode;
    public int _codeLength = 5;
    public float _codeResetTimeInSeconds = 0.5f;
    private bool _isResetting = false;
    public Malfunction Cannon;

    private void OnEnable()
    {
        string code = string.Empty;

        for (int i = 0; i < _codeLength; i++)
        {
            code += Random.Range(1, 10);
        }

        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[12];

        for (int i = 0; i < 12; i++)
        {
            stringChars[i] = chars[Random.Range(0,chars.Length)];
        }

        _cardSer.text = new string(stringChars);
        _cardCode.text = code;

        _inputCode.text = string.Empty;
    }

    public void ButtonClick(int number)
    {
        if (_isResetting) { return; }

        _inputCode.text += number;

        if (_inputCode.text == _cardCode.text)
        {
            _inputCode.text = "Correct";
            //StartCoroutine(ResetCode());
            Cannon.FixedTurret();
        }
        else if (_inputCode.text.Length >= _codeLength)
        {
            _inputCode.text = "Failed";
            StartCoroutine(ResetCode());
        }
    }
    private IEnumerator ResetCode()
    {
        _isResetting = true;
        yield return new WaitForSeconds(_codeResetTimeInSeconds);

        _inputCode.text = string.Empty;
        _isResetting = false;
    }
}
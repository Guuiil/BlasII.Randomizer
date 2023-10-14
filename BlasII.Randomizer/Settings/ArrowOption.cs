﻿using Il2CppTGK.Game.Components.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.Randomizer.Settings
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ArrowOption : MonoBehaviour
    {
        private UIPixelTextWithShadow _optionText;
        private Image _leftArrow;
        private Image _rightArrow;

        private string[] _options;
        private int _currentOption;

        public int CurrentOption
        {
            get => _currentOption;
            set
            {
                if (value < 0 || value >= _options.Length)
                    return;

                _currentOption = value;
                UpdateStatus();
            }
        }

        public void ChangeOption(int change)
        {
            CurrentOption += change;
        }

        public void Initialize(UIPixelTextWithShadow optionText, Image leftArrow, Image rightArrow, string[] options)
        {
            _optionText = optionText;
            _leftArrow = leftArrow;
            _rightArrow = rightArrow;

            _options = options;
        }

        private void UpdateStatus()
        {
            _optionText.SetText(_options[_currentOption]);
            _leftArrow.sprite = Main.Randomizer.Data.GetUI(_currentOption == 0 ? DataStorage.UIType.ArrowLeftOff : DataStorage.UIType.ArrowLeftOn);
            _rightArrow.sprite = Main.Randomizer.Data.GetUI(_currentOption == _options.Length - 1 ? DataStorage.UIType.ArrowRightOff : DataStorage.UIType.ArrowRightOn);
        }
    }
}

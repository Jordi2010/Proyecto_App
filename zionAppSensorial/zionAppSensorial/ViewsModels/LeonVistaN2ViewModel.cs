﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class LeonVistaN2ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private bool _hasSelectedCorrect = false;
        private Color _tigreButtonColor = Color.LightGray;
        private Color _leonButtonColor = Color.LightGray;
        private Color _gatoButtonColor = Color.LightGray;
        private Color _guepardoButtonColor = Color.LightGray;
        private Color _pumaButtonColor = Color.LightGray;
        private int _incorrectSelections = 0;

        public event EventHandler NavigateToNextPage;

        public ICommand IncorrectButtonCommand { get; }
        public ICommand LeonButtonClickedCommand { get; }
        public ICommand ImageClickedCommand { get; }

        public Color TigreButtonColor
        {
            get => _tigreButtonColor;
            set
            {
                _tigreButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color LeonButtonColor
        {
            get => _leonButtonColor;
            set
            {
                _leonButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color GatoButtonColor
        {
            get => _gatoButtonColor;
            set
            {
                _gatoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color GuepardoButtonColor
        {
            get => _guepardoButtonColor;
            set
            {
                _guepardoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color PumaButtonColor
        {
            get => _pumaButtonColor;
            set
            {
                _pumaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public LeonVistaN2ViewModel()
        {
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            LeonButtonClickedCommand = new Command(async () => await OnLeonButtonClicked());
            ImageClickedCommand = new Command(async () => await OnImageClicked());
        }

        private async Task OnIncorrectButtonClicked(string button)
        {
            switch (button)
            {
                case "Tigre":
                    TigreButtonColor = Color.Red;
                    break;
                case "Gato":
                    GatoButtonColor = Color.Red;
                    break;
                case "Guepardo":
                    GuepardoButtonColor = Color.Red;
                    break;
                case "Puma":
                    PumaButtonColor = Color.Red;
                    break;
            }

            _incorrectSelections++;

            if (_incorrectSelections >= 4 && !_hasSelectedCorrect)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnLeonButtonClicked()
        {
            _hasSelectedCorrect = true;
            LeonButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");
            ResetButtons(); // Restablece los colores de los botones antes de navegar
            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            TigreButtonColor = Color.LightGray;
            LeonButtonColor = Color.LightGray;
            GatoButtonColor = Color.LightGray;
            GuepardoButtonColor = Color.LightGray;
            PumaButtonColor = Color.LightGray;
            _hasLost = false;
            _incorrectSelections = 0;
            _hasSelectedCorrect = false;
        }

        private async Task OnImageClicked()
        {
            var text = "Con melena majestuosa y un rugido poderoso, en la sabana reino, ¿qué soy?";
            await TextToSpeech.SpeakAsync(text);
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}

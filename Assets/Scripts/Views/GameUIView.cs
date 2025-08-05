using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class GameUIView : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _messageText;
	[SerializeField] private float _messageDisplayTime = 1f;

	[Inject] private readonly GameViewModel _gameViewModel;

	private void Start()
	{
		_gameViewModel.Message
		.Where(message => !string.IsNullOrEmpty(message))
		.Subscribe(ShowMessage);
	}

	private void ShowMessage(string message)
	{
		_messageText.text = message;

		_messageText.alpha = 1f;
		_messageText.gameObject.SetActive(true);

		_messageText.DOKill();
		_messageText.DOFade(1f, 0.3f)
		.OnComplete(() =>
		{
			_messageText.DOFade(0f, 0.3f)
			.SetDelay(_messageDisplayTime)
			.OnComplete(() =>
			{
				_messageText.gameObject.SetActive(false);
			});
		});
	}
}
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationService
{
	public void AnimateCubePlacement(Transform cubeTransform, Vector3 targetPosition, Action onComplete = null)
	{
		float cubeHeight = 100f;
		if(cubeTransform is RectTransform rectTransform)
			cubeHeight = rectTransform.rect.height;

		cubeTransform.localScale = Vector3.zero;
		cubeTransform.localPosition = targetPosition + Vector3.up * cubeHeight;

		cubeTransform.DOScale(Vector3.one * 1.2f, 0.2f)
		.SetEase(Ease.OutBack)
		.OnComplete(() =>
		{
			cubeTransform.DOScale(Vector3.one, 0.2f)
				.SetEase(Ease.OutBounce)
				.OnComplete(() => onComplete?.Invoke());
		});

		cubeTransform.DOLocalMoveY(targetPosition.y, 0.4f)
			.SetEase(Ease.OutBounce);
	}

	public void AnimateCubeDestruction(Transform cubeTransform, Action onComplete = null)
	{
		cubeTransform.DOScale(Vector3.zero, 0.3f)
		.SetEase(Ease.InBack)
		.OnComplete(() => onComplete?.Invoke());
	}

	public void AnimateCubesDownFrom(int startIndex, float size, List<CubeView> cubes, Action onComplete = null)
	{
		Sequence sequence = DOTween.Sequence();
		for(int i = startIndex; i < cubes.Count; i++)
		{
			var targetY = i * size;
			sequence.Join(cubes[i].transform.DOLocalMoveY(targetY, 0.5f).SetEase(Ease.OutBounce));
		}

		sequence.OnComplete(() => onComplete?.Invoke());
	}

	public void AnimateCubeIntoHole(Transform cubeTransform, Vector3 targetPosition, Action onComplete = null)
	{
		Vector3 elevatedStartPosition = targetPosition + (Vector3.up * 200f);
		cubeTransform.position = elevatedStartPosition;

		float speed = 0.5f;
		float fadeSpeed = 1f;

		Sequence holeSequence = DOTween.Sequence();

		holeSequence.Append(cubeTransform.DOMove(targetPosition, speed)
		.SetEase(Ease.InQuad));

		holeSequence.Join(cubeTransform.DOScale(Vector3.zero, speed)
		.SetEase(Ease.InBack));

		holeSequence.Join(cubeTransform.DOLocalRotate(new Vector3(0, 0, 180), speed)
		.SetEase(Ease.InQuad));

		if(cubeTransform.TryGetComponent(out CanvasGroup canvasGroup))
		{
			holeSequence.Join(canvasGroup.DOFade(0f, fadeSpeed)
			.SetEase(Ease.InQuad));
		}

		holeSequence.OnComplete(() =>
		{
			canvasGroup.alpha = 1f;
			cubeTransform.rotation = Quaternion.identity;
			onComplete?.Invoke();
		});
	}
}
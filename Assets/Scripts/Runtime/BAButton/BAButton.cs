﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.BAButton
{
    public class BAButton : MonoBehaviour
    {
        private static readonly int pushButton = Animator.StringToHash("OnButtonPush");
        
        [SerializeField] private float cooldownPeriod = 0.5f;
        [SerializeField] private UnityEvent onButtonPush = new UnityEvent();
        
        private Animator _buttonAnimator;
        private bool isAnimationPlaying;

        private void Awake()
        {
            _buttonAnimator = GetComponent<Animator>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!isAnimationPlaying)
                StartCoroutine(ButtonAnimation());
        }
        
        private IEnumerator ButtonAnimation()
        {
            isAnimationPlaying = true;
            _buttonAnimator.SetTrigger(pushButton);
            var animationLength = _buttonAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
            
            onButtonPush.Invoke();

            yield return new WaitForSeconds(cooldownPeriod);
            isAnimationPlaying = false;
        }
    }

}
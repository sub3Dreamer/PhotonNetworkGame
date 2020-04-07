using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class HpGauge : MonoBehaviour
    {
        [SerializeField] RectTransform _fillAmountRect;
        [SerializeField] int _fillAmountMaxWidth = 250;
        [SerializeField] int _fillAmountHeight = 20;

        public void SetGauge(int currentHp, int maxHp)
        {
            var width = Mathf.Clamp((float)currentHp / maxHp * _fillAmountMaxWidth, 0, _fillAmountMaxWidth);
            _fillAmountRect.sizeDelta = new Vector2(width, _fillAmountHeight);
        }

        /*
        int TestMaxHp = 100;
        int TestCurHp = 100;
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                TestCurHp -= 5;
                SetGauge(TestCurHp, TestMaxHp);
            }
        }
        */
    }
}
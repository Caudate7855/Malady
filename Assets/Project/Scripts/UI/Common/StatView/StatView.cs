using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private TMP_Text _statValue;

        public void SetData(string statName, string statValue)
        {
            _statName.text = statName;
            _statValue.text = statValue;
        }
    }
}
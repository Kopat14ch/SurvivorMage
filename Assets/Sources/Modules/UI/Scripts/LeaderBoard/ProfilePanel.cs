using TMPro;
using UnityEngine;

namespace Sources.Modules.UI.Scripts.LeaderBoard
{
    public class ProfilePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public void SetName(string userName) => _name.text = userName;

        public void SetScore(int score) => _score.text = score.ToString();
    }
}

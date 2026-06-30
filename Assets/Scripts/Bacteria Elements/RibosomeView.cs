using TMPro;
using UnityEngine;

namespace SuperPhage
{
    public class RibosomeView : MonoBehaviour
    {
        [SerializeField] private GameObject Gauge;
        [SerializeField] private GameObject AminoAmount;
        [SerializeField] private GameObject AGTCAmount;

        public void PhageFactoryView()
        {
            EraseView();

            Gauge.gameObject.SetActive(true);
            AminoAmount.gameObject.SetActive(true);
            AGTCAmount.gameObject.SetActive(true);
        }

        public void NucleaseFactoryView()
        {
            EraseView();

            Gauge.gameObject.SetActive(true);
            AminoAmount.gameObject.SetActive(true);
        }

        public void EraseView()
        {
            Gauge.gameObject.SetActive(false);
            AminoAmount.gameObject.SetActive(false);
            AGTCAmount.gameObject.SetActive(false);
        }
    }

}


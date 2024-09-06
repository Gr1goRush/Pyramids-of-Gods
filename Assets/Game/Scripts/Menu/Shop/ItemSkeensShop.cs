using System;
using UnityEngine;

namespace Shop
{
    public class ItemSkeensShop : MonoBehaviour
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Transform _context;
        public string ItemName => _itemName;
        public event Action<string, string> OnItemSelected;

        private ShopItem[] _skeens;
        private bool _isInicialized;

        public void Start()
        {
            if (!_isInicialized)
            {
                Open();
                Init();
            }
            if (_context.gameObject.activeInHierarchy)
            {
                Close();
            }

        }
        public void Init()
        {

            _skeens = GetComponentsInChildren<ShopItem>();
            Wallet wallet = ServiceLocator.Locator.CoinsWallet;
            for (int i = 0; i < _skeens.Length; i++)
            {

                _skeens[i].Init(wallet, _itemName);
                _skeens[i].OnSelected += (name) =>
                {
                    for (int i = 0; i < _skeens.Length; i++)
                    {
                        if (_skeens[i].Name != name) _skeens[i].MarkAsNoSelected();
                    }
                    OnItemSelected?.Invoke(_itemName, name);
                };
            }
        }
        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
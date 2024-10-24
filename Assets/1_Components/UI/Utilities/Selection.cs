namespace UnityEngine.UI
{
    public abstract class Selection : MonoBehaviour
    {
        public int Value { get; private set; } = -1;
        public bool IsSelected => Value != -1;

        [SerializeField] private SelectionOption optionPrefab;
        [SerializeField] private Transform optionsContainer;

        protected SelectionOption[] options;

        protected abstract void OnOptionCreated(SelectionOption option);
        protected abstract void OnValueChanged(int oldValue, int newValue);


        protected void CreateOptions(int count)
        {
            options = new SelectionOption[count];
            for (int i = 0; i < count; i++)
            {
                options[i] = Instantiate(optionPrefab, optionsContainer);
                options[i].Init(i, SelectOption);
                OnOptionCreated(options[i]);
            }
            
            void SelectOption(SelectionOption option)
            {
                int oldValue = Value;
                Value = option.Value;
                OnValueChanged(oldValue, Value);
            }
        }
    }
}

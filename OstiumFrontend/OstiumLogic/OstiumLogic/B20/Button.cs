namespace B20.Logic
{
    public abstract class Button
    {
        public void Click()
        {
            OnClick();
        }

        protected abstract void OnClick();
    }
}

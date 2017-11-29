using GalaSoft.MvvmLight;

namespace StudyingForDummies.Model
{
    public class Answer : ObservableObject
    {
        private bool _isSelected;

        public Answer()
        {
        }
        public Answer(int id, string text)
        {
            Id = id;
            Text = text;
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int RightAnswer { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }
    }
}
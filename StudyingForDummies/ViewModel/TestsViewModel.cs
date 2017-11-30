using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StudyingForDummies.Model;
using StudyingForDummies.Service;
using System.Collections.Generic;
using System.Windows.Input;

namespace StudyingForDummies
{
    class TestsViewModel : ObservableObject
    {
        private QuestionsService _questionsService = new QuestionsService();
        private IEnumerable<Question> _questions;
        private string _result;


        public TestsViewModel()
        {
            Questions = _questionsService.GetQuestions(10);
            CheckResultsCommand = new RelayCommand(() =>
           {
               Result = _questionsService.CheckResult(Questions);
           });
        }


        public ICommand CheckResultsCommand { get; set; }

        public IEnumerable<Question> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged(() => Result);
            }
        }
    }
}

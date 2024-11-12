using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UiNutriguia.ViewModels.Pages
{
    public partial class SchedulerViewModel : ObservableObject
    {

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                SetProperty(ref selectedDate, value);
                OnPropertyChanged(nameof(SelectedDateOnly));
            }
        }

        public DateOnly SelectedDateOnly => DateOnly.FromDateTime(SelectedDate);

        public ICommand AddAppointmentCommand { get; }

        public SchedulerViewModel()
        {
            selectedDate = DateTime.Now;
        }

    }
}

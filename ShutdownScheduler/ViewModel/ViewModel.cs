using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Mvvm;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ShutdownScheduler.ViewModel
{
    public class ViewModel : ReactiveObject
    {
        [Reactive] public bool IsRestart { get; set; }
        [Reactive] public DateTime Date { get; set; } = DateTime.Today;
        [Reactive] public DateTime Time { get; set; } = DateTime.Now;

        [Reactive] public ushort SelectedHours { get; set; } = 1;
        [Reactive] public byte SelectedMinutes { get; set; } = 0;
        [Reactive] public byte SelectedSeconds { get; set; } = 0;

        [Reactive] public ushort Seconds { get; set; } = 3600;
        [Reactive] public ActionType Type { get; set; } = ActionType.OffsetDateTime;

        private DelegateCommand start;
        public DelegateCommand Start => start ??= new DelegateCommand(() =>
        {
            switch (Type)
            {
                case ActionType.ExactTime:
                    Model.Model.PerformAction(IsRestart, Date.AddHours(Time.Hour).AddMinutes(Time.Minute));
                    break;
                case ActionType.OffsetDateTime:
                    Model.Model.PerformAction(IsRestart, SelectedHours, SelectedMinutes, SelectedSeconds);
                    break;
                case ActionType.OffsetSeconds:
                    Model.Model.PerformAction(IsRestart, Seconds);
                    break;
                case ActionType.Cancel:
                    Model.Model.CancelShutdownOrRestart();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });


    }
    public enum ActionType
    {
        ExactTime,
        OffsetDateTime,
        OffsetSeconds,
        Cancel
    }
}

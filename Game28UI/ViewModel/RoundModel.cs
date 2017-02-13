using System;

namespace Game28UI.ViewModel
{
    public class RoundModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }

        public bool IsDan { get { return Value % 2 != 0; } }
        public bool IsZhong { get { return Value >= 10 && Value <= 17; } }
        public bool IsXiao { get { return Value <= 13; } }
    }
}

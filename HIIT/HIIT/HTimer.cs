using System.Collections.Generic;

namespace HIIT
{
    class HTimer
    {
        public enum Cycle { WarmUp, Work, Rest, CoolDown }
        public Dictionary<Cycle, int> Lengths;
        public int Length { get; }
        private readonly (Cycle, int Time)[] Intervals;

        public HTimer(int WarmUp, int Work, int Rest, int CoolDown, int Rounds)
        {
            Lengths = new Dictionary<Cycle, int>()
            {
                [Cycle.WarmUp]   = WarmUp,
                [Cycle.Work]     = Work,
                [Cycle.Rest]     = Rest,
                [Cycle.CoolDown] = CoolDown
            };

            Intervals = new (Cycle, int)[2 + Rounds * 2];

            int current = 0;
            Intervals[0] = (Cycle.WarmUp, current += WarmUp);
            for (int i = 1; i < Rounds * 2; i += 2)
            {
                Intervals[i]     = (Cycle.Work, current += Work);
                Intervals[i + 1] = (Cycle.Rest, current += Rest);
            }

            Intervals[Intervals.Length - 1] = (Cycle.CoolDown, current += CoolDown);
            Length = Intervals[Intervals.Length - 1].Time;
        }

       public IEnumerable<(Cycle, int)> Run()
        {
            for (int rn = 1; rn <= Length; rn++)
            {
                foreach ((Cycle c, int i) in Intervals)
                {
                    if (rn <= i)
                    {
                        yield return (c, i - rn);
                        break;
                    }
                }
            }
        }

    }
}

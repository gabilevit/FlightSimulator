using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Simulators
{
    public interface IArrivalSimulator
    {
        void EmitArrival();

        void Start();
    }
}

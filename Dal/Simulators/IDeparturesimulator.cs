using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Simulators
{
    public interface IDeparturesimulator
    {
        void EmitDeparture();

        void Start();
    }
}

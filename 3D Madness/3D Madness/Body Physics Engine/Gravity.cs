using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Gravity : ForceGenerator
    {
        //=------------------data member------=
        Vector3 gravity;

        //=------------------method-----------=


        Gravity(Vector3 gravity)
        {
            this.gravity = gravity; 
        }

        void ParticleForceGenerator.updateForce(Body body, float duration)
        {
            // Check that we do not have infinite mass
            if (!body.hasFiniteMass())
                     return;

            // Apply the mass-scaled force to the particle
            body.addForce(gravity * body.getMass());
        }

    }
}
